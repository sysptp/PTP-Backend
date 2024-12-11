using AutoMapper;
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

            CreateMap<CtaCitaConfiguracionRequest, CtaCitaConfiguracion>()
                .ReverseMap();
            CreateMap<CtaCitaConfiguracionResponse, CtaCitaConfiguracion>()
             .ReverseMap();

            CreateMap<CtaEmailConfiguracionRequest, CtaEmailConfiguracion>()
                .ReverseMap();
            CreateMap<CtaEmailConfiguracionResponse, CtaEmailConfiguracion>()
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
                .ReverseMap();
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
        }

    }
}
