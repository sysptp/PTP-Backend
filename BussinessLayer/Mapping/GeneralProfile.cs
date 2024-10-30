using AutoMapper;
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
        }
    }
}
