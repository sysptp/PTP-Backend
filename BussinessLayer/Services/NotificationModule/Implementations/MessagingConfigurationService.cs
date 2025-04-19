using AutoMapper;
using BussinessLayer.DTOs.NotificationModule.MessagingConfiguration;
using BussinessLayer.Services.WhatsAppService.Contracts;
using BussinessLayer.Wrappers;
using DataLayer.Models.MessagingModule;

namespace BussinessLayer.Services.WhatsAppService.Implementations
{
    public class MessagingConfigurationService(IMessagingConfigurationRepository messagingConfigurationRepository, IMapper mapper) : IMessagingConfigurationService
    {
        public async Task<Response<CreateMessagingConfigurationDto>> CreateAsync(CreateMessagingConfigurationDto request)
        {
            try
            {
                MessagingConfiguration messagingConfiguration = mapper.Map<MessagingConfiguration>(request);
                messagingConfiguration.UsuarioAdicion = request.AddedBy;
                
                await messagingConfigurationRepository.CreateAsync(messagingConfiguration);
                return Response<CreateMessagingConfigurationDto>.Success(request);
            } 
            catch (Exception ex)
            {
                return Response<CreateMessagingConfigurationDto>.ServerError(ex.Message);
            }
        }

        public async  Task<Response<List<MessagingConfigurationDto>>> GetAllAsync(int bussinessId)
        {
            try{

                if(bussinessId <= 0){
                    return Response<List<MessagingConfigurationDto>>
                    .BadRequest(new List<string> { "El ID del negocio debe ser mayor a 0" });
                }
                List<MessagingConfiguration> messagingConfigurations = await messagingConfigurationRepository.GetAllAsync(bussinessId);

                if(messagingConfigurations.Count == 0){
                    return Response<List<MessagingConfigurationDto>>.NoContent();
                }

                List<MessagingConfigurationDto> messagingConfigurationDtos = messagingConfigurations.Select(x => new MessagingConfigurationDto(
                    x.ConfigurationId,
                    x.BussinessId,
                    x.AccountSid,
                    x.AuthToken,
                    x.WhatsAppNumber,
                    x.SmsNumber,
                    x.UsuarioAdicion
                )).ToList();

                return Response<List<MessagingConfigurationDto>>.Success(messagingConfigurationDtos);
            }
            catch (Exception ex)
            {
                
                return Response<List<MessagingConfigurationDto>>
                .ServerError(@$"Ocurrio un error al obtener las configuracions de WhatsApp");
            }
        }

        
        public async Task<Response<MessagingConfigurationDto>> GetByIdAsync(int configurationId, int businessId)
        {
            try
            {
                if(configurationId <= 0 || businessId <= 0){
                    return Response<MessagingConfigurationDto>
                    .BadRequest(new List<string> { "El ID de la configuración y el ID del negocio deben ser mayores a 0" });
                }

                MessagingConfiguration messagingConfiguration = await messagingConfigurationRepository.GetByIdAsync(configurationId, businessId);

                if (messagingConfiguration == null)
                {
                    return Response<MessagingConfigurationDto>.NoContent();
                }

                MessagingConfigurationDto messagingConfigurationDto = new MessagingConfigurationDto(
                    messagingConfiguration.ConfigurationId,
                    messagingConfiguration.BussinessId,
                    messagingConfiguration.AccountSid,
                    messagingConfiguration.AuthToken,
                    messagingConfiguration.WhatsAppNumber,
                    messagingConfiguration.SmsNumber,
                    messagingConfiguration.UsuarioAdicion
                );
                return Response<MessagingConfigurationDto>.Success(messagingConfigurationDto);
            }
            catch (Exception ex)
            {
                return Response<MessagingConfigurationDto>.ServerError(ex.Message);
            }
        }
    }
}
