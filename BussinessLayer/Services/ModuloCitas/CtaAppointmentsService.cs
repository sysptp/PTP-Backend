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
using Microsoft.Extensions.Configuration;
using SendGrid.Helpers.Errors.Model;

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


        public async override Task<CtaAppointmentsResponse> Update(CtaAppointmentsRequest vm, int id)
        {
            var currentAppointment = await _appointmentRepository.GetById(id);
            bool stateChanged = currentAppointment != null && currentAppointment.IdState != vm.IdState;

            await base.Update(vm, id);

            await UpdateAppointmentParticipants(vm, id, _mapper.Map<CtaAppointmentsResponse>(vm));

            await SendAppointmentEmailsAsync(vm, vm.CompanyId, isUpdate: true, stateChanged);

            return _mapper.Map<CtaAppointmentsResponse>(vm);
        }

        public async Task UpdateAppointmentParticipants(CtaAppointmentsRequest vm, int appointmentId, CtaAppointmentsResponse appointmentEntity)
        {
            // Obtener todos los participantes actuales
            var currentParticipants = await GetAllParticipantsByAppointmentId(appointmentId);

            if (vm.AppointmentParticipants != null)
            {
                // === USUARIOS DEL SISTEMA ===
                // Filtrar participantes actuales que son usuarios del sistema
                var currentUsers = currentParticipants
                    .Where(x => x.ParticipantTypeId == (int)AppointmentParticipant.SystemUser)
                    .ToList();

                // Obtener IDs de nuevos usuarios del sistema
                var newSystemUserIds = vm.AppointmentParticipants
                    .Where(x => x.ParticipantTypeId == (int)AppointmentParticipant.SystemUser)
                    .Select(user => user.ParticipantId)
                    .ToList();

                // Identificar usuarios a eliminar (están en BD pero no en la petición)
                var userIdsToDelete = currentUsers
                    .Where(u => !newSystemUserIds.Contains(u.ParticipantId))
                    .Select(u => u.ParticipantId)
                    .ToList();

                // Identificar usuarios a agregar (están en la petición pero no en BD)
                var userIdsToAdd = newSystemUserIds
                    .Where(userId => !currentUsers.Any(u => u.ParticipantId == userId))
                    .ToList();

                // Eliminar usuarios que ya no están en la petición
                if (userIdsToDelete.Any())
                {
                    // Obtener entidades de usuario para eliminar (usando el método existente)
                    var userEntitiesToDelete = await _userAppointmentRepository.GetAllUserByAppointmentId(appointmentId);
                    // Filtrar solo los que queremos eliminar
                    userEntitiesToDelete = userEntitiesToDelete
                        .Where(u => userIdsToDelete.Contains(u.Id))
                        .ToList();

                    // Crear entidades de CtaAppointmentUsers para eliminar
                    var appointmentUsersToDelete = userEntitiesToDelete.Select(u => new CtaAppointmentUsers
                    {
                        UserId = u.Id,
                        AppointmentId = appointmentId,
                        CompanyId = appointmentEntity.CompanyId
                    }).ToList();

                    if (appointmentUsersToDelete.Any())
                        foreach (var appointmentUser in appointmentUsersToDelete)
                        {
                            await _userAppointmentRepository.DeleteByAppointmentId(appointmentUser.AppointmentId,appointmentUser.UserId);
                        }
                }

                // Agregar nuevos usuarios
                if (userIdsToAdd.Any())
                {
                    var usersToAdd = userIdsToAdd
                        .Select(userId => new CtaAppointmentUsers
                        {
                            UserId = userId,
                            AppointmentId = appointmentId,
                            CompanyId = appointmentEntity.CompanyId
                        })
                        .ToList();

                    await _userAppointmentRepository.AddRangeAsync(usersToAdd);
                }

                // === CONTACTOS ===
                // Filtrar participantes actuales que son contactos
                var currentContacts = currentParticipants
                    .Where(x => x.ParticipantTypeId == (int)AppointmentParticipant.Contact)
                    .ToList();

                // Obtener IDs de nuevos contactos
                var newContactIds = vm.AppointmentParticipants
                    .Where(x => x.ParticipantTypeId == (int)AppointmentParticipant.Contact)
                    .Select(contact => contact.ParticipantId)
                    .ToList();

                // Identificar contactos a eliminar
                var contactIdsToDelete = currentContacts
                    .Where(c => !newContactIds.Contains(c.ParticipantId))
                    .Select(c => c.ParticipantId)
                    .ToList();

                // Identificar contactos a agregar
                var contactIdsToAdd = newContactIds
                    .Where(contactId => !currentContacts.Any(c => c.ParticipantId == contactId))
                    .ToList();

                // Eliminar contactos que ya no están en la petición
                if (contactIdsToDelete.Any())
                {
                    // Obtener entidades de contacto para eliminar
                    var contactEntitiesToDelete = await _appointmentContactsRepository.GetAllContactsByAppointmentId(appointmentId);
                    // Filtrar solo los que queremos eliminar
                    contactEntitiesToDelete = contactEntitiesToDelete
                        .Where(c => contactIdsToDelete.Contains(c.Id))
                        .ToList();

                    // Crear entidades de CtaAppointmentContacts para eliminar
                    var appointmentContactsToDelete = contactEntitiesToDelete.Select(c => new CtaAppointmentContacts
                    {
                        ContactId = c.Id,
                        AppointmentId = appointmentId,
                        CompanyId = appointmentEntity.CompanyId
                    }).ToList();

                    if (appointmentContactsToDelete.Any())
                        foreach (var contact in appointmentContactsToDelete) 
                        {
                        await _appointmentContactsRepository.DeleteByAppointmentId(contact.AppointmentId,contact.ContactId);
                        }
                }

                // Agregar nuevos contactos
                if (contactIdsToAdd.Any())
                {
                    var contactsToAdd = contactIdsToAdd
                        .Select(contactId => new CtaAppointmentContacts
                        {
                            ContactId = contactId,
                            AppointmentId = appointmentId,
                            CompanyId = appointmentEntity.CompanyId
                        })
                        .ToList();

                    await _appointmentContactsRepository.AddRangeAsync(contactsToAdd);
                }

                // === INVITADOS ===
                // Filtrar participantes actuales que son invitados
                var currentGuests = currentParticipants
                    .Where(x => x.ParticipantTypeId == (int)AppointmentParticipant.Guest)
                    .ToList();

                // Obtener IDs de nuevos invitados
                var newGuestIds = vm.AppointmentParticipants
                    .Where(x => x.ParticipantTypeId == (int)AppointmentParticipant.Guest)
                    .Select(guest => guest.ParticipantId)
                    .ToList();

                // Identificar invitados a eliminar
                var guestIdsToDelete = currentGuests
                    .Where(g => !newGuestIds.Contains(g.ParticipantId))
                    .Select(g => g.ParticipantId)
                    .ToList();

                // Identificar invitados a agregar
                var guestIdsToAdd = newGuestIds
                    .Where(guestId => !currentGuests.Any(g => g.ParticipantId == guestId))
                    .ToList();

                // Eliminar invitados que ya no están en la petición
                if (guestIdsToDelete.Any())
                {
                    // Obtener entidades de invitado para eliminar
                    var guestEntitiesToDelete = await _ctaAppointmentGuestRepository.GetAllGuestByAppointmentId(appointmentId);
                    // Filtrar solo los que queremos eliminar
                    guestEntitiesToDelete = guestEntitiesToDelete
                        .Where(g => guestIdsToDelete.Contains(g.Id))
                        .ToList();

                    // Crear entidades de CtaAppointmentGuest para eliminar
                    var appointmentGuestsToDelete = guestEntitiesToDelete.Select(g => new CtaAppointmentGuest
                    {
                        GuestId = g.Id,
                        AppointmentId = appointmentId,
                        CompanyId = appointmentEntity.CompanyId
                    }).ToList();

                    if (appointmentGuestsToDelete.Any())
                        foreach (var guest in appointmentGuestsToDelete)
                        {
                        await _ctaAppointmentGuestRepository.DeleteByAppointmentId(guest.AppointmentId, guest.GuestId);
                        }
                }

                // Agregar nuevos invitados
                if (guestIdsToAdd.Any())
                {
                    var guestsToAdd = guestIdsToAdd
                        .Select(guestId => new CtaAppointmentGuest
                        {
                            GuestId = guestId,
                            AppointmentId = appointmentId,
                            CompanyId = appointmentEntity.CompanyId
                        })
                        .ToList();

                    await _ctaAppointmentGuestRepository.AddRangeAsync(guestsToAdd);
                }
            }
        }

        #region privateMethods
        private async Task SendAppointmentEmailsAsync(CtaAppointmentsRequest appointment, long companyId, bool isUpdate = false, bool stateChanged = false)
        {
            var creator = await _userRepository.GetById(appointment.AssignedUser);
            var appointmentState = await _ctaStateRepository.GetById(appointment.IdState);

            if (isUpdate && stateChanged)
            {
                // Caso 1: Actualización con cambio de estado - se envía notificación de cambio de estado
                await SendStateChangeEmailNotification(appointment, creator, appointmentState, companyId);
            }
            else
            {
                // Caso 2: Creación nueva o actualización sin cambio de estado - se envía notificación regular
                await SendRegularEmailNotification(appointment, creator, appointmentState, companyId, isUpdate);
            }

        }

        private async Task SendStateChangeEmailNotification(
    CtaAppointmentsRequest appointment,
    dynamic creator,
    dynamic appointmentState,
    long companyId)
        {
            var configStateChangeSubject = _configuration["EmailTemplates:DefaultTemplates:StateChangeTemplate:Subject"] ?? "Cambio de Estado en Cita";
            var configStateChangeBody = _configuration["EmailTemplates:DefaultTemplates:StateChangeTemplate:Body"] ??
                "<html><body><p>La cita {AppointmentCode} ha cambiado de estado de {PreviousState} a {NewState}.</p></body></html>";

            var emailTemplateForStateChange = await _ctaEmailTemplateRepository.GetById(appointmentState.EmailTemplateIdStateChange ?? 0);

            var currentAppointment = await _appointmentRepository.GetById(appointment.AppointmentId);
            var previousState = await _ctaStateRepository.GetById(currentAppointment.IdState);

            var allParticipants = await GetAllEmailsForAppointment(appointment);

            string stateChangeBody = emailTemplateForStateChange?.Body ?? configStateChangeBody;
            stateChangeBody = _ctaEmailTemplateRepository.ReplaceEmailTemplateValues(stateChangeBody, appointment);

            stateChangeBody = stateChangeBody.Replace("{PreviousState}", previousState?.Description ?? "Estado anterior")
                                           .Replace("{NewState}", appointmentState?.Description ?? "Nuevo estado");

            var emailData = new CtaEmailBackgroundJobData
            {
                CreatorEmails = new List<string> { creator.Email },
                ContactEmails = allParticipants.ContactEmails,
                UserEmails = allParticipants.UserEmails,
                GuestEmails = allParticipants.GuestEmails,
                AssignedSubject = emailTemplateForStateChange?.Subject ?? configStateChangeSubject,
                AssignedBody = stateChangeBody,
                ParticipantSubject = emailTemplateForStateChange?.Subject ?? configStateChangeSubject,
                ParticipantBody = stateChangeBody,
                CompanyId = companyId,
                IsStateChange = true,
                PreviousState = previousState?.Description,
                NewState = appointmentState?.Description
            };

            _backgroundEmailService.QueueAppointmentEmails(emailData);
        }

        private async Task SendRegularEmailNotification(
    CtaAppointmentsRequest appointment,
    dynamic creator,
    dynamic appointmentState,
    long companyId,
    bool isUpdate)
        {
            var configAssignedSubject = isUpdate
                ? _configuration["EmailTemplates:DefaultTemplates:UpdatedAppointmentTemplate:Subject"]
                : _configuration["EmailTemplates:DefaultTemplates:AssignedUserTemplate:Subject"];

            var configAssignedBody = isUpdate
                ? _configuration["EmailTemplates:DefaultTemplates:UpdatedAppointmentTemplate:Body"]
                : _configuration["EmailTemplates:DefaultTemplates:AssignedUserTemplate:Body"];

            var configParticipantSubject = isUpdate
                ? _configuration["EmailTemplates:DefaultTemplates:UpdatedParticipantTemplate:Subject"]
                : _configuration["EmailTemplates:DefaultTemplates:ParticipantTemplate:Subject"];

            var configParticipantBody = isUpdate
                ? _configuration["EmailTemplates:DefaultTemplates:UpdatedParticipantTemplate:Body"]
                : _configuration["EmailTemplates:DefaultTemplates:ParticipantTemplate:Body"];

            var emailTemplateIdToUse = isUpdate
                ? appointmentState.EmailTemplateIdUpdate
                : appointmentState.EmailTemplateIdIn;

            var emailTemplateForAssignedUser = await _ctaEmailTemplateRepository.GetById(emailTemplateIdToUse ?? 0);

            var emailTemplateIdParticipantToUse = isUpdate
                ? appointmentState.EmailTemplateIdUpdateParticipant
                : appointmentState.EmailTemplateIdOut;

            var emailTemplateForParticipant = await _ctaEmailTemplateRepository.GetById(emailTemplateIdParticipantToUse ?? 0);

            var allParticipantEmails = await GetAllEmailsForAppointment(appointment);

            string assignedBody = emailTemplateForAssignedUser?.Body ?? configAssignedBody;
            string participantBody = emailTemplateForParticipant?.Body ?? configParticipantBody;

            assignedBody = _ctaEmailTemplateRepository.ReplaceEmailTemplateValues(assignedBody, appointment);
            participantBody = _ctaEmailTemplateRepository.ReplaceEmailTemplateValues(participantBody, appointment);

            var emailData = new CtaEmailBackgroundJobData
            {
                CreatorEmails = new List<string> { creator.Email },
                ContactEmails = allParticipantEmails.ContactEmails,
                UserEmails = allParticipantEmails.UserEmails,
                GuestEmails = allParticipantEmails.GuestEmails,
                AssignedSubject = emailTemplateForAssignedUser?.Subject ?? configAssignedSubject,
                AssignedBody = assignedBody,
                ParticipantSubject = emailTemplateForParticipant?.Subject ?? configParticipantSubject,
                ParticipantBody = participantBody,
                CompanyId = companyId,
                IsUpdate = isUpdate
            };

            _backgroundEmailService.QueueAppointmentEmails(emailData);
        }

        private async Task<(List<string> ContactEmails, List<string> UserEmails, List<string> GuestEmails)>
            GetAllEmailsForAppointment(CtaAppointmentsRequest appointment)
        {
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
                    g.ParticipantTypeId == (int)AppointmentParticipant.Guest && x.Id == g.ParticipantId)?.Email)
                .Where(email => !string.IsNullOrEmpty(email)) ?? new List<string>()).ToList();

            return (contactEmails, userEmails, guestEmails);
        }

        #endregion

        public async Task<DetailMessage> ExistsAppointmentInTimeRange(CtaAppointmentsRequest appointmentDto)
        {
            var existingAppointments = await _appointmentRepository.GetAppointmentsByDate(appointmentDto.AppointmentDate, appointmentDto.CompanyId, appointmentDto.AssignedUser);

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
            var existingAppointments = await _appointmentRepository.GetAppointmentsByDate(appointmentDto.AppointmentDate, appointmentDto.CompanyId, appointmentDto.AssignedUser);

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
