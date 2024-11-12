using AutoMapper;
using BussinessLayer.DTOs.HelpDesk;
using DataLayer.Models.HelpDesk;

namespace BussinessLayer.Mapping.HeplDesk
{
    public class HdkCategoryTicketProfile:Profile
    {
        public HdkCategoryTicketProfile()
        {
            CreateMap<HdkCategoryTicketRequest, HdkCategoryTicket>()
            .ForMember(dest => dest.Descripcion, opt => opt.MapFrom(src => src.Descripcion))
            .ForMember(dest => dest.IdEmpresa, opt => opt.MapFrom(src => src.IdEmpresa)).ReverseMap();

            CreateMap<HdkCategoryTicketReponse, HdkCategoryTicket>()
            .ForMember(dest => dest.IdCategoria, opt => opt.MapFrom(src => src.IdCategoria))
            .ForMember(dest => dest.Descripcion, opt => opt.MapFrom(src => src.Descripcion))
            .ForMember(dest => dest.FechaAdicion, opt => opt.MapFrom(src => src.FechaAdicion))
            .ForMember(dest => dest.FechaModificacion, opt => opt.MapFrom(src => src.FechaModificacion))
            .ForMember(dest => dest.UsuarioAdicion, opt => opt.MapFrom(src => src.UsuarioAdicion))
            .ForMember(dest => dest.UsuarioModificacion, opt => opt.MapFrom(src => src.UsuarioModificacion))
            .ForMember(dest => dest.Borrado, opt => opt.MapFrom(src => src.Borrado))
            .ForMember(dest => dest.IdEmpresa, opt => opt.MapFrom(src => src.IdEmpresa)).ReverseMap();


        }
    }
}
