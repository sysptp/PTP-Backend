using AutoMapper;
using BussinessLayer.DTOs.ModuloCampaña.CmpPlantillas;
using DataLayer.Models.ModuloCampaña;

namespace BussinessLayer.Mapping.ModuloCampaña
{
    public class CmpPlantillaMapping : Profile
    {
        public CmpPlantillaMapping()
        {
            CreateMap<CmpPlantillas, CmpPlantillaCreateDto>()
                .ReverseMap();
            
            CreateMap<CmpPlantillas, CmpPlantillaDto>()
                .ReverseMap();
            
            CreateMap<CmpPlantillas, CmpPlantillaUpdateDto>()
                .ReverseMap();
        }
    }
}
