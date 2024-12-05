using AutoMapper;
using BussinessLayer.DTOs.ModuloGeneral.Empresas;
using DataLayer.Models.Empresa;

namespace BussinessLayer.Mapping.ModuloGeneral.Empresa
{
    public class GnEmpresaProfile : Profile
    {
        public GnEmpresaProfile()
        {
            CreateMap<GnEmpresaRequest, GnEmpresa>()
                .ForMember(dest => dest.NOMBRE_EMP, opt => opt.MapFrom(src => src.CompanyName))
                .ForMember(dest => dest.LOGO_EMP, opt => opt.MapFrom(src => src.Logo))
                .ForMember(dest => dest.RNC_EMP, opt => opt.MapFrom(src => src.RNC))
                .ForMember(dest => dest.DIRECCION, opt => opt.MapFrom(src => src.Address))
                .ForMember(dest => dest.TELEFONO1, opt => opt.MapFrom(src => src.PrimaryPhone))
                .ForMember(dest => dest.TELEFONO2, opt => opt.MapFrom(src => src.SecondaryPhone))
                .ForMember(dest => dest.EXT_TEL1, opt => opt.MapFrom(src => src.PrimaryExtension))
                .ForMember(dest => dest.EXT_TEL2, opt => opt.MapFrom(src => src.SecondaryExtension))
                .ForMember(dest => dest.CANT_SUCURSALES, opt => opt.MapFrom(src => src.SucursalCount))
                .ForMember(dest => dest.CANT_USUARIO, opt => opt.MapFrom(src => src.UserCount))
                .ForMember(dest => dest.WEB_URL, opt => opt.MapFrom(src => src.WebsiteUrl))
                .ReverseMap();

            CreateMap<GnEmpresa, GnEmpresaResponse>()
                .ForMember(dest => dest.CompanyId, opt => opt.MapFrom(src => src.CODIGO_EMP))
                .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.NOMBRE_EMP))
                .ForMember(dest => dest.Logo, opt => opt.MapFrom(src => src.LOGO_EMP))
                .ForMember(dest => dest.RNC, opt => opt.MapFrom(src => src.RNC_EMP))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.DIRECCION))
                .ForMember(dest => dest.PrimaryPhone, opt => opt.MapFrom(src => src.TELEFONO1))
                .ForMember(dest => dest.SecondaryPhone, opt => opt.MapFrom(src => src.TELEFONO2))
                .ForMember(dest => dest.PrimaryExtension, opt => opt.MapFrom(src => src.EXT_TEL1))
                .ForMember(dest => dest.SecondaryExtension, opt => opt.MapFrom(src => src.EXT_TEL2))
                .ForMember(dest => dest.SucursalCount, opt => opt.MapFrom(src => src.CANT_SUCURSALES))
                .ForMember(dest => dest.UserCount, opt => opt.MapFrom(src => src.CANT_USUARIO))
                .ForMember(dest => dest.WebsiteUrl, opt => opt.MapFrom(src => src.WEB_URL))
                .ReverseMap();
        }
    }

}
