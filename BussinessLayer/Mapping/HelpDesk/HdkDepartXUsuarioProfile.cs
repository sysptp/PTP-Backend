using BussinessLayer.DTOs.HelpDesk;
using AutoMapper;
using DataLayer.Models.ModuloHelpDesk;


namespace BussinessLayer.Mapping.HeplDesk
{
    public class HdkDepartXUsuarioProfile:Profile
    {
        public HdkDepartXUsuarioProfile()
        {
            CreateMap<HdkDepartXUsuarioRequest, HdkDepartXUsuario>()
           .ForMember(dest => dest.IdDepartamento, opt => opt.MapFrom(src => src.IdDepartamento))
           .ForMember(dest => dest.IdEmpresa, opt => opt.MapFrom(src => src.IdEmpresa))
           .ReverseMap();

            CreateMap<HdkDepartXUsuarioReponse, HdkDepartXUsuario>()
            .ForMember(dest => dest.IdDepartamento, opt => opt.MapFrom(src => src.IdDepartamento))         
            .ForMember(dest => dest.FechaAdicion, opt => opt.MapFrom(src => src.FechaAdicion))
            .ForMember(dest => dest.FechaModificacion, opt => opt.MapFrom(src => src.FechaModificacion))
            .ForMember(dest => dest.UsuarioAdicion, opt => opt.MapFrom(src => src.UsuarioAdicion))
            .ForMember(dest => dest.UsuarioModificacion, opt => opt.MapFrom(src => src.UsuarioModificacion))
            .ForMember(dest => dest.Borrado, opt => opt.MapFrom(src => src.Borrado))
            .ForMember(dest => dest.IdEmpresa, opt => opt.MapFrom(src => src.IdEmpresa)).ReverseMap();

        }
    }
}
