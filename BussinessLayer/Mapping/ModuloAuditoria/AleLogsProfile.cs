using AutoMapper;
using DataLayer.Models.ModuloAuditoria;
using BussinessLayer.DTOs.ModuloAuditoria;

namespace BussinessLayer.Mapping.ModuloAuditoria
{
    public class AleLogsProfile : Profile
    {
        public AleLogsProfile()
        {

            CreateMap<AleLogsRequest, AleLogs>().ReverseMap();

            CreateMap<AleLogsReponse, AleLogs>().ReverseMap();

        }
    }
}
