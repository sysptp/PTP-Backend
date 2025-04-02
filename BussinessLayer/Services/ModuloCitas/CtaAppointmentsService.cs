using System.Runtime.CompilerServices;
using AutoMapper;
using BussinessLayer.DTOs.ModuloCitas.CtaAppointments;
using BussinessLayer.DTOs.ModuloCitas.CtaEmailBackgroundJobData;
using BussinessLayer.Enums;
using BussinessLayer.Interface.Repository.Modulo_Citas;
using BussinessLayer.Interface.Repository.ModuloCitas;
using BussinessLayer.Interfaces.Repository.ModuloCitas;
using BussinessLayer.Interfaces.Repository.ModuloGeneral.Seguridad;
using BussinessLayer.Interfaces.Services.ModuloCitas;
using BussinessLayer.Interfaces.Services.ModuloGeneral.Email;
using BussinessLayer.Services;
using BussinessLayer.Wrappers;
using DataLayer.Models.ModuloCitas;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

namespace DataLayer.Models.Modulo_Citas
{
    public class CtaAppointmentsService : GenericService<CtaAppointmentsRequest, CtaAppointmentsResponse, CtaAppointments>, ICtaAppointmentsService
    {
        private readonly ICtaAppointmentsRepository _appointmentRepository;
        private readonly IGnEmailService _gnEmailService;
        private readonly IUsuarioRepository _userRepository;
        private readonly ICtaContactRepository _contactRepository;
        private readonly ICtaGuestRepository _guestRepository;
        private readonly ICtaAppointmentSequenceService _sequenceService;
        private readonly ICtaAppointmentUsersRepository _userAppointmentRepository;
        private readonly ICtaAppointmentContactsRepository _appointmentContactsRepository;
        private readonly ICtaAppointmentGuestRepository _ctaAppointmentGuestRepository;
        private readonly ICtaEmailTemplatesRepository _ctaEmailTemplateRepository;
        private readonly ICtaAppointmentAreaRepository _ctaAppointmentAreaRepository;
        private readonly ICtaBackgroundEmailService _backgroundEmailService;
        private readonly ICtaStateRepository _ctaStateRepository;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public CtaAppointmentsService(ICtaAppointmentsRepository appointmentRepository,
            IGnEmailService gnEmailService, IUsuarioRepository userRepository,
            ICtaContactRepository contactRepository,
            ICtaGuestRepository guestRepository, IMapper mapper, ICtaAppointmentSequenceService appointmentSequenceService,
            ICtaAppointmentUsersRepository userUsersRepository,
            ICtaAppointmentGuestRepository ctaAppointmentGuestRepository, ICtaAppointmentContactsRepository appointmentContactsRepository,
            ICtaEmailTemplatesRepository ctaEmailTemplateRepository, ICtaAppointmentAreaRepository ctaAppointmentAreaRepository,
            ICtaBackgroundEmailService backgroundEmailService, ICtaStateRepository ctaStateRepository, IConfiguration configuration) : base(appointmentRepository, mapper)
        {
            _appointmentRepository = appointmentRepository;
            _gnEmailService = gnEmailService;
            _userRepository = userRepository;
            _contactRepository = contactRepository;
            _guestRepository = guestRepository;
            _sequenceService = appointmentSequenceService;
            _userAppointmentRepository = userUsersRepository;
            _ctaAppointmentGuestRepository = ctaAppointmentGuestRepository;
            _appointmentContactsRepository = appointmentContactsRepository;
            _mapper = mapper;
            _ctaEmailTemplateRepository = ctaEmailTemplateRepository;
            _ctaAppointmentAreaRepository = ctaAppointmentAreaRepository;
            _backgroundEmailService = backgroundEmailService;
            _ctaStateRepository = ctaStateRepository;
            _configuration = configuration;
        }

        public async Task<CtaAppointmentsResponse> AddAppointment(CtaAppointmentsRequest vm,bool IsForSession)
        {
            var nextSequence = await _sequenceService.GetFormattedSequenceAsync(vm.CompanyId, vm.AreaId);
            vm.AppointmentCode = nextSequence;

            await _sequenceService.UpdateSequenceAsync(vm.CompanyId, vm.AreaId);

            var appointmentEntity = await base.Add(vm);

            var appointmentId = appointmentEntity.AppointmentId;

            await AddAppointmentParticipants(vm, appointmentId, appointmentEntity);

            if (!IsForSession)
            {
                await SendAppointmentEmailsAsync(vm, vm.CompanyId);
            }

            return _mapper.Map<CtaAppointmentsResponse>(appointmentEntity);
        }

