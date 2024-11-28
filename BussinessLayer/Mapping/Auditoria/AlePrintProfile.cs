using BussinessLayer.DTOs.Auditoria;
using DataLayer.Models.Auditoria;
using AutoMapper;

namespace BussinessLayer.Mapping.Auditoria
{
    public class AlePrintProfile : Profile
    {
        public AlePrintProfile() {

            CreateMap<AlePrintRequest, AlePrint>().ReverseMap();

            CreateMap<AlePrintReponse, AlePrint>().ReverseMap();

        }
    }
}
