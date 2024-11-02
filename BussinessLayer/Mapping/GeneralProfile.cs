using AutoMapper;
using BussinessLayer.Dtos.Account;
using BussinessLayer.DTOs.Empresas;
using BussinessLayer.DTOs.Seguridad;
using DataLayer.Models.Entities;

namespace TaskMaster.Core.Application.Mapping
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {

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

        }
    }


}
