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
        private readonly ICtaContactRepository _contactRepository;
        private readonly ICtaGuestRepository _guestRepository;
        private readonly ICtaAppointmentsService _appointmentService;
        private readonly ICtaContactService _contactService;
        private readonly ICtaGuestService _guestService;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public CtaBookingPortalService(
            ICtaBookingPortalConfigRepository portalRepository,
            ICtaContactRepository contactRepository,
            ICtaGuestRepository guestRepository,
            ICtaAppointmentsService appointmentService,
            ICtaContactService contactService,
            ICtaGuestService guestService,
            IMapper mapper,
            IConfiguration configuration)
        {
            _portalRepository = portalRepository;
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
            // Generar slug único si no se proporciona
            if (string.IsNullOrEmpty(request.CustomSlug))
            {
                request.CustomSlug = await GenerateUniqueSlugAsync(request.PortalName);
            }
            else
            {
                // Verificar que el slug no exista
                if (await _portalRepository.SlugExistsAsync(request.CustomSlug))
                {
                    throw new InvalidOperationException($"El slug '{request.CustomSlug}' ya está en uso.");
                }
            }

            var entity = _mapper.Map<CtaBookingPortalConfig>(request);
            entity.AvailableDaysJson = request.AvailableDays != null ?
                JsonSerializer.Serialize(request.AvailableDays) : null;

            var created = await _portalRepository.Add(entity);
            var response = _mapper.Map<BookingPortalConfigResponse>(created);
            response.PublicUrl = GeneratePublicUrl(response.CustomSlug);

            return response;
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

            // Si el portal no requiere autenticación, permitir acceso
            if (!portal.RequireAuthentication)
            {
                return new ClientAuthenticationResponse
                {
                    IsAuthenticated = true,
                    IsNewClient = false,
                    Message = "Acceso permitido",
                    AuthToken = GenerateAuthToken(request.PhoneNumber, portal.Id)
                };
            }

            // Buscar en contactos
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
                    ClientType = "Contact",
                    AuthToken = GenerateAuthToken(request.PhoneNumber, portal.Id),
                    Message = "Cliente autenticado exitosamente"
                };
            }

            // Buscar en invitados
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
                    ClientType = "Guest",
                    AuthToken = GenerateAuthToken(request.PhoneNumber, portal.Id),
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

            // Si requiere autenticación, validar token
            if (portal.RequireAuthentication && string.IsNullOrEmpty(request.AuthToken))
            {
                throw new UnauthorizedAccessException("Se requiere autenticación");
            }

            var response = new AvailableSlotResponse
            {
                Date = request.Date,
                AvailableSlots = new List<TimeSlot>()
            };

            // Verificar si el día está disponible
            var availableDays = !string.IsNullOrEmpty(portal.AvailableDaysJson) ?
                JsonSerializer.Deserialize<List<int>>(portal.AvailableDaysJson) : null;

            if (availableDays != null && !availableDays.Contains((int)request.Date.DayOfWeek))
            {
                return response; // Día no disponible
            }

            // Generar slots disponibles
            var startTime = portal.StartTime ?? new TimeSpan(9, 0, 0);
            var endTime = portal.EndTime ?? new TimeSpan(17, 0, 0);
            var duration = portal.DefaultAppointmentDuration ?? new TimeSpan(1, 0, 0);

            // Obtener citas existentes para ese día
            var existingAppointments = await GetExistingAppointmentsForDate(request.Date, portal.AssignedUserId, portal.CompanyId);

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
                    Message = "Portal no encontrado"
                };
            }

            int participantId;
            int participantTypeId;

            // Si requiere autenticación, validar y obtener participante existente
            if (portal.RequireAuthentication)
            {
                if (string.IsNullOrEmpty(request.AuthToken))
                {
                    return new PublicAppointmentResponse
                    {
                        Success = false,
                        Message = "Se requiere autenticación"
                    };
                }

                var clientInfo = ValidateAuthToken(request.AuthToken);
                participantId = clientInfo.ClientId;
                participantTypeId = clientInfo.ClientType == "Contact" ?
                    (int)AppointmentParticipant.Contact : (int)AppointmentParticipant.Guest;
            }
            else
            {
                // Crear nuevo participante como guest
                if (string.IsNullOrEmpty(request.ClientName) || string.IsNullOrEmpty(request.ClientPhone))
                {
                    return new PublicAppointmentResponse
                    {
                        Success = false,
                        Message = "Se requiere nombre y teléfono del cliente"
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

            // Crear la cita
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
                AreaId = portal.AreaId,
                AssignedUser = portal.AssignedUserId,
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
                    Message = "Cita creada exitosamente",
                    ConfirmationDetails = $"Su cita ha sido confirmada para el {createdAppointment.AppointmentDate:dd/MM/yyyy} a las {createdAppointment.AppointmentTime:HH:mm}. Código de cita: {createdAppointment.AppointmentCode}"
                };
            }
            catch (Exception ex)
            {
                return new PublicAppointmentResponse
                {
                    Success = false,
                    Message = $"Error al crear la cita: {ex.Message}"
                };
            }
        }

        // Métodos auxiliares privados
        private async Task<List<dynamic>> GetExistingAppointmentsForDate(DateTime date, int userId, long companyId)
        {
            // Aquí implementarías la lógica para obtener citas existentes
            // Por simplicidad, retorno una lista vacía
            return new List<dynamic>();
        }

        private string GenerateAuthToken(string phoneNumber, int portalId)
        {
            var tokenData = $"{phoneNumber}:{portalId}:{DateTime.UtcNow:yyyy-MM-dd}";
            return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(tokenData));
        }

        private (int ClientId, string ClientType) ValidateAuthToken(string authToken)
        {
            try
            {
                var tokenData = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(authToken));
                // Implementar validación real del token
                // Por ahora retorno valores dummy
                return (1, "Contact");
            }
            catch
            {
                throw new UnauthorizedAccessException("Token inválido");
            }
        }

        private string GeneratePublicUrl(string slug)
        {
            var baseUrl = _configuration["ApplicationUrl"] ?? "https://localhost";
            return $"{baseUrl}/booking/{slug}";
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

        // Implementar otros métodos de la interfaz...
        public Task<BookingPortalConfigResponse> UpdatePortalAsync(int id, BookingPortalConfigRequest request)
        {
            throw new NotImplementedException();
        }

        public Task DeletePortalAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<BookingPortalConfigResponse?> GetPortalByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<BookingPortalConfigResponse?> GetPortalBySlugAsync(string slug)
        {
            throw new NotImplementedException();
        }

        public Task<List<BookingPortalConfigResponse>> GetPortalsByCompanyAsync(long companyId)
        {
            throw new NotImplementedException();
        }
    }
}
