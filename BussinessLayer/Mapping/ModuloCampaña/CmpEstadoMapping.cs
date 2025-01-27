using AutoMapper;
using BussinessLayer.DTOs.ModuloCampaña.CmpEstado;
using DataLayer.Models.ModuloCampaña;

namespace BussinessLayer.Mapping.ModuloCampaña
{
    public class CmpEstadoMapping : Profile
    {
        public CmpEstadoMapping()
        {
            CreateMap<CmpEstadoDto, CmpEstado>()
                .ReverseMap();
        }
    }
}
