using AutoMapper;
using BussinessLayer.DTOs.Configuracion.Geografia.DMunicipio;
using BussinessLayer.DTOs.Configuracion.Geografia.DPais;
using BussinessLayer.DTOs.Configuracion.Geografia.DProvincia;
using BussinessLayer.DTOs.Configuracion.Geografia.DRegion;
using BussinessLayer.DTOs.Configuracion.Menu;
using BussinessLayer.DTOs.Configuracion.Seguridad;
using BussinessLayer.DTOs.ModuloGeneral.Empresas;
using DataLayer.Models.Entities;
using DataLayer.Models.Geografia;
using DataLayer.Models.MenuApp;

namespace TaskMaster.Core.Application.Mapping
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            #region AGREGADOS POR Domingo 

            #region GnPefil
            CreateMap<GnPerfil, GnPerfilResponse>()
               .ForMember(dest => dest.IdRole, opt => opt.MapFrom(src => src.Id))
               .ForMember(dest => dest.CompanyId, opt => opt.MapFrom(src => src.IDEmpresa))
               .ReverseMap();

            CreateMap<GnPerfil, GnPerfilRequest>()
                .ForMember(dest => dest.CompanyId, opt => opt.MapFrom(src => src.IDEmpresa))
                .ReverseMap();
            #endregion

            #region
            CreateMap<GnEmpresaRequest, GnEmpresaResponse>().ReverseMap();
            #endregion

            #region  configurations de geografía
            CreateMap<CountryRequest, Pais>()
                .ForMember(dest => dest.Nombre, opt => opt.MapFrom(x => x.Name))
                .ReverseMap();
            CreateMap<CountryResponse, Pais>()
                .ForMember(dest => dest.Nombre, opt => opt.MapFrom(x => x.Name))
                .ReverseMap();

            CreateMap<RegionRequest, Region>()
                .ForMember(dest => dest.Nombre, opt => opt.MapFrom(x => x.Name))
                .ForMember(dest => dest.IdPais, opt => opt.MapFrom(x => x.CountryId))
                .ReverseMap();
            CreateMap<RegionResponse, Region>()
                .ForMember(dest => dest.Nombre, opt => opt.MapFrom(x => x.Name))
                .ForMember(dest => dest.IdPais, opt => opt.MapFrom(x => x.CountryId))
                .ReverseMap();

            CreateMap<MunicipioRequest, Municipio>()
               .ForMember(dest => dest.Nombre, opt => opt.MapFrom(x => x.Name))
               .ForMember(dest => dest.IdProvincia, opt => opt.MapFrom(x => x.ProvinceId))
               .ReverseMap();
            CreateMap<MunicipioResponse, Municipio>()
                .ForMember(dest => dest.Nombre, opt => opt.MapFrom(x => x.Name))
                .ForMember(dest => dest.IdProvincia, opt => opt.MapFrom(x => x.ProvinceId))
                .ReverseMap();

            CreateMap<ProvinceRequest, Provincia>()
               .ForMember(dest => dest.Nombre, opt => opt.MapFrom(x => x.Name))
               .ForMember(dest => dest.IdRegion, opt => opt.MapFrom(x => x.RegionId))
               .ReverseMap();
            CreateMap<ProvinceResponse, Provincia>()
                .ForMember(dest => dest.Nombre, opt => opt.MapFrom(x => x.Name))
                .ForMember(dest => dest.IdRegion, opt => opt.MapFrom(x => x.RegionId))
                .ReverseMap();

            #endregion

            #region Menu
            CreateMap<GnMenu, GnMenuResponse>()
                .ForMember(dest => dest.MenuID, opt => opt.MapFrom(src => src.IDMenu))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Menu))
                .ForMember(dest => dest.Level, opt => opt.MapFrom(src => src.Nivel))
                .ForMember(dest => dest.Order, opt => opt.MapFrom(src => src.Orden))
                .ForMember(dest => dest.Url, opt => opt.MapFrom(src => src.URL))
                .ForMember(dest => dest.Icon, opt => opt.MapFrom(src => src.MenuIcon))
                .ForMember(dest => dest.ModuleID, opt => opt.MapFrom(src => src.IdModulo))
                .ForMember(dest => dest.ParentMenuId, opt => opt.MapFrom(src => src.MenuPadre))
                .ForMember(dest => dest.Query, opt => opt.MapFrom(src => src.Consultar))
                .ForMember(dest => dest.Create, opt => opt.MapFrom(src => src.Crear))
                .ForMember(dest => dest.Edit, opt => opt.MapFrom(src => src.Editar))
                .ForMember(dest => dest.Delete, opt => opt.MapFrom(src => src.Eliminar))
                .ReverseMap();

            CreateMap<GnMenu, SaveGnMenuRequest>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Menu))
                .ForMember(dest => dest.Level, opt => opt.MapFrom(src => src.Nivel))
                .ForMember(dest => dest.Order, opt => opt.MapFrom(src => src.Orden))
                .ForMember(dest => dest.Url, opt => opt.MapFrom(src => src.URL))
                .ForMember(dest => dest.Icon, opt => opt.MapFrom(src => src.MenuIcon))
                .ForMember(dest => dest.ModuleID, opt => opt.MapFrom(src => src.IdModulo))
                .ForMember(dest => dest.ParentMenuId, opt => opt.MapFrom(src => src.MenuPadre))
                .ForMember(dest => dest.Query, opt => opt.MapFrom(src => src.Consultar))
                .ForMember(dest => dest.Create, opt => opt.MapFrom(src => src.Crear))
                .ForMember(dest => dest.Edit, opt => opt.MapFrom(src => src.Editar))
                .ForMember(dest => dest.Delete, opt => opt.MapFrom(src => src.Eliminar))
                .ReverseMap();
            #endregion
            #endregion

        }
    }

}
