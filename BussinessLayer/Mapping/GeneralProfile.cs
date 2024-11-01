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
            CreateMap<GnPerfil, GnPerfilDto>().ReverseMap();
            #endregion

            #region GnPefil
            CreateMap<GnPerfil, GnPerfilDto>().ReverseMap();
            CreateMap<GnPerfil, SaveGnPerfilDto>();
            #endregion

            #region
            CreateMap<SaveSC_EMP001Dto, SC_EMP001Dto>().ReverseMap();
            #endregion

        }
    }

    
}
