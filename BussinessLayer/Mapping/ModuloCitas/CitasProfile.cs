using AutoMapper;
using BussinessLayer.DTOs.ModuloCitas;
using BussinessLayer.DTOs.ModuloCitas.CtaAppointmentManagement;
using BussinessLayer.DTOs.ModuloCitas.CtaAppointmentMovements;
using BussinessLayer.DTOs.ModuloCitas.CtaAppointmentReason;
using BussinessLayer.DTOs.ModuloCitas.CtaAppointments;
using BussinessLayer.DTOs.ModuloCitas.CtaCitaConfiguracion;
using BussinessLayer.DTOs.ModuloCitas.CtaContacts;
using BussinessLayer.DTOs.ModuloCitas.CtaEmailConfiguracion;
using BussinessLayer.DTOs.ModuloCitas.CtaEmailTemplates;
using BussinessLayer.DTOs.ModuloCitas.CtaEmailTemplateTypes;
using BussinessLayer.DTOs.ModuloCitas.CtaGuest;
using BussinessLayer.DTOs.ModuloCitas.CtaMeetingPlace;
using BussinessLayer.DTOs.ModuloCitas.CtaNotificationSettings;
using BussinessLayer.DTOs.ModuloCitas.CtaSessionDetails;
using BussinessLayer.DTOs.ModuloCitas.CtaSessions;
using BussinessLayer.DTOs.ModuloCitas.CtaState;
using BussinessLayer.DTOs.ModuloCitas.CtaUnwanted;
using DataLayer.Models.Modulo_Citas;
using DataLayer.Models.ModuloCitas;
using DataLayer.Models.ModuloGeneral.Seguridad;

namespace BussinessLayer.Mapping.ModuloCitas
{
    public class CitasProfile : Profile
    {
        public CitasProfile()
        {
            CreateMap<CtaAppointmentManagementRequest, CtaAppointmentManagement>()
                .ReverseMap();
            CreateMap<CtaAppointmentManagementResponse, CtaAppointmentManagement>()
               .ReverseMap();

            CreateMap<CtaAppointmentMovementsRequest, CtaAppointmentMovements>()
                .ReverseMap();
            CreateMap<CtaAppointmentMovementsResponse, CtaAppointmentMovements>()
                .ReverseMap();

            CreateMap<CtaAppointmentReasonRequest, CtaAppointmentReason>()
                .ReverseMap();
            CreateMap<CtaAppointmentReasonResponse, CtaAppointmentReason>()
                .ReverseMap();

            CreateMap<CtaAppointmentsRequest, CtaAppointments>()
               .ReverseMap();

            CreateMap<AppointmentInformation, CtaAppointments>()
               .ReverseMap();

            CreateMap<AppointmentInformation, CtaAppointmentsRequest>()
            .ReverseMap();

            #region AppointmentResponse
            CreateMap<CtaAppointments, CtaAppointmentsResponse>()
            .ForMember(dest => dest.ReasonDescription, opt => opt.MapFrom(src => src.CtaAppointmentReason != null ? src.CtaAppointmentReason.Description : string.Empty))
            .ForMember(dest => dest.MeetingPlaceDescription, opt => opt.MapFrom(src => src.CtaMeetingPlace != null ? src.CtaMeetingPlace.Description : string.Empty))
            .ForMember(dest => dest.StateDescription, opt => opt.MapFrom(src => src.CtaState != null ? src.CtaState.Description : string.Empty))
            .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.GnEmpresa != null ? src.GnEmpresa.NOMBRE_EMP : string.Empty))
            .ForMember(dest => dest.CtaAppointmentUsers, opt => opt.MapFrom(src => src.CtaAppointmentUsers != null ? src.CtaAppointmentUsers.Select(x => x.Usuario) : new List<Usuario>()))
            .ForMember(dest => dest.CtaAppointmentContacts, opt => opt.MapFrom(src => src.CtaAppointmentContacts ?? new List<CtaAppointmentContacts>()))
            .ForMember(dest => dest.CtaAppointmentManagement, opt => opt.MapFrom(src => src.CtaAppointmentManagement ?? new List<CtaAppointmentManagement>()))
            .ForMember(dest => dest.CtaGuest, opt => opt.MapFrom(src => src.CtaAppointmentGuest != null ?
                src.CtaAppointmentGuest
                .Where(g => g.Guest != null)
                .Select(g => new CtaGuestInformation
                {
                    Id = g.Guest.Id,
                    Names = g.Guest.Names,
                    LastName = g.Guest.LastName,
                    PhoneNumber = g.Guest.PhoneNumber,
                    Email = g.Guest.Email,
                    NickName = g.Guest.NickName
                }).ToList() : new List<CtaGuestInformation>()));

