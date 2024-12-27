
using AutoMapper;
using BussinessLayer.DTOs.ModuloAuditoria;
using DataLayer.Models.ModuloAuditoria;

namespace BussinessLayer.Mapping.ModuloAuditoria
{
    public class AleAuditTableControlProfile : Profile
    {
        public AleAuditTableControlProfile() {

            CreateMap<AleAuditTableControlRequest, AleAuditTableControl>()
                .ReverseMap();
            CreateMap<AleAuditTableControlResponse, AleAuditTableControl>()
                .ReverseMap();
        }
    }
}
