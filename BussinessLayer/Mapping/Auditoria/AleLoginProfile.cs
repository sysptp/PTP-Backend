using BussinessLayer.DTOs.Auditoria;
using DataLayer.Models.Auditoria;
using AutoMapper;

namespace BussinessLayer.Mapping.Auditoria
{
    public class AleLoginProfile:Profile
    {
        public AleLoginProfile() {

            CreateMap<AleLoginRequest, AleLogin>().ReverseMap();

            CreateMap<AleLoginReponse, AleLogin>().ReverseMap();
        }
    }
}
