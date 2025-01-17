using AutoMapper;
using BussinessLayer.DTOs.ModuloCampaña.CmpConfiguraciones;
using DataLayer.Models.ModuloCampaña;
namespace BussinessLayer.Mapping.ModuloCampaña
{
    public class CmpConfiguracionMappingProfile : Profile
    {
        public CmpConfiguracionMappingProfile()
        {
            CreateMap<CmpConfiguracionesSmtp, CmpConfiguracionCreateDto>()
                .ReverseMap();
            
            CreateMap<CmpConfiguracionesSmtp, CmpConfiguracionDto>()
                .ReverseMap();
            
            CreateMap<CmpConfiguracionesSmtp, CmpConfiguracionUpdateDto>()
                .ReverseMap();
        }
    }
}
