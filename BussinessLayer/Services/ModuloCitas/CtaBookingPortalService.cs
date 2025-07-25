using AutoMapper;
using BussinessLayer.DTOs.ModuloCitas.BookingPortal;
using BussinessLayer.DTOs.ModuloCitas.CtaAppointments;
using BussinessLayer.DTOs.ModuloCitas.CtaGuest;
using BussinessLayer.Enums;
using BussinessLayer.Interfaces.Repository.ModuloCitas;
using BussinessLayer.Interfaces.Services.ModuloCitas;
using DataLayer.Models.ModuloCitas;
using Microsoft.Extensions.Configuration;
using System.Text.Json;

namespace BussinessLayer.Services.ModuloCitas
{
    public class CtaBookingPortalService : ICtaBookingPortalService
    {
        private readonly ICtaBookingPortalConfigRepository _portalRepository;
        private readonly ICtaBookingPortalUsersRepository _portalUsersRepository;
        private readonly ICtaBookingPortalAreasRepository _portalAreasRepository;
        private readonly ICtaContactRepository _contactRepository;
        private readonly ICtaGuestRepository _guestRepository;
        private readonly ICtaAppointmentsService _appointmentService;
        private readonly ICtaContactService _contactService;
        private readonly ICtaGuestService _guestService;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public CtaBookingPortalService(
            ICtaBookingPortalConfigRepository portalRepository,
            ICtaBookingPortalUsersRepository portalUsersRepository,
            ICtaBookingPortalAreasRepository portalAreasRepository,
            ICtaContactRepository contactRepository,
            ICtaGuestRepository guestRepository,
            ICtaAppointmentsService appointmentService,
            ICtaContactService contactService,
            ICtaGuestService guestService,
            IMapper mapper,
            IConfiguration configuration)
        {
            _portalRepository = portalRepository;
            _portalUsersRepository = portalUsersRepository;
            _portalAreasRepository = portalAreasRepository;
            _contactRepository = contactRepository;
            _guestRepository = guestRepository;
            _appointmentService = appointmentService;
            _contactService = contactService;
            _guestService = guestService;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<BookingPortalConfigResponse> CreatePortalAsync(BookingPortalConfigRequest request)
        {
            if (string.IsNullOrEmpty(request.CustomSlug))
            {
                request.CustomSlug = await GenerateUniqueSlugAsync(request.PortalName);
            }
            else
            {
                if (await _portalRepository.SlugExistsAsync(request.CustomSlug))
                {
                    throw new InvalidOperationException($"El slug '{request.CustomSlug}' ya está en uso.");
                }
            }

            var entity = _mapper.Map<CtaBookingPortalConfig>(request);
            entity.AvailableDaysJson = request.AvailableDays != null ?
                JsonSerializer.Serialize(request.AvailableDays) : null;

            var created = await _portalRepository.Add(entity);

            // Crear relaciones Many-to-Many para usuarios
            if (request.AssignedUserIds != null && request.AssignedUserIds.Any())
            {
                var portalUsers = request.AssignedUserIds.Select(userId => new CtaBookingPortalUsers
                {
                    PortalId = created.Id,
                    UserId = userId,
                    IsMainAssignee = userId == request.MainAssigneeUserId
                }).ToList();

                foreach (var portalUser in portalUsers)
                {
                    await _portalUsersRepository.Add(portalUser);
                }
            }

            // Crear relaciones Many-to-Many para áreas
            if (request.AreaIds != null && request.AreaIds.Any())
            {
                var portalAreas = request.AreaIds.Select(areaId => new CtaBookingPortalAreas
                {
                    PortalId = created.Id,
                    AreaId = areaId,
                    IsDefault = areaId == request.DefaultAreaId
                }).ToList();

                foreach (var portalArea in portalAreas)
                {
                    await _portalAreasRepository.Add(portalArea);
                }
            }

            var response = await GetPortalByIdAsync(created.Id);
            return response!;
        }

        public async Task<BookingPortalConfigResponse> UpdatePortalAsync(int id, BookingPortalConfigRequest request)
        {
            var existingPortal = await _portalRepository.GetById(id);
            if (existingPortal == null)
            {
                throw new InvalidOperationException("Portal no encontrado");
            }

            if (!string.IsNullOrEmpty(request.CustomSlug) && request.CustomSlug != existingPortal.CustomSlug)
            {
                if (await _portalRepository.SlugExistsAsync(request.CustomSlug, id))
                {
                    throw new InvalidOperationException($"El slug '{request.CustomSlug}' ya está en uso.");
                }
            }

            _mapper.Map(request, existingPortal);
            existingPortal.AvailableDaysJson = request.AvailableDays != null ?
                JsonSerializer.Serialize(request.AvailableDays) : null;

            await _portalRepository.Update(existingPortal, id);

            // Actualizar relaciones Many-to-Many para usuarios
            await _portalUsersRepository.DeleteByPortalIdAsync(id);
            if (request.AssignedUserIds != null && request.AssignedUserIds.Any())
            {
                var portalUsers = request.AssignedUserIds.Select(userId => new CtaBookingPortalUsers
                {
                    PortalId = id,
                    UserId = userId,
                    IsMainAssignee = userId == request.MainAssigneeUserId
                }).ToList();

                foreach (var portalUser in portalUsers)
                {
                    await _portalUsersRepository.Add(portalUser);
                }
            }

            // Actualizar relaciones Many-to-Many para áreas
            await _portalAreasRepository.DeleteByPortalIdAsync(id);
            if (request.AreaIds != null && request.AreaIds.Any())
            {
                var portalAreas = request.AreaIds.Select(areaId => new CtaBookingPortalAreas
                {
                    PortalId = id,
                    AreaId = areaId,
                    IsDefault = areaId == request.DefaultAreaId
                }).ToList();

                foreach (var portalArea in portalAreas)
                {
                    await _portalAreasRepository.Add(portalArea);
                }
            }

            var response = await GetPortalByIdAsync(id);
            return response!;
        }

        public async Task DeletePortalAsync(int id)
        {
            var existingPortal = await _portalRepository.GetById(id);
            if (existingPortal == null)
            {
                throw new InvalidOperationException("Portal no encontrado");
            }

            // Eliminar relaciones Many-to-Many
            await _portalUsersRepository.DeleteByPortalIdAsync(id);
            await _portalAreasRepository.DeleteByPortalIdAsync(id);

            // Eliminar el portal principal
            await _portalRepository.Delete(id);
        }

        public async Task<BookingPortalConfigResponse?> GetPortalByIdAsync(int id)
        {
            var portal = await _portalRepository.GetById(id);
            if (portal == null) return null;

            var response = _mapper.Map<BookingPortalConfigResponse>(portal);

            // Cargar usuarios asignados
            var portalUsers = await _portalUsersRepository.GetByPortalIdAsync(id);
            response.AssignedUsers = portalUsers.Select(pu => new BookingPortalUserResponse
            {
                Id = pu.Id,
                PortalId = pu.PortalId,
                UserId = pu.UserId,
                UserName = pu.User?.Nombre + " " + pu.User?.Apellido,
                UserEmail = pu.User?.Email,
                IsMainAssignee = pu.IsMainAssignee
            }).ToList();

            // Cargar áreas asignadas
            var portalAreas = await _portalAreasRepository.GetByPortalIdAsync(id);
            response.Areas = portalAreas.Select(pa => new BookingPortalAreaResponse
            {
                Id = pa.Id,
                PortalId = pa.PortalId,
                AreaId = pa.AreaId,
                AreaName = pa.Area?.Description,
                AreaDescription = pa.Area?.Description,
                IsDefault = pa.IsDefault
            }).ToList();

            return response;
        }

        public async Task<BookingPortalConfigResponse?> GetPortalBySlugAsync(string slug)
        {
            var portal = await _portalRepository.GetBySlugAsync(slug);
            if (portal == null) return null;

            return await GetPortalByIdAsync(portal.Id);
        }

        public async Task<List<BookingPortalConfigResponse>> GetPortalsByCompanyAsync(long companyId)
        {
            var portals = await _portalRepository.GetActivePortalsByCompanyAsync(companyId);
            var responses = new List<BookingPortalConfigResponse>();

            foreach (var portal in portals)
            {
                var response = await GetPortalByIdAsync(portal.Id);
                if (response != null)
                {
                    responses.Add(response);
                }
            }

            return responses;
        }

        public async Task<ClientAuthenticationResponse> AuthenticateClientAsync(ClientAuthenticationRequest request)
        {
            var portal = await _portalRepository.GetBySlugAsync(request.PortalSlug);
            if (portal == null)
            {
                return new ClientAuthenticationResponse
                {
                    IsAuthenticated = false,
                    Message = "Portal no encontrado"
                };
            }

            if (!portal.RequireAuthentication)
            {
                return new ClientAuthenticationResponse
                {
                    IsAuthenticated = true,
                    IsNewClient = false,
                    Message = "Este portal no requiere autenticación",
                    AuthToken = null
                };
            }

            var contacts = await _contactRepository.GetAll();
            var contact = contacts.FirstOrDefault(c =>
                c.ContactNumber == request.PhoneNumber &&
                c.CompanyId == request.CompanyId &&
                !c.Borrado);

            if (contact != null)
            {
                return new ClientAuthenticationResponse
                {
                    IsAuthenticated = true,
                    IsNewClient = false,
                    ClientName = contact.Name,
                    ClientEmail = contact.ContactEmail,
                    ClientId = contact.Id,
                    ClientType = nameof(AppointmentParticipant.Contact),
                    AuthToken = GenerateAuthToken(contact.Id, (int)AppointmentParticipant.Contact, portal.Id),
                    Message = "Cliente autenticado exitosamente"
                };
            }

            var guests = await _guestRepository.GetAll();
            var guest = guests.FirstOrDefault(g =>
                g.PhoneNumber == request.PhoneNumber &&
                g.CompanyId == request.CompanyId &&
                !g.Borrado);

            if (guest != null)
            {
                return new ClientAuthenticationResponse
                {
                    IsAuthenticated = true,
                    IsNewClient = false,
                    ClientName = $"{guest.Names} {guest.LastName}",
                    ClientEmail = guest.Email,
                    ClientId = guest.Id,
                    ClientType = nameof(AppointmentParticipant.Guest),
                    AuthToken = GenerateAuthToken(guest.Id, (int)AppointmentParticipant.Guest, portal.Id),
                    Message = "Cliente autenticado exitosamente"
                };
            }

            return new ClientAuthenticationResponse
            {
                IsAuthenticated = false,
                IsNewClient = true,
                Message = "Número no registrado. Se requiere completar información."
            };
        }

        public async Task<AvailableSlotResponse> GetAvailableSlotsAsync(AvailableSlotRequest request)
        {
            var portal = await _portalRepository.GetBySlugAsync(request.PortalSlug);
            if (portal == null)
            {
                throw new InvalidOperationException("Portal no encontrado");
            }

            if (portal.RequireAuthentication && string.IsNullOrEmpty(request.AuthToken))
            {
                throw new UnauthorizedAccessException("Se requiere autenticación");
            }

            var response = new AvailableSlotResponse
            {
                Date = request.Date,
                AvailableSlots = new List<TimeSlot>()
            };

            var availableDays = !string.IsNullOrEmpty(portal.AvailableDaysJson) ?
                JsonSerializer.Deserialize<List<int>>(portal.AvailableDaysJson) : null;

            if (availableDays != null && !availableDays.Contains((int)request.Date.DayOfWeek))
            {
                return response;
            }

            var startTime = portal.StartTime ?? new TimeSpan(9, 0, 0);
            var endTime = portal.EndTime ?? new TimeSpan(17, 0, 0);
            var duration = portal.DefaultAppointmentDuration ?? new TimeSpan(1, 0, 0);

            // Obtener usuario principal del portal
            var mainAssignee = await _portalUsersRepository.GetMainAssigneeByPortalIdAsync(portal.Id);
            int assignedUserId = mainAssignee?.UserId ?? 0;

            var existingAppointments = await GetExistingAppointmentsForDate(request.Date, assignedUserId, portal.CompanyId);

            var currentTime = startTime;
            while (currentTime.Add(duration) <= endTime)
            {
                var slotEndTime = currentTime.Add(duration);
                var isConflict = existingAppointments.Any(apt =>
                    (currentTime >= apt.AppointmentTime && currentTime < apt.EndAppointmentTime) ||
                    (slotEndTime > apt.AppointmentTime && slotEndTime <= apt.EndAppointmentTime) ||
                    (currentTime <= apt.AppointmentTime && slotEndTime >= apt.EndAppointmentTime));

                response.AvailableSlots.Add(new TimeSlot
                {
                    StartTime = currentTime,
                    EndTime = slotEndTime,
                    IsAvailable = !isConflict,
                    ReasonUnavailable = isConflict ? "Horario ocupado" : null
                });

                currentTime = currentTime.Add(duration);
            }

            return response;
        }

        public async Task<PublicAppointmentResponse> CreatePublicAppointmentAsync(PublicAppointmentRequest request)
        {
            var portal = await _portalRepository.GetBySlugAsync(request.PortalSlug);
            if (portal == null)
            {
                return new PublicAppointmentResponse
                {
                    Success = false,
                    ErrorMessage = "Portal no encontrado"
                };
            }

            int participantId;
            int participantTypeId;

            if (portal.RequireAuthentication)
            {
                if (string.IsNullOrEmpty(request.AuthToken))
                {
                    return new PublicAppointmentResponse
                    {
                        Success = false,
                        ErrorMessage = "Se requiere autenticación"
                    };
                }

                var clientInfo = ValidateAuthToken(request.AuthToken);
                participantId = clientInfo.ClientId;
                participantTypeId = clientInfo.ParticipantTypeId;
            }
            else
            {
                if (string.IsNullOrEmpty(request.ClientName) || string.IsNullOrEmpty(request.ClientPhone))
                {
                    return new PublicAppointmentResponse
                    {
                        Success = false,
                        ErrorMessage = "Se requiere nombre y teléfono del cliente"
                    };
                }

                var guestRequest = new CtaGuestRequest
                {
                    Names = request.ClientName,
                    LastName = "",
                    PhoneNumber = request.ClientPhone,
                    Email = request.ClientEmail ?? "",
                    NickName = request.ClientNickName,
                    CompanyId = portal.CompanyId
                };

                var createdGuest = await _guestService.Add(guestRequest);
                participantId = createdGuest.Id;
                participantTypeId = (int)AppointmentParticipant.Guest;
            }

            // Obtener usuario asignado
            int assignedUserId;
            if (request.AssignedUser != null && request.AssignedUser != 0)
            {
                assignedUserId = request.AssignedUser.Value;
            }
            else
            {
                var mainAssignee = await _portalUsersRepository.GetMainAssigneeByPortalIdAsync(portal.Id);
                assignedUserId = mainAssignee?.UserId ?? throw new InvalidOperationException("No hay usuario principal configurado");
            }

            // Obtener área seleccionada
            int? selectedAreaId;
            if (request.AreaId.HasValue && request.AreaId.Value > 0)
            {
                selectedAreaId = request.AreaId.Value;
            }
            else
            {
                var defaultArea = await _portalAreasRepository.GetDefaultAreaByPortalIdAsync(portal.Id);
                selectedAreaId = defaultArea?.AreaId;
            }

            var appointmentRequest = new CtaAppointmentsRequest
            {
                Description = request.Description ?? "Cita agendada desde portal público",
                Title = $"Cita - {request.ClientName ?? "Cliente"}",
                IdReasonAppointment = portal.DefaultReasonId ?? 1,
                AppointmentDate = request.AppointmentDate,
                AppointmentTime = request.AppointmentTime,
                IdPlaceAppointment = portal.DefaultPlaceId ?? 1,
                IdState = portal.DefaultStateId ?? 1,
                IsConditionedTime = true,
                EndAppointmentTime = request.AppointmentTime.Add(portal.DefaultAppointmentDuration ?? new TimeSpan(1, 0, 0)),
                NotificationTime = new TimeSpan(0, 30, 0),
                AreaId = selectedAreaId,
                AssignedUser = assignedUserId,
                CompanyId = portal.CompanyId,
                AppointmentParticipants = new List<AppointmentParticipantsRequest>
                {
                    new AppointmentParticipantsRequest
                    {
                        ParticipantTypeId = participantTypeId,
                        ParticipantId = participantId
                    }
                }
            };

            try
            {
                var createdAppointment = await _appointmentService.AddAppointment(appointmentRequest, false);

                return new PublicAppointmentResponse
                {
                    Success = true,
                    AppointmentCode = createdAppointment.AppointmentCode,
                    AppointmentDate = createdAppointment.AppointmentDate,
                    AppointmentTime = createdAppointment.AppointmentTime,
                    ConfirmationDetails = $"Su cita ha sido confirmada para el {createdAppointment.AppointmentDate:dd/MM/yyyy} a las {createdAppointment.AppointmentTime.ToString(@"hh\:mm")}. Código de cita: {createdAppointment.AppointmentCode}"
                };
            }
            catch (Exception ex)
            {
                return new PublicAppointmentResponse
                {
                    Success = false,
                    ErrorMessage = $"Error al crear la cita: {ex.Message}"
                };
            }
        }

        public async Task<string> GenerateUniqueSlugAsync(string baseName)
        {
            var slug = baseName.ToLower()
                .Replace(" ", "-")
                .Replace("á", "a").Replace("é", "e").Replace("í", "i").Replace("ó", "o").Replace("ú", "u")
                .Replace("ñ", "n");

            var counter = 1;
            var originalSlug = slug;

            while (await _portalRepository.SlugExistsAsync(slug))
            {
                slug = $"{originalSlug}-{counter}";
                counter++;
            }

            return slug;
        }

        public async Task<CtaClientInfoResponse?> GetClientInfoAsync(CtaClientInfoRequest request)
        {
            // Buscar por teléfono si se proporciona
            if (!string.IsNullOrEmpty(request.PhoneNumber))
            {
                var clientByPhone = await FindClientByPhoneAndEmailAsync(request.PhoneNumber, null, request.CompanyId);
                if (clientByPhone != null) return clientByPhone;
            }

            // Buscar por email si se proporciona
            if (!string.IsNullOrEmpty(request.Email))
            {
                var clientByEmail = await FindClientByPhoneAndEmailAsync(null, request.Email, request.CompanyId);
                if (clientByEmail != null) return clientByEmail;
            }

            return null;
        }

        private async Task<CtaClientInfoResponse?> FindClientByPhoneAndEmailAsync(string? phoneNumber, string? email, long companyId)
        {
            // Buscar en contactos
            var contacts = await _contactRepository.GetAll();
            var contact = contacts.FirstOrDefault(c =>
                ((!string.IsNullOrEmpty(phoneNumber) && c.ContactNumber == phoneNumber) ||
                 (!string.IsNullOrEmpty(email) && c.ContactEmail == email)) &&
                c.CompanyId == companyId &&
                !c.Borrado);

            if (contact != null)
            {
                return new CtaClientInfoResponse
                {
                    ClientName = contact.Name,
                    ClientPhone = contact.ContactNumber,
                    ClientEmail = contact.ContactEmail,
                    ClientType = nameof(AppointmentParticipant.Contact),
                    IsExistingClient = true
                };
            }

            // Buscar en invitados
            var guests = await _guestRepository.GetAll();
            var guest = guests.FirstOrDefault(g =>
                ((!string.IsNullOrEmpty(phoneNumber) && g.PhoneNumber == phoneNumber) ||
                 (!string.IsNullOrEmpty(email) && g.Email == email)) &&
                g.CompanyId == companyId &&
                !g.Borrado);

            if (guest != null)
            {
                return new CtaClientInfoResponse
                {
                    ClientName = $"{guest.Names} {guest.LastName}",
                    ClientPhone = guest.PhoneNumber,
                    ClientEmail = guest.Email,
                    ClientNickName = guest.NickName,
                    ClientType = nameof(AppointmentParticipant.Guest),
                    IsExistingClient = true
                };
            }

            return null;
        }

        // Métodos auxiliares privados
        private async Task<List<dynamic>> GetExistingAppointmentsForDate(DateTime date, int userId, long companyId)
        {
            return new List<dynamic>();
        }

        private string GenerateAuthToken(int clientId, int participantTypeId, int portalId)
        {
            var tokenData = $"{clientId}:{participantTypeId}:{portalId}:{DateTime.UtcNow:yyyy-MM-dd}";
            return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(tokenData));
        }

        private (int ClientId, int ParticipantTypeId) ValidateAuthToken(string authToken)
        {
            try
            {
                var tokenData = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(authToken));
                var parts = tokenData.Split(':');

                if (parts.Length >= 3)
                {
                    var clientId = int.Parse(parts[0]);
                    var participantTypeId = int.Parse(parts[1]);
                    return (clientId, participantTypeId);
                }

                throw new UnauthorizedAccessException("Formato de token inválido");
            }
            catch
            {
                throw new UnauthorizedAccessException("Token inválido");
            }
        }

    }
}