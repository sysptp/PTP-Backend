using AutoMapper;
using BussinessLayer.DTOs.ModuloGeneral.Configuracion.Seguridad.Schedule;
using BussinessLayer.DTOs.ModuloGeneral.Empresas;
using BussinessLayer.DTOs.ModuloGeneral.Geografia.DMunicipio;
using BussinessLayer.DTOs.ModuloGeneral.Geografia.DPais;
using BussinessLayer.DTOs.ModuloGeneral.Geografia.DProvincia;
using BussinessLayer.DTOs.ModuloGeneral.Geografia.DRegion;
using BussinessLayer.DTOs.ModuloGeneral.Menu;
using BussinessLayer.DTOs.ModuloGeneral.Seguridad.Perfil;
using BussinessLayer.DTOs.ModuloGeneral.Seguridad.Permiso;
using BussinessLayer.DTOs.ModuloGeneral.Seguridad.Usuario;
using DataLayer.Models.ModuloGeneral.Geografia;
using DataLayer.Models.ModuloGeneral.Menu;
using DataLayer.Models.ModuloGeneral.Seguridad;

namespace BussinessLayer.Mapping.ModuloGeneral
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
                .ReverseMap();

            #endregion

            #region Usuario

            CreateMap<Usuario, UserResponse>()
           .ForMember(dest => dest.CompanyId, opt => opt.MapFrom(src => src.CodigoEmp ?? 0))
           .ForMember(dest => dest.ScheduleId, opt => opt.MapFrom(src => src.IdHorario))
           .ForMember(dest => dest.RoleId, opt => opt.MapFrom(src => src.IdPerfil ?? 0))
           .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.GnEmpresa.NOMBRE_EMP))
           .ForMember(dest => dest.SucursalName, opt => opt.MapFrom(src => src.GnSucursal.NombreSuc))
           .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.GnPerfil.Name))
           .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.Nombre))
           .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.Apellido))
           .ForMember(dest => dest.UserImage, opt => opt.MapFrom(src => src.ImagenUsuario))
           .ForMember(dest => dest.PersonalPhone, opt => opt.MapFrom(src => src.TelefonoPersonal))
           .ForMember(dest => dest.IsUserOnline, opt => opt.MapFrom(src => src.OnlineUsuario))
           .ForMember(dest => dest.SucursalId, opt => opt.MapFrom(src => src.CodigoSuc ?? 0))
           .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
           .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.Telefono))
           .ReverseMap();


            CreateMap<Usuario, UpdateUserRequest>()
           .ForMember(dest => dest.CompanyId, opt => opt.MapFrom(src => src.CodigoEmp ?? 0))
           .ForMember(dest => dest.ScheduleId, opt => opt.MapFrom(src => src.IdHorario))
           .ForMember(dest => dest.RoleId, opt => opt.MapFrom(src => src.IdPerfil ?? 0))
           .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.Nombre))
           .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.Apellido))
           .ForMember(dest => dest.UserImage, opt => opt.MapFrom(src => src.ImagenUsuario))
           .ForMember(dest => dest.SucursalId, opt => opt.MapFrom(src => src.CodigoSuc ?? 0))
           .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.TelefonoPersonal))
           .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.TelefonoPersonal))
           .ForMember(dest => dest.UserImage, opt => opt.MapFrom(src => src.ImagenUsuario))
           .ReverseMap();
            #endregion

            #region Permiso 

            CreateMap<GnPermiso, GnPermisoResponse>()
               .ForMember(dest => dest.PermisoId, opt => opt.MapFrom(src => src.IDPermiso))
               .ForMember(dest => dest.RoleId, opt => opt.MapFrom(src => src.IDPerfil))
               .ForMember(dest => dest.MenuId, opt => opt.MapFrom(src => src.IDMenu))
               .ForMember(dest => dest.CompanyId, opt => opt.MapFrom(src => src.Codigo_EMP))
               .ForMember(dest => dest.Create, opt => opt.MapFrom(src => src.Crear))
               .ForMember(dest => dest.Delete, opt => opt.MapFrom(src => src.Eliminar))
               .ForMember(dest => dest.Edit, opt => opt.MapFrom(src => src.Editar))
               .ForMember(dest => dest.Query, opt => opt.MapFrom(src => src.Consultar))
               .ReverseMap();

            CreateMap<GnPermisoRequest, GnPermiso>()
                .ForMember(dest => dest.IDPerfil, opt => opt.MapFrom(src => src.RoleId))
                .ForMember(dest => dest.IDMenu, opt => opt.MapFrom(src => src.MenuId))
                .ForMember(dest => dest.Codigo_EMP, opt => opt.MapFrom(src => src.CompanyId))
                .ForMember(dest => dest.Crear, opt => opt.MapFrom(src => src.Create))
                .ForMember(dest => dest.Eliminar, opt => opt.MapFrom(src => src.Delete))
                .ForMember(dest => dest.Editar, opt => opt.MapFrom(src => src.Edit))
                .ForMember(dest => dest.Consultar, opt => opt.MapFrom(src => src.Query))
                .ReverseMap();
            #endregion

            #region GnSchedule
            CreateMap<GnSchedule, GnScheduleRequest>()
                .ReverseMap();
            CreateMap<GnSchedule, GnScheduleResponse>()
                .ReverseMap();

            CreateMap<GnScheduleUser, GnScheduleUserRequest>()
               .ReverseMap();
            CreateMap<GnScheduleUser, GnScheduleUserResponse>()
                .ForMember(dest => dest.Schedule, opt => opt.MapFrom(src => src.GnSchedule))
                .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.GnEmpresa.NOMBRE_EMP))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Usuario.UserName))
                .ForMember(dest => dest.NameOfUser, opt => opt.MapFrom(src => $"{src.Usuario.Nombre} {src.Usuario.Apellido}"))
                .ReverseMap();
            #endregion

            #endregion

        }
    }

}
