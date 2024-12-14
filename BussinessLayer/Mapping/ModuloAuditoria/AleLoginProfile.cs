using AutoMapper;
using DataLayer.Models.ModuloAuditoria;
using BussinessLayer.DTOs.ModuloAuditoria;

namespace BussinessLayer.Mapping.ModuloAuditoria
{
    public class AleLoginProfile : Profile
    {
        public AleLoginProfile()
        {

            CreateMap<AleLoginRequest, AleLogin>().ReverseMap();

            CreateMap<AleLoginReponse, AleLogin>().ReverseMap();
        }
    }
}
