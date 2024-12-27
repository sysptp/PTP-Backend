using AutoMapper;
using DataLayer.Models.ModuloAuditoria;
using BussinessLayer.DTOs.ModuloAuditoria;

namespace BussinessLayer.Mapping.ModuloAuditoria
{
    public class AleAuditoriaProfile : Profile
    {
        public AleAuditoriaProfile()
        {

            CreateMap<AleBitacoraRequest, AleAuditoria>().ReverseMap();

            CreateMap<AleBitacoraReponse, AleAuditoria>().ReverseMap();



        }
    }
}
