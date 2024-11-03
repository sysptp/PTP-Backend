using AutoMapper;
using BussinessLayer.Dtos.Account;
using BussinessLayer.DTOs.Empresas;
using BussinessLayer.DTOs.Geografia.DMunicipio;
using BussinessLayer.DTOs.Geografia.DPais;
using BussinessLayer.DTOs.Geografia.DProvincia;
using BussinessLayer.DTOs.Geografia.DRegion;
using BussinessLayer.DTOs.Seguridad;
using DataLayer.Models.Entities;
using DataLayer.Models.Geografia;

namespace TaskMaster.Core.Application.Mapping
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            #region AGREGADOS POR Domingo 
            #region GnPerfil
            CreateMap<GnPerfil, GnPerfilResponse>().ReverseMap();
            #endregion

            #region GnPefil
            CreateMap<GnPerfil, GnPerfilResponse>()
               .ForMember(dest => dest.IdRole, opt => opt.MapFrom(src => src.Id))
               .ForMember(dest => dest.IDEmpresa, opt => opt.MapFrom(src => src.IDEmpresa.HasValue ? src.IDEmpresa.Value : 0))
               .ReverseMap();

            CreateMap<GnPerfilRequest, GnPerfil>()
                .ForMember(dest => dest.IDEmpresa, opt => opt.MapFrom(src => (long?)src.IDEmpresa))
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
            CreateMap<CountryResponse, Pais>()
                .ForMember(dest => dest.Nombre, opt => opt.MapFrom(x => x.Name))
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
                .ReverseMap();

            #endregion
            #endregion

        }
    }

}
