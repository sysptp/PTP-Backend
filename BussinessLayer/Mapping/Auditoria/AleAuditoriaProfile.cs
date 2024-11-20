using BussinessLayer.DTOs.Auditoria;
using DataLayer.Models.Auditoria;
using AutoMapper;

namespace BussinessLayer.Mapping.Auditoria
{
    public class AleAuditoriaProfile:Profile
    {
       public AleAuditoriaProfile() {

            CreateMap<AleAuditoriaRequest, AleAuditoria>().ReverseMap();

            CreateMap<AleAuditoriaReponse, AleAuditoria>().ReverseMap();



        }
    }
}