        public async Task AddAppointmentParticipants(CtaAppointmentsRequest vm, int appointmentId, CtaAppointmentsResponse appointmentEntity)
        {

            if (vm.AppointmentParticipants != null && vm.AppointmentParticipants.Any())
            {

                if (vm.AppointmentParticipants.Any(x => x.ParticipantTypeId == (int)AppointmentParticipant.SystemUser))
                {
                    var usersToInsert = vm.AppointmentParticipants.Where(x => x.ParticipantTypeId == (int)AppointmentParticipant.SystemUser)
                    .Select(user => new CtaAppointmentUsers
                    {
                        UserId = user.ParticipantId,
                        AppointmentId = appointmentId,
                        CompanyId = appointmentEntity.CompanyId
                    }).ToList();

                    await _userAppointmentRepository.AddRangeAsync(usersToInsert);
                }

                if (vm.AppointmentParticipants.Any(x => x.ParticipantTypeId == (int)AppointmentParticipant.Contact))
                {
                    var contactsToInsert = vm.AppointmentParticipants.Where(x => x.ParticipantTypeId == (int)AppointmentParticipant.Contact)
                    .Select(contact => new CtaAppointmentContacts
                    {
                        ContactId = contact.ParticipantId,
                        AppointmentId = appointmentId,
                        CompanyId = appointmentEntity.CompanyId
                    }).ToList();

                    await _appointmentContactsRepository.AddRangeAsync(contactsToInsert);
                }

                if (vm.AppointmentParticipants.Any(x => x.ParticipantTypeId == (int)AppointmentParticipant.Guest))
                {
                    var guestToInsert = vm.AppointmentParticipants.Where(x => x.ParticipantTypeId == (int)AppointmentParticipant.Guest)
                    .Select(guest => new CtaAppointmentGuest
                    {
                        AppointmentId = appointmentId,
                        CompanyId = appointmentEntity.CompanyId,
                        GuestId = guest.ParticipantId
                    }).ToList();

                    await _ctaAppointmentGuestRepository.AddRangeAsync(guestToInsert);
                }
            }
        }

        public override async Task<List<CtaAppointmentsResponse>> GetAllDto()
        {
            try
            {
                var appointments = await _appointmentRepository.GetAllWithIncludeAsync(new List<string>
            { "CtaAppointmentReason",
                "CtaMeetingPlace",
                "CtaState",
                "CtaAppointmentManagement"});

                var appointmentDtoList = _mapper.Map<List<CtaAppointmentsResponse>>(appointments.OrderByDescending(x => x.AppointmentId));
                
                foreach(var appointmentDto in appointmentDtoList)
                {
                    var area = await _ctaAppointmentAreaRepository.GetById(appointmentDto.AreaId);
                    appointmentDto.Area = area?.Description;
                    appointmentDto.Participants = await GetAllParticipantsByAppointmentId(appointmentDto.AppointmentId);
                }

                return appointmentDtoList;
            }
            catch (AutoMapperMappingException ex)
            {
                throw new Exception($"Error en el mapeo: {ex.Message}, InnerException: {ex.InnerException?.Message}");
            }
        }

        public override async Task<CtaAppointmentsResponse> GetByIdResponse(object id)
        {
            var appointment = await base.GetByIdResponse(id);
            if(appointment != null)
            {
                appointment.Participants = await GetAllParticipantsByAppointmentId(appointment.AppointmentId);
                return appointment;
            }
            return null;
        }

