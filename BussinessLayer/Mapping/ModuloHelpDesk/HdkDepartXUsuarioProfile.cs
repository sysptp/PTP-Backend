using AutoMapper;
using DataLayer.Models.ModuloHelpDesk;
using BussinessLayer.DTOs.ModuloHelpDesk;


namespace BussinessLayer.Mapping.ModuloHelpDesk
{
    public class HdkDepartXUsuarioProfile : Profile
    {
        public HdkDepartXUsuarioProfile()
        {
            CreateMap<HdkDepartXUsuarioRequest, HdkDepartXUsuario>()
           .ReverseMap();

            CreateMap<HdkDepartXUsuario, HdkDepartXUsuarioReponse>()
            .ForPath(dest => dest.NombreEmpresa, opt => opt.MapFrom(src => src.GnEmpresa.NOMBRE_EMP))
            .ForPath(dest => dest.NombreUsuario, opt => opt.MapFrom(src => src.Usuario.Nombre))
            .ForMember(dest => dest.HdkDepartamentsReponse, opt => opt.MapFrom(src => src.HdkDepartaments))
            .ReverseMap();

        }

    }
}
