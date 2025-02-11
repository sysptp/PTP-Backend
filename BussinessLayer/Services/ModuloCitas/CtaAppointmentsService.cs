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
            var creator = await _userRepository.GetById(appointment.UserId);
            var contacts = appointment.CtaAppointmentContacts?.co.Select(c => c.co).ToList() ?? new List<string>();
            var users = appointment.CtaAppointmentUsers?.Select(u => u.Email).ToList() ?? new List<string>();
            var guests = appointment.CtaAppointvmentsRequest?.Select(g => g.Email).ToList() ?? new List<string>();

            if (contacts.Any())
            {
                await SendEmailAsync(contacts, "Usted ha creado una cita", "Detalles de la cita...", companyId);
            }

            if (users.Any())
            {
                await SendEmailAsync(users, "Se requiere su asistencia a una cita", "Detalles de la cita...", companyId);
            }

            if (guests.Any())
            {
                await SendEmailAsync(guests, "Usted fue invitado a una cita", "Detalles de la cita...", companyId);
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

            await _gnE.SendAsync(emailMessage, companyId);
        }

    }
}