        // 3. Modifica el método SendAppointmentEmailsAsync en tu servicio de Appointments
        private async Task SendAppointmentEmailsAsync(CtaAppointmentsRequest appointment, long companyId)
        {
            var creator = await _userRepository.GetById(appointment.UserId);
            var appointmentState = await _ctaStateRepository.GetById(appointment.IdState);

            var configAssignedSubject = _configuration["EmailTemplates:DefaultTemplates:AssignedUserTemplate:Subject"];
            var configAssignedBody = _configuration["EmailTemplates:DefaultTemplates:AssignedUserTemplate:Body"];
            var configParticipantSubject = _configuration["EmailTemplates:DefaultTemplates:ParticipantTemplate:Subject"];
            var configParticipantBody = _configuration["EmailTemplates:DefaultTemplates:ParticipantTemplate:Body"];

            var emailTemplateForAssignedUser = await _ctaEmailTemplateRepository.GetById(appointmentState.EmailTemplateIdIn);
            var emailTemplateForParticipant = await _ctaEmailTemplateRepository.GetById(appointmentState.EmailTemplateIdOut);

            var allContacts = await _contactRepository.GetAll();
            var allUsers = await _userRepository.GetAll();
            var allGuests = await _guestRepository.GetAll();

            var contactEmails = (appointment.AppointmentParticipants?
                .Select(c => allContacts.FirstOrDefault(x =>
                    c.ParticipantTypeId == (int)AppointmentParticipant.Contact && x.Id == c.ParticipantId)?.ContactEmail)
                .Where(email => !string.IsNullOrEmpty(email)) ?? new List<string>()).ToList();
            var userEmails = (appointment.AppointmentParticipants?
                .Select(u => allUsers.FirstOrDefault(x =>
                    u.ParticipantTypeId == (int)AppointmentParticipant.SystemUser && x.Id == u.ParticipantId)?.Email)
                .Where(email => !string.IsNullOrEmpty(email)) ?? new List<string>()).ToList();
            var guestEmails = (appointment.AppointmentParticipants?
                .Select(g => allGuests.FirstOrDefault(x =>
                    g.ParticipantTypeId == (int)AppointmentParticipant.Contact && x.Id == g.ParticipantId)?.Email)
                .Where(email => !string.IsNullOrEmpty(email)) ?? new List<string>()).ToList();

            string assignedBody = emailTemplateForAssignedUser?.Body ?? configAssignedBody;
            string participantBody = emailTemplateForParticipant?.Body ?? configParticipantBody;

            assignedBody = _ctaEmailTemplateRepository.ReplaceEmailTemplateValues(assignedBody, appointment);
            participantBody = _ctaEmailTemplateRepository.ReplaceEmailTemplateValues(participantBody, appointment);

            var emailData = new CtaEmailBackgroundJobData
            {
                CreatorEmails = new List<string> { creator.Email },
                ContactEmails = contactEmails,
                UserEmails = userEmails,
                GuestEmails = guestEmails,
                AssignedSubject = emailTemplateForAssignedUser?.Subject ?? configAssignedSubject,
                AssignedBody = assignedBody,
                ParticipantSubject = emailTemplateForParticipant?.Subject ?? configParticipantSubject,
                ParticipantBody = participantBody,
                CompanyId = companyId
            };

            _backgroundEmailService.QueueAppointmentEmails(emailData);
        }

        public async Task<DetailMessage> ExistsAppointmentInTimeRange(CtaAppointmentsRequest appointmentDto)
        {
            var existingAppointments = await _appointmentRepository.GetAppointmentsByDate(appointmentDto.AppointmentDate, appointmentDto.CompanyId, appointmentDto.UserId);

            var existAppointment = existingAppointments.Any(a =>
                (appointmentDto.AppointmentTime >= a.AppointmentTime && appointmentDto.AppointmentTime < a.EndAppointmentTime) ||
                (appointmentDto.EndAppointmentTime > a.AppointmentTime && appointmentDto.EndAppointmentTime <= a.EndAppointmentTime) ||
                (appointmentDto.AppointmentTime <= a.AppointmentTime && appointmentDto.EndAppointmentTime >= a.EndAppointmentTime));

            if (existAppointment)
            {
                return new DetailMessage()
                {
                    Message = "La cita que desea crear posee conflicto con otra cita.",
                    Details = $"{appointmentDto.Description} programada de {appointmentDto.AppointmentTime} a {appointmentDto.EndAppointmentTime}",
                    Action = "¿Desea eliminar esta cita y proceder con la creación?"
                };
            }

            return null;
        }

