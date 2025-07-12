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
                .ForMember(dest => dest.AvailableDaysJson, opt => opt.Ignore())
                .ReverseMap(); // Se maneja manualmente

            // Entity to Response
            CreateMap<CtaBookingPortalConfig, BookingPortalConfigResponse>()
                .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.Company != null ? src.Company.NOMBRE_EMP : null))
                .ForMember(dest => dest.DefaultReasonName, opt => opt.MapFrom(src => src.DefaultReason != null ? src.DefaultReason.Description : null))
                .ForMember(dest => dest.DefaultPlaceName, opt => opt.MapFrom(src => src.DefaultPlace != null ? src.DefaultPlace.Description : null))
                .ForMember(dest => dest.DefaultStateName, opt => opt.MapFrom(src => src.DefaultState != null ? src.DefaultState.Description : null))
                .ForMember(dest => dest.AvailableDays, opt => opt.MapFrom(src =>
        DeserializeAvailableDays(src.AvailableDaysJson)))
                 .ReverseMap();

            CreateMap<BookingPortalUserResponse, CtaBookingPortalUsers>().ReverseMap();
            CreateMap<BookingPortalUserRequest, CtaBookingPortalUsers>().ReverseMap();

            CreateMap<BookingPortalAreaRequest, CtaBookingPortalAreas>().ReverseMap();
            CreateMap<BookingPortalAreaResponse, CtaBookingPortalAreas>().ReverseMap();
        }

        private static List<int> DeserializeAvailableDays(string json)
        {
            if (string.IsNullOrEmpty(json)) return new List<int>();
            try
            {
                return JsonSerializer.Deserialize<List<int>>(json) ?? new List<int>();
            }
            catch
            {
                return new List<int>();
            }
        }
    }
}
