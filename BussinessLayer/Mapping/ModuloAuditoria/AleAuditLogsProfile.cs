using AutoMapper;
using BussinessLayer.DTOs.ModuloAuditoria;
using DataLayer.Models.ModuloAuditoria;

namespace BussinessLayer.Mapping.ModuloAuditoria
{
    public class AleAuditLogsProfile : Profile
    {
        public AleAuditLogsProfile() 
        {
            CreateMap<AleAuditLogRequest, AleAuditLog>()
                .ReverseMap();
            CreateMap<AleAuditLogResponse, AleAuditLog>()
                .ReverseMap();
        }
    }
}
