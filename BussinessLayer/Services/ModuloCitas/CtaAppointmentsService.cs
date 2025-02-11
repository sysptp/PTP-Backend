using AutoMapper;
using BussinessLayer.DTOs.ModuloCitas.CtaAppointments;
using BussinessLayer.DTOs.ModuloGeneral.Email;
using BussinessLayer.Interface.Repository.ModuloCitas;
using BussinessLayer.Interfaces.Repository.ModuloCitas;
using BussinessLayer.Interfaces.Repository.ModuloGeneral.Seguridad;
using BussinessLayer.Interfaces.Services.ModuloCitas;
using BussinessLayer.Interfaces.Services.ModuloGeneral.Email;
using BussinessLayer.Services;
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
        private readonly IMapper _mapper;

        public CtaAppointmentsService(ICtaAppointmentsRepository appointmentRepository,
            IGnEmailService gnEmailService, IUsuarioRepository userRepository,
            ICtaContactRepository contactRepository,
            ICtaGuestRepository guestRepository, IMapper mapper, ICtaAppointmentSequenceService appointmentSequenceService, ICtaAppointmentUsersRepository userUsersRepository, ICtaAppointmentGuestRepository ctaAppointmentGuestRepository, ICtaAppointmentContactsRepository appointmentContactsRepository) : base(appointmentRepository, mapper)
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
        }

        public override async Task<CtaAppointmentsResponse> Add(CtaAppointmentsRequest vm)
        {
            var nextSequence = await _sequenceService.GetFormattedSequenceAsync(vm.CompanyId, vm.AreaId);
            vm.AppointmentCode = nextSequence;

            await _sequenceService.UpdateSequenceAsync(vm.CompanyId, vm.AreaId);

            var appointmentEntity = await base.Add(vm);

            var appointmentId = appointmentEntity.AppointmentId;

            if (vm.CtaAppointmentUsers != null && vm.CtaAppointmentUsers.Any())
            {
                var usersToInsert = vm.CtaAppointmentUsers.Select(user => new CtaAppointmentUsers
                {
                    UserId = user.UserId,
                    AppointmentId = appointmentId,
                    CompanyId = appointmentEntity.CompanyId
                }).ToList();

                await _userAppointmentRepository.AddRangeAsync(usersToInsert);
            }

            if (vm.CtaAppointmentContacts != null && vm.CtaAppointmentContacts.Any())
            {
                var contactsToInsert = vm.CtaAppointmentContacts.Select(contact => new CtaAppointmentContacts
                {
                    ContactId = contact.ContactId,
                    AppointmentId = appointmentId,
                    CompanyId = appointmentEntity.CompanyId
                }).ToList();

                await _appointmentContactsRepository.AddRangeAsync(contactsToInsert);
            }

            if (vm.CtaAppointmentGuests != null && vm.CtaAppointmentGuests.Any())
            {
                var guestToInsert = vm.CtaAppointmentGuests.Select(guest => new CtaAppointmentGuest
                {
                    Id = guest.Id,
                    AppointmentId = appointmentId,
                    CompanyId = appointmentEntity.CompanyId,
                    GuestId = guest.GuestId
                }).ToList();

                await _ctaAppointmentGuestRepository.AddRangeAsync(guestToInsert);
            }

            await SendAppointmentEmailsAsync(vm, vm.CompanyId);

            return _mapper.Map<CtaAppointmentsResponse>(appointmentEntity);
        }

        public override async Task<List<CtaAppointmentsResponse>> GetAllDto()
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

            return appointmentDtoList;
        }
        private async Task SendAppointmentEmailsAsync(CtaAppointmentsRequest appointment, long companyId)
        {
            _ = Task.Run(async () =>
            {
                var creator = await _userRepository.GetById(appointment.UserId);

                var allContacts = await _contactRepository.GetAll();
                var allUsers = await _userRepository.GetAll();
                var allGuests = await _guestRepository.GetAll();

                var contactEmails = (appointment.CtaAppointmentContacts?
                    .Select(c => allContacts.FirstOrDefault(x => x.Id == c.ContactId)?.ContactEmail)
                    .Where(email => !string.IsNullOrEmpty(email)) ?? new List<string>()).ToList();

                var userEmails = (appointment.CtaAppointmentUsers?
                    .Select(u => allUsers.FirstOrDefault(x => x.Id == u.UserId)?.Email)
                    .Where(email => !string.IsNullOrEmpty(email)) ?? new List<string>()).ToList();

                var guestEmails = (appointment.CtaAppointmentGuests?
                    .Select(g => allGuests.FirstOrDefault(x => x.Id == g.GuestId)?.Email)
                    .Where(email => !string.IsNullOrEmpty(email)) ?? new List<string>()).ToList();

                var emailTasks = new List<Task>();

                if (contactEmails.Any())
                {
                    emailTasks.Add(SendEmailAsync(contactEmails, "Usted ha creado una cita", "Detalles de la cita...", companyId));
                }

                if (userEmails.Any())
                {
                    emailTasks.Add(SendEmailAsync(userEmails, "Se requiere su asistencia a una cita", "Detalles de la cita...", companyId));
                }

                if (guestEmails.Any())
                {
                    emailTasks.Add(SendEmailAsync(guestEmails, "Usted fue invitado a una cita", "Detalles de la cita...", companyId));
                }

                await Task.WhenAll(emailTasks);
            });
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


    }
}
