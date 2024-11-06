using AutoMapper;
using BussinessLayer.DTOs.ModuloGeneral.Sucursal;
using DataLayer.Models.Empresa;


namespace BussinessLayer.Mapping
{
    public class GnSucursalProfile : Profile
    {
        public GnSucursalProfile()
        {
            CreateMap<GnSucursal, GnSucursalResponse>()
                .ForMember(dest => dest.SucursalId, opt => opt.MapFrom(src => src.CodigoSuc))
                .ForMember(dest => dest.CompanyId, opt => opt.MapFrom(src => src.CodigoEmp))
                .ForMember(dest => dest.SucursalName, opt => opt.MapFrom(src => src.NombreSuc))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Telefono1))
                .ForMember(dest => dest.ResponsibleUserId, opt => opt.MapFrom(src => src.IdUsuarioResponsable))
                .ForMember(dest => dest.CountryId, opt => opt.MapFrom(src => src.CodPais))
                .ForMember(dest => dest.RegionId, opt => opt.MapFrom(src => src.CodRegion))
                .ForMember(dest => dest.ProvinceId, opt => opt.MapFrom(src => src.CodProvincia))
                .ForMember(dest => dest.MunicipalityId, opt => opt.MapFrom(src => src.IdMunicipio))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Direccion))
                .ForMember(dest => dest.SucursalStatus, opt => opt.MapFrom(src => src.EstadoSuc))
                .ForMember(dest => dest.AdditionIp, opt => opt.MapFrom(src => src.IpAdicion))
                .ForMember(dest => dest.ModificationIp, opt => opt.MapFrom(src => src.IpModificacion))
                .ForMember(dest => dest.Longitude, opt => opt.MapFrom(src => src.Longitud))
                .ForMember(dest => dest.Latitude, opt => opt.MapFrom(src => src.Latitud))
                .ForMember(dest => dest.IsPrincipal, opt => opt.MapFrom(src => src.Principal))
                .ReverseMap();

            CreateMap<GnSucursalRequest, GnSucursal>()
                .ForMember(dest => dest.CodigoEmp, opt => opt.MapFrom(src => src.CompanyId))
                .ForMember(dest => dest.NombreSuc, opt => opt.MapFrom(src => src.SucursalName))
                .ForMember(dest => dest.Telefono1, opt => opt.MapFrom(src => src.Phone))
                .ForMember(dest => dest.IdUsuarioResponsable, opt => opt.MapFrom(src => src.ResponsibleUserId))
                .ForMember(dest => dest.CodPais, opt => opt.MapFrom(src => src.CountryId))
                .ForMember(dest => dest.CodRegion, opt => opt.MapFrom(src => src.RegionId))
                .ForMember(dest => dest.CodProvincia, opt => opt.MapFrom(src => src.ProvinceId))
                .ForMember(dest => dest.IdMunicipio, opt => opt.MapFrom(src => src.MunicipalityId))
                .ForMember(dest => dest.Direccion, opt => opt.MapFrom(src => src.Address))
                .ForMember(dest => dest.EstadoSuc, opt => opt.MapFrom(src => src.SucursalStatus))
                .ForMember(dest => dest.IpAdicion, opt => opt.MapFrom(src => src.UserIp))
                .ForMember(dest => dest.Principal, opt => opt.MapFrom(src => src.IsPrincipal))
                .ReverseMap();
        }
    }
}
