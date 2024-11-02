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
            CreateMap<GnPerfil, GnPerfilResponse>().ReverseMap();
            CreateMap<GnPerfil, GnPerfilRequest>().ReverseMap();
            #endregion

            #region
            CreateMap<SaveGnEmpresaDto, GnEmpresaDto>().ReverseMap();
            #endregion

        }
    }

    
}
