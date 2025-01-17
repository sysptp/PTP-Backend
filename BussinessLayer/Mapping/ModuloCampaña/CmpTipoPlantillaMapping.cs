using AutoMapper;
using BussinessLayer.DTOs.ModuloCampaña.CmpTipoPlantillas;
using DataLayer.Models.ModuloCampaña;

namespace BussinessLayer.Mapping.ModuloCampaña
{
    public class CmpTipoPlantillaMapping : Profile
    {
        public CmpTipoPlantillaMapping()
        {
            CreateMap<CmpTipoPlantilla, CmpTipoPlantillaCreateDto>()
                .ReverseMap();


            CreateMap<CmpTipoPlantilla, CmpTipoPlantillaDto>()
                .ReverseMap();


            CreateMap<CmpTipoPlantilla, CmpTipoPlantillaUpdateDto>()
                .ReverseMap();
        }
    }
}
