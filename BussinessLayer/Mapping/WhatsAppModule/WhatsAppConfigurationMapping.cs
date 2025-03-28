using AutoMapper;
using BussinessLayer.DTOs.NotificationModule.MessagingConfiguration;
using DataLayer.Models.MessagingModule;

public class WhatsAppConfigurationMapping : Profile
{
    public WhatsAppConfigurationMapping()
    {
            CreateMap<MessagingConfigurationDto, MessagingConfiguration>()
            .ReverseMap();

                CreateMap<CreateMessagingConfigurationDto, MessagingConfiguration>()
            .ReverseMap();

    }
}
