using AutoMapper;
using BussinessLayer.DTOs.ModuloHelpDesk;
using DataLayer.Models.ModuloHelpDesk;

namespace BussinessLayer.Mapping.ModuloHelpDesk
{
    public class HdkDepartamentsProfile : Profile
    {
        public HdkDepartamentsProfile()
        {
            CreateMap<HdkDepartamentsRequest, HdkDepartaments>()
           .ForMember(dest => dest.Descripcion, opt => opt.MapFrom(src => src.Descripcion))
           .ForMember(dest => dest.IdEmpresa, opt => opt.MapFrom(src => src.IdEmpresa))
           .ForMember(dest => dest.EsPrincipal, opt => opt.MapFrom(src => src.EsPrincipal)).ReverseMap();

            CreateMap<HdkDepartamentsReponse, HdkDepartaments>()
            .ForMember(dest => dest.IdDepartamentos, opt => opt.MapFrom(src => src.IdDepartamentos))
            .ForMember(dest => dest.Descripcion, opt => opt.MapFrom(src => src.Descripcion))
            .ForMember(dest => dest.EsPrincipal, opt => opt.MapFrom(src => src.EsPrincipal))
            .ForMember(dest => dest.FechaAdicion, opt => opt.MapFrom(src => src.FechaAdicion))
            .ForMember(dest => dest.FechaModificacion, opt => opt.MapFrom(src => src.FechaModificacion))
            .ForMember(dest => dest.UsuarioAdicion, opt => opt.MapFrom(src => src.UsuarioAdicion))
            .ForMember(dest => dest.UsuarioModificacion, opt => opt.MapFrom(src => src.UsuarioModificacion))
            .ForMember(dest => dest.Borrado, opt => opt.MapFrom(src => src.Borrado))
            .ForMember(dest => dest.IdEmpresa, opt => opt.MapFrom(src => src.IdEmpresa)).ReverseMap();

        }
    }
}
