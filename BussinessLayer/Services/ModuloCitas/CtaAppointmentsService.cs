using AutoMapper;
using BussinessLayer.DTOs.ModuloCitas;
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
        private readonly ICtaUnifiedNotificationService _notificationService;
        private readonly ICtaMeetingPlaceRepository _meetingPlaceRepository;
        private readonly ICtaAppointmentReasonRepository _appointmentReasonRepository;


        public CtaAppointmentsService(ICtaAppointmentsRepository appointmentRepository,
            IGnEmailService gnEmailService, IUsuarioRepository userRepository,
            ICtaContactRepository contactRepository,
            ICtaGuestRepository guestRepository, IMapper mapper, ICtaAppointmentSequenceService appointmentSequenceService,
            ICtaAppointmentUsersRepository userUsersRepository,
            ICtaAppointmentGuestRepository ctaAppointmentGuestRepository, ICtaAppointmentContactsRepository appointmentContactsRepository,
            ICtaEmailTemplatesRepository ctaEmailTemplateRepository, ICtaAppointmentAreaRepository ctaAppointmentAreaRepository,
            ICtaBackgroundEmailService backgroundEmailService, ICtaStateRepository ctaStateRepository, IConfiguration configuration, ICtaUnifiedNotificationService notificationService, ICtaAppointmentReasonRepository appointmentReasonRepository, ICtaMeetingPlaceRepository meetingPlaceRepository) : base(appointmentRepository, mapper)
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
            _notificationService = notificationService;
            _appointmentReasonRepository = appointmentReasonRepository;
            _meetingPlaceRepository = meetingPlaceRepository;
        }

        public async Task<CtaAppointmentsResponse> AddAppointment(CtaAppointmentsRequest vm, bool IsForSession)
        {
            var nextSequence = await _sequenceService.GetFormattedSequenceAsync(vm.CompanyId, vm.AreaId);
            vm.AppointmentCode = nextSequence;

            var appointmentEntity = await base.Add(vm);
            await _sequenceService.UpdateSequenceAsync(vm.CompanyId, vm.AreaId);

            var appointmentId = appointmentEntity.AppointmentId;
            await AddAppointmentParticipants(vm, appointmentId, appointmentEntity);

            if (!IsForSession)
            {
                // Notificación para el usuario asignado
                var userContext = await CreateNotificationContext(vm, NotificationType.CreationForUser);
                await _notificationService.SendNotificationsForAppointmentAsync(vm, NotificationType.CreationForUser, userContext);

                // Notificación para los participantes
                var participantsContext = await CreateNotificationContext(vm, NotificationType.CreationForParticipant);
                await _notificationService.SendNotificationsForAppointmentAsync(vm, NotificationType.CreationForParticipant, participantsContext);
            }

            return _mapper.Map<CtaAppointmentsResponse>(appointmentEntity);
        }

        public override async Task<CtaAppointmentsResponse> Update(CtaAppointmentsRequest vm, int id)
        {
            var currentAppointment = await _appointmentRepository.GetById(id);
            bool stateChanged = currentAppointment != null && currentAppointment.IdState != vm.IdState;

            await base.Update(vm, id);
            await UpdateAppointmentParticipants(vm, id, _mapper.Map<CtaAppointmentsResponse>(vm));

            if (stateChanged)
            {
                // Notificación de cambio de estado para todos
                var stateChangeContext = await CreateNotificationContext(vm, NotificationType.StateChangeForUser);
                stateChangeContext.PreviousState = (await _ctaStateRepository.GetById(currentAppointment.IdState))?.Description;
                stateChangeContext.NewState = (await _ctaStateRepository.GetById(vm.IdState))?.Description;
                await _notificationService.SendNotificationsForAppointmentAsync(vm, NotificationType.StateChangeForParticipant, stateChangeContext);
            }
            else
            {
                // Notificación de actualización para el usuario asignado
                var userUpdateContext = await CreateNotificationContext(vm, NotificationType.UpdateForUser);
                await _notificationService.SendNotificationsForAppointmentAsync(vm, NotificationType.UpdateForUser, userUpdateContext);

                // Notificación de actualización para los participantes
                var participantsUpdateContext = await CreateNotificationContext(vm, NotificationType.UpdateForParticipant);
                await _notificationService.SendNotificationsForAppointmentAsync(vm, NotificationType.UpdateForParticipant, participantsUpdateContext);
            }

            return _mapper.Map<CtaAppointmentsResponse>(vm);
        }

        private async Task<NotificationContext> CreateNotificationContext(CtaAppointmentsRequest appointment, NotificationType notificationType)
        {
            var context = new NotificationContext();

            // Obtener información del usuario asignado
            var assignedUser = await _userRepository.GetById(appointment.AssignedUser);
            context.AssignedUserName = $"{assignedUser?.Nombre} {assignedUser?.Apellido}";

            // Obtener información del lugar de reunión
            var meetingPlace = await _meetingPlaceRepository.GetById(appointment.IdPlaceAppointment);
            context.MeetingPlaceDescription = meetingPlace?.Description;

            // Obtener información del motivo
            var reason = await _appointmentReasonRepository.GetById(appointment.IdReasonAppointment);
            context.ReasonDescription = reason?.Description;

            // Obtener información del área
            var area = await _ctaAppointmentAreaRepository.GetById(appointment.AreaId);
            context.AreaDescription = area?.Description;

            // Obtener correos y teléfonos según el tipo de notificación
            await PopulateContactsByNotificationType(appointment, context, notificationType);

            return context;
        }
        private async Task PopulateContactsByNotificationType(CtaAppointmentsRequest appointment, NotificationContext context, NotificationType notificationType)
        {
            var allContacts = await _contactRepository.GetAll();
            var allUsers = await _userRepository.GetAll();
            var allGuests = await _guestRepository.GetAll();

            if (notificationType == NotificationType.CreationForUser || notificationType == NotificationType.UpdateForUser)
            {
                // Solo incluir al usuario asignado
                var assignedUser = await _userRepository.GetById(appointment.AssignedUser);
                if (assignedUser != null)
                {
                    if (!string.IsNullOrEmpty(assignedUser.Email))
                        context.RecipientEmails.Add(assignedUser.Email);
                    if (!string.IsNullOrEmpty(assignedUser.PhoneNumber))
                        context.RecipientPhoneNumbers.Add(assignedUser.PhoneNumber);
                }
            }
            else if (notificationType == NotificationType.CreationForParticipant || notificationType == NotificationType.UpdateForParticipant)
            {
                // Solo incluir a los participantes, sin el usuario asignado
                if (appointment.AppointmentParticipants != null)
                {
                    foreach (var participant in appointment.AppointmentParticipants)
                    {
                        switch (participant.ParticipantTypeId)
                        {
                            case (int)AppointmentParticipant.Contact:
                                var contact = allContacts.FirstOrDefault(c => c.Id == participant.ParticipantId);
                                if (contact != null)
                                {
                                    if (!string.IsNullOrEmpty(contact.ContactEmail))
                                        context.RecipientEmails.Add(contact.ContactEmail);
                                    if (!string.IsNullOrEmpty(contact.ContactNumber))
                                        context.RecipientPhoneNumbers.Add(contact.ContactNumber);
                                }
                                break;

                            case (int)AppointmentParticipant.SystemUser:
                                var user = allUsers.FirstOrDefault(u => u.Id == participant.ParticipantId);
                                if (user != null && user.Id != appointment.AssignedUser) 
                                {
                                    if (!string.IsNullOrEmpty(user.Email))
                                        context.RecipientEmails.Add(user.Email);
                                    if (!string.IsNullOrEmpty(user.PhoneNumber))
                                        context.RecipientPhoneNumbers.Add(user.PhoneNumber);
                                }
                                break;

                            case (int)AppointmentParticipant.Guest:
                                var guest = allGuests.FirstOrDefault(g => g.Id == participant.ParticipantId);
                                if (guest != null)
                                {
                                    if (!string.IsNullOrEmpty(guest.Email))
                                        context.RecipientEmails.Add(guest.Email);
                                    if (!string.IsNullOrEmpty(guest.PhoneNumber))
                                        context.RecipientPhoneNumbers.Add(guest.PhoneNumber);
                                }
                                break;
                        }
                    }
                }
            }
            else
            {
                // Para los demás tipos, incluir todos (comportamiento actual)
                var assignedUser = await _userRepository.GetById(appointment.AssignedUser);
                if (assignedUser != null)
                {
                    if (!string.IsNullOrEmpty(assignedUser.Email))
                        context.RecipientEmails.Add(assignedUser.Email);
                    if (!string.IsNullOrEmpty(assignedUser.PhoneNumber))
                        context.RecipientPhoneNumbers.Add(assignedUser.PhoneNumber);
                }

                if (appointment.AppointmentParticipants != null)
                {
                    foreach (var participant in appointment.AppointmentParticipants)
                    {
                        switch (participant.ParticipantTypeId)
                        {
                            case (int)AppointmentParticipant.Contact:
                                var contact = allContacts.FirstOrDefault(c => c.Id == participant.ParticipantId);
                                if (contact != null)
                                {
                                    if (!string.IsNullOrEmpty(contact.ContactEmail))
                                        context.RecipientEmails.Add(contact.ContactEmail);
                                    if (!string.IsNullOrEmpty(contact.ContactNumber))
                                        context.RecipientPhoneNumbers.Add(contact.ContactNumber);
                                }
                                break;

                            case (int)AppointmentParticipant.SystemUser:
                                var user = allUsers.FirstOrDefault(u => u.Id == participant.ParticipantId);
                                if (user != null && user.Id != appointment.AssignedUser) 
                                {
                                    if (!string.IsNullOrEmpty(user.Email))
                                        context.RecipientEmails.Add(user.Email);
                                    if (!string.IsNullOrEmpty(user.PhoneNumber))
                                        context.RecipientPhoneNumbers.Add(user.PhoneNumber);
                                }
                                break;

                            case (int)AppointmentParticipant.Guest:
                                var guest = allGuests.FirstOrDefault(g => g.Id == participant.ParticipantId);
                                if (guest != null)
                                {
                                    if (!string.IsNullOrEmpty(guest.Email))
                                        context.RecipientEmails.Add(guest.Email);
                                    if (!string.IsNullOrEmpty(guest.PhoneNumber))
                                        context.RecipientPhoneNumbers.Add(guest.PhoneNumber);
                                }
                                break;
                        }
                    }
                }
            }
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
                "CtaAppointmentManagement",
                "Usuario"});

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