            CreateMap<CtaAppointmentGuest, CtaGuestInformation>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Guest.Id))
                .ForMember(dest => dest.Names, opt => opt.MapFrom(src => src.Guest.Names))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.Guest.LastName))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.Guest.PhoneNumber))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Guest.Email))
                .ForMember(dest => dest.NickName, opt => opt.MapFrom(src => src.Guest.NickName));

            CreateMap<CtaAppointmentContacts, CtaContactInformation>().ReverseMap();
            CreateMap<CtaGuest, CtaGuestInformation>().ReverseMap();
            CreateMap<CtaAppointmentManagement, CtaManagmentInformation>().ReverseMap();

            // Mapeo de la tabla intermedia `CtaAppointmentUsers`
            CreateMap<CtaAppointmentUsers, CtaUserInformation>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Usuario.Id))
                .ForMember(dest => dest.CodigoEmp, opt => opt.MapFrom(src => src.Usuario.CodigoEmp))
                .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Usuario.Nombre))
                .ForMember(dest => dest.Apellido, opt => opt.MapFrom(src => src.Usuario.Apellido))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Usuario.Email))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.Usuario.IsActive))
                .ReverseMap();

            CreateMap<CtaUserInformation, Usuario>().ReverseMap();
            CreateMap<CtaContactInformation, CtaAppointmentContacts>().ReverseMap();
            CreateMap<CtaGuestInformation, CtaGuest>().ReverseMap();
            CreateMap<CtaManagmentInformation, CtaAppointmentManagement>().ReverseMap();

            #endregion

            CreateMap<CtaConfiguracionRequest, CtaConfiguration>()
                .ReverseMap();
            CreateMap<CtaConfiguracionResponse, CtaConfiguration>()
             .ReverseMap();

            CreateMap<CtaEmailConfiguracionRequest, CtaEmailConfiguration>()
                .ReverseMap();
            CreateMap<CtaEmailConfiguracionResponse, CtaEmailConfiguration>()
               .ReverseMap();

            CreateMap<CtaMeetingPlaceRequest, CtaMeetingPlace>()
                .ReverseMap();
            CreateMap<CtaMeetingPlaceResponse, CtaMeetingPlace>()
                .ReverseMap();

            CreateMap<CtaSessionDetailsRequest, CtaSessionDetails>()
                .ReverseMap();
            CreateMap<CtaSessionDetailsResponse, CtaSessionDetails>()
                .ReverseMap();

            CreateMap<CtaSessionsRequest, CtaSessions>()
                .ForPath(dest => dest.CompanyId, opt => opt.MapFrom(src => src.AppointmentInformation.CompanyId))
                .ForPath(dest => dest.Description, opt => opt.MapFrom(src => src.AppointmentInformation.Description))
                .ForMember(dest => dest.IdUser, opt => opt.MapFrom(src => src.IdUser))
                .ReverseMap()
                .ForPath(dest => dest.AppointmentInformation.UserId, opt => opt.MapFrom(src => src.IdUser));

            CreateMap<CtaSessionsResponse, CtaSessions>()
                .ReverseMap();

            CreateMap<CtaStateRequest, CtaState>()
                .ReverseMap();
            CreateMap<CtaStateResponse, CtaState>()
                .ReverseMap();

            CreateMap<CtaUnwantedRequest, CtaUnwanted>()
                .ReverseMap();
            CreateMap<CtaUnwantedResponse, CtaUnwanted>()
               .ReverseMap();

            CreateMap<CtaContactTypeRequest, CtaContactType>()
              .ReverseMap();
            CreateMap<CtaContactTypeResponse, CtaContactType>()
               .ReverseMap();

            CreateMap<CtaAppointmentContactsRequest, CtaAppointmentContacts>()
              .ReverseMap();
            CreateMap<CtaAppointmentContactsResponse, CtaAppointmentContacts>()
               .ReverseMap();

            CreateMap<CtaAppointmentUsersRequest, CtaAppointmentUsers>()
              .ReverseMap();
            CreateMap<CtaAppointmentUsersResponse, CtaAppointmentUsers>()
               .ReverseMap();
            CreateMap<CtaAppointmentSequenceRequest, CtaAppointmentSequence>()
            .ReverseMap();
            CreateMap<CtaAppointmentSequenceResponse, CtaAppointmentSequence>()
               .ReverseMap();

            CreateMap<CtaAppointmentSequenceRequest, CtaAppointmentSequence>()
                .ReverseMap();
            CreateMap<CtaAppointmentSequenceResponse, CtaAppointmentSequence>()
               .ReverseMap();

            CreateMap<CtaAppointmentAreaRequest, CtaAppointmentArea>()
                .ReverseMap();
            CreateMap<CtaAppointmentAreaResponse, CtaAppointmentArea>()
               .ReverseMap();

            CreateMap<CtaAreaXUserRequest, CtaAreaXUser>()
                .ReverseMap();
            CreateMap<CtaAreaXUserResponse, CtaAreaXUser>()
               .ReverseMap();

            CreateMap<CtaGuestRequest, CtaGuest>()
               .ReverseMap();
            CreateMap<CtaGuestResponse, CtaGuest>()
               .ReverseMap();

            CreateMap<CtaContactRequest, CtaContacts>()
             .ReverseMap();
            CreateMap<CtaContactResponse, CtaContacts>()
               .ReverseMap();

            CreateMap<CtaEmailTemplateTypes, CtaEmailTemplateTypesRequest>()
             .ReverseMap();
            CreateMap<CtaEmailTemplateTypes, CtaEmailTemplateTypesResponse>()
               .ReverseMap();

            CreateMap<CtaEmailTemplates, CtaEmailTemplatesRequest>()
             .ReverseMap();
            CreateMap<CtaEmailTemplates, CtaEmailTemplatesResponse>()
               .ReverseMap();

            CreateMap<CtaNotificationSettings, CtaNotificationSettingsRequest>()
             .ReverseMap();
            CreateMap<CtaNotificationSettings, CtaNotificationSettingsResponse>()
               .ReverseMap();
        }

    }
}
