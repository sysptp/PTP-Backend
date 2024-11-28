using BussinessLayer.DTOs.Auditoria;
using DataLayer.Models.Auditoria;
using AutoMapper;

namespace BussinessLayer.Mapping.Auditoria
{
    public class AleLogsProfile:Profile
    {
        public AleLogsProfile() {

            CreateMap<AleLogsRequest, AleLogs>().ReverseMap();

            CreateMap<AleLogsReponse, AleLogs>().ReverseMap();

        }
    }
}