        public async Task DeleteExistsAppointmentInTimeRange(CtaAppointmentsRequest appointmentDto)
        {
            var existingAppointments = await _appointmentRepository.GetAppointmentsByDate(appointmentDto.AppointmentDate, appointmentDto.CompanyId, appointmentDto.UserId);

            var existAppointment = existingAppointments.Any(a =>
                (appointmentDto.AppointmentTime >= a.AppointmentTime && appointmentDto.AppointmentTime < a.EndAppointmentTime) ||
                (appointmentDto.EndAppointmentTime > a.AppointmentTime && appointmentDto.EndAppointmentTime <= a.EndAppointmentTime) ||
                (appointmentDto.AppointmentTime <= a.AppointmentTime && appointmentDto.EndAppointmentTime >= a.EndAppointmentTime));

            if (existingAppointments.Count() > 0)
            {
                foreach (var appointment in existingAppointments)
                {
                    await _appointmentRepository.Delete(appointment.AppointmentId);
                }
            }
        }

        public async Task<List<AppointmentParticipantsResponse>> GetAllParticipantsByAppointmentId(int appointmentId)
        {
            var contactList = await _appointmentContactsRepository.GetAllContactsByAppointmentId(appointmentId);
            var userSystemList = await _userAppointmentRepository.GetAllUserByAppointmentId(appointmentId);
            var guestList = await _ctaAppointmentGuestRepository.GetAllGuestByAppointmentId(appointmentId);
            var participantList = new List<AppointmentParticipantsResponse>();

            foreach (var contact in contactList)
            {
                var participant = new AppointmentParticipantsResponse
                {
                    ParticipantId = contact.Id,
                    ParticipantTypeId = (int)AppointmentParticipant.Contact,
                    ParticipantEmail = contact.ContactEmail,
                    ParticipantName = contact.Name,
                    ParticipantPhone = contact.ContactNumber,
                    CompanyId = contact.CompanyId
                };
                participantList.Add(participant);
            }

            foreach (var user in userSystemList)
            {
                var participant = new AppointmentParticipantsResponse
                {
                    ParticipantId = user.Id,
                    ParticipantTypeId = (int)AppointmentParticipant.SystemUser,
                    ParticipantEmail = user.Email,
                    ParticipantName = user.Nombre + " " + user.Apellido,
                    ParticipantPhone = user.TelefonoPersonal,
                    CompanyId = (long)user.CodigoEmp
                };
                participantList.Add(participant);
            }

            foreach (var guest in guestList)
            {
                var participant = new AppointmentParticipantsResponse
                {
                    ParticipantId = guest.Id,
                    ParticipantTypeId = (int)AppointmentParticipant.Guest,
                    ParticipantEmail = guest.Email,
                    ParticipantName = guest.Names,
                    ParticipantPhone = guest.PhoneNumber,
                    CompanyId = guest.CompanyId
                };
                participantList.Add(participant);
            }

            return participantList.OrderBy(x => x.ParticipantName).ToList();
        }

        public async Task<List<AppointmentParticipantsResponse>> GetAllParticipants()
        {
            var contactList = await _contactRepository.GetAll();
            var userSystemList = await _userRepository.GetAll();
            var guestList = await _guestRepository.GetAll();
            var participantList = new List<AppointmentParticipantsResponse>();

            foreach (var contact in contactList)
            {
                var participant = new AppointmentParticipantsResponse
                {
                    ParticipantId = contact.Id,
                    ParticipantTypeId = (int)AppointmentParticipant.Contact,
                    ParticipantEmail = contact.ContactEmail,
                    ParticipantName = contact.Name,
                    ParticipantPhone = contact.ContactNumber,
                    CompanyId = contact.CompanyId
                };
                participantList.Add(participant);
            }

            foreach (var user in userSystemList)
            {
                var participant = new AppointmentParticipantsResponse
                {
                    ParticipantId = user.Id,
                    ParticipantTypeId = (int)AppointmentParticipant.SystemUser,
                    ParticipantEmail = user.Email,
                    ParticipantName = user.Nombre + " " + user.Apellido,
                    ParticipantPhone = user.TelefonoPersonal,
                    CompanyId = (long)user.CodigoEmp
                };
                participantList.Add(participant);
            }

            foreach (var guest in guestList)
            {
                var participant = new AppointmentParticipantsResponse
                {
                    ParticipantId = guest.Id,
                    ParticipantTypeId = (int)AppointmentParticipant.Guest,
                    ParticipantEmail = guest.Email,
                    ParticipantName = guest.Names,
                    ParticipantPhone = guest.PhoneNumber,
                    CompanyId = guest.CompanyId
                };
                participantList.Add(participant);
            }

            return participantList.OrderBy(x => x.ParticipantName).ToList();
        }

    }
}
