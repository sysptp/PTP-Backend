using AutoMapper;
using BussinessLayer.DTOs.ModuloCitas;
using BussinessLayer.DTOs.ModuloCitas.CtaAppointmentManagement;
using BussinessLayer.DTOs.ModuloCitas.CtaAppointmentMovements;
using BussinessLayer.DTOs.ModuloCitas.CtaAppointmentReason;
using BussinessLayer.DTOs.ModuloCitas.CtaAppointments;
using BussinessLayer.DTOs.ModuloCitas.CtaCitaConfiguracion;
using BussinessLayer.DTOs.ModuloCitas.CtaEmailConfiguracion;
using BussinessLayer.DTOs.ModuloCitas.CtaMeetingPlace;
using BussinessLayer.DTOs.ModuloCitas.CtaSessionDetails;
using BussinessLayer.DTOs.ModuloCitas.CtaSessions;
using BussinessLayer.DTOs.ModuloCitas.CtaState;
using BussinessLayer.DTOs.ModuloCitas.CtaUnwanted;
using DataLayer.Models.Modulo_Citas;
using DataLayer.Models.ModuloCitas;

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

            CreateMap<CtaAppointmentMovementsRequest,  CtaAppointmentMovements>()
                .ReverseMap();
            CreateMap<CtaAppointmentMovementsResponse, CtaAppointmentMovements>()
                .ReverseMap();

            CreateMap<CtaAppointmentReasonRequest, CtaAppointmentReason>()
                .ReverseMap();
            CreateMap<CtaAppointmentReasonResponse, CtaAppointmentReason>()
                .ReverseMap();

            CreateMap<CtaAppointmentsRequest, CtaAppointments>()
                .ReverseMap();
            CreateMap<CtaAppointmentsResponse, CtaAppointments>()
               .ReverseMap();

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
                .ReverseMap();
            CreateMap<CtaSessionsResponse, CtaSessions>()
                .ReverseMap();
            CreateMap<AppointmentInformation, CtaAppointmentsRequest>()
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.AppointmentDescription))
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
        }

    }
}
