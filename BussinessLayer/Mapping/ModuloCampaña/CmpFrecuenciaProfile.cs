using AutoMapper;
using BussinessLayer.DTOs.ModuloCampaña.CmpFrecuencia;
using DataLayer.Models.ModuloCampaña;

namespace BussinessLayer.Mapping.ModuloCampaña
{
    public class CmpFrecuenciaProfile : Profile
    {
        public CmpFrecuenciaProfile()
        {
            CreateMap<CmpFrecuencia, CmpFrecuenciaDto>().ReverseMap();
        }
    }
}
