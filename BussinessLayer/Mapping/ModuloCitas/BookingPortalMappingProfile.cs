using System.Text.Json;
using AutoMapper;
using BussinessLayer.DTOs.ModuloCitas.BookingPortal;
using DataLayer.Models.ModuloCitas;

namespace BussinessLayer.Mapping.ModuloCitas
{
    public class BookingPortalMappingProfile : Profile
    {
        public BookingPortalMappingProfile()
        {
            // Request to Entity
            CreateMap<BookingPortalConfigRequest, CtaBookingPortalConfig>()
                .ForMember(dest => dest.AvailableDaysJson, opt => opt.Ignore()); // Se maneja manualmente

            // Entity to Response
            CreateMap<CtaBookingPortalConfig, BookingPortalConfigResponse>()
                .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.Company != null ? src.Company.NOMBRE_EMP : null))
                .ForMember(dest => dest.DefaultReasonName, opt => opt.MapFrom(src => src.DefaultReason != null ? src.DefaultReason.Description : null))
                .ForMember(dest => dest.DefaultPlaceName, opt => opt.MapFrom(src => src.DefaultPlace != null ? src.DefaultPlace.Description : null))
                .ForMember(dest => dest.DefaultStateName, opt => opt.MapFrom(src => src.DefaultState != null ? src.DefaultState.Description : null))
                .ForMember(dest => dest.AvailableDays, opt => opt.MapFrom(src =>src.AvailableDaysJson))
                .ForMember(dest => dest.PublicUrl, opt => opt.Ignore()); 
        }
    }
}
