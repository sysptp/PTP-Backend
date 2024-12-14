using AutoMapper;
using DataLayer.Models.ModuloAuditoria;
using BussinessLayer.DTOs.ModuloAuditoria;

namespace BussinessLayer.Mapping.ModuloAuditoria
{
    public class AleAuditoriaProfile : Profile
    {
        public AleAuditoriaProfile()
        {

            CreateMap<AleAuditoriaRequest, AleAuditoria>().ReverseMap();

            CreateMap<AleAuditoriaReponse, AleAuditoria>().ReverseMap();



        }
    }
}
