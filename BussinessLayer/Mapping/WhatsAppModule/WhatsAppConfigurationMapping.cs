using AutoMapper;
using BussinessLayer.DTOs.WhatsAppModule.WhatsAppConfiguration;
using DataLayer.Models.WhatsAppFeature;

public class WhatsAppConfigurationMapping : Profile
{
    public WhatsAppConfigurationMapping()
    {
        CreateMap<WhatsAppConfigurationDto, CmpWhatsAppConfiguration>()
        .ReverseMap();

    }
}
