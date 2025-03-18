using AutoMapper;
using BussinessLayer.DTOs.ModuloCitas.CtaAppointments;
using BussinessLayer.DTOs.ModuloGeneral.Email;
using BussinessLayer.Enums;
using BussinessLayer.Interface.Repository.ModuloCitas;
using BussinessLayer.Interfaces.Repository.ModuloCitas;
using BussinessLayer.Interfaces.Repository.ModuloGeneral.Seguridad;
using BussinessLayer.Interfaces.Services.ModuloCitas;
using BussinessLayer.Interfaces.Services.ModuloGeneral.Email;
using BussinessLayer.Services;
using BussinessLayer.Wrappers;
using DataLayer.Models.ModuloCitas;

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
        private readonly IMapper _mapper;

        public CtaAppointmentsService(ICtaAppointmentsRepository appointmentRepository,
            IGnEmailService gnEmailService, IUsuarioRepository userRepository,
            ICtaContactRepository contactRepository,
            ICtaGuestRepository guestRepository, IMapper mapper, ICtaAppointmentSequenceService appointmentSequenceService, ICtaAppointmentUsersRepository userUsersRepository, ICtaAppointmentGuestRepository ctaAppointmentGuestRepository, ICtaAppointmentContactsRepository appointmentContactsRepository, ICtaEmailTemplatesRepository ctaEmailTemplateRepository, ICtaAppointmentAreaRepository ctaAppointmentAreaRepository) : base(appointmentRepository, mapper)
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
        }

        public override async Task<CtaAppointmentsResponse> Add(CtaAppointmentsRequest vm)
        {
            var nextSequence = await _sequenceService.GetFormattedSequenceAsync(vm.CompanyId, vm.AreaId);
            vm.AppointmentCode = nextSequence;

            await _sequenceService.UpdateSequenceAsync(vm.CompanyId, vm.AreaId);

            var appointmentEntity = await base.Add(vm);

            var appointmentId = appointmentEntity.AppointmentId;

            await AddAppointmentParticipants(vm, appointmentId, appointmentEntity);

            await SendAppointmentEmailsAsync(vm, vm.CompanyId);

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
                "CtaAppointmentContacts",
                "CtaAppointmentUsers",
                "CtaAppointmentManagement",
                 "CtaAppointmentContacts.Contact",
                 "CtaAppointmentUsers.Usuario",
                 "CtaAppointmentGuest.Guest"});

                var appointmentDtoList = _mapper.Map<List<CtaAppointmentsResponse>>(appointments);

                var tasks = appointmentDtoList.Select(async appointmentDto =>
                {
                    var area = await _ctaAppointmentAreaRepository.GetById(appointmentDto.AreaId);
                    appointmentDto.AreaId = area?.AreaId;
                });

                return appointmentDtoList;
            }
            catch (AutoMapperMappingException ex)
            {
                throw new Exception($"Error en el mapeo: {ex.Message}, InnerException: {ex.InnerException?.Message}");
            }
        }


        // 3. Modifica el método SendAppointmentEmailsAsync en tu servicio de Appointments
        private async Task SendAppointmentEmailsAsync(CtaAppointmentsRequest appointment, long companyId)
        {
            // Captura los datos necesarios antes de iniciar la tarea en segundo plano
            // para evitar usar el DbContext fuera de su alcance
            var creator = await _userRepository.GetById(appointment.UserId);
            var emailTemplateForAssignedUser = await _ctaEmailTemplateRepository.GetEmailTemplateByFilters(companyId);
            var emailTemplateForParticipant = await _ctaEmailTemplateRepository.GetEmailTemplateByFilters(companyId);

            // Copia todos los datos necesarios para evitar acceder al DbContext dentro de Task.Run
            var allContacts = await _contactRepository.GetAll();
            var allUsers = await _userRepository.GetAll();
            var allGuests = await _guestRepository.GetAll();

            // Prepara las listas de correos electrónicos
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

            // Almacena toda la información necesaria para los correos
            var creatorEmail = creator.Email;
            var assignedSubject = emailTemplateForAssignedUser.Subject;
            var assignedBody = emailTemplateForAssignedUser.Body;
            var participantSubject = emailTemplateForParticipant.Subject;
            var participantBody = emailTemplateForParticipant.Body;

           
                try
                {
                    var emailTasks = new List<Task>();

                    // Envía correos con los datos ya capturados
                    emailTasks.Add(SendEmailAsync(new List<string> { creatorEmail },
                        assignedSubject, assignedBody, companyId));

                    if (contactEmails.Any())
                    {
                        emailTasks.Add(SendEmailAsync(contactEmails,
                            participantSubject, participantBody, companyId));
                    }

                    if (userEmails.Any())
                    {
                        emailTasks.Add(SendEmailAsync(userEmails,
                            participantSubject, participantBody, companyId));
                    }

                    if (guestEmails.Any())
                    {
                        emailTasks.Add(SendEmailAsync(guestEmails,
                            participantSubject, participantBody, companyId));
                    }

                    await Task.WhenAll(emailTasks);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message, ex);
                }
        }


        private async Task SendEmailAsync(List<string> recipients, string subject, string body, long companyId)
        {
            var emailMessage = new GnEmailMessageDto
            {
                To = recipients,
                Subject = subject,
                Body = body,
                IsHtml = true,
                Attachments = null,
                EmpresaId = companyId,
            };

            await _gnEmailService.SendAsync(emailMessage, companyId);
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
