using AutoMapper;
using DataLayer.Models.ModuloAuditoria;
using BussinessLayer.DTOs.ModuloAuditoria;

namespace BussinessLayer.Mapping.ModuloAuditoria
{
    public class AlePrintProfile : Profile
    {
        public AlePrintProfile()
        {

            CreateMap<AlePrintRequest, AlePrint>().ReverseMap();

            CreateMap<AlePrintReponse, AlePrint>().ReverseMap();

        }
    }
}
