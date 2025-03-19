using AutoMapper;
using BussinessLayer.DTOs.WhatsAppModule.WhatsAppConfiguration;
using BussinessLayer.Services.WhatsAppService.Contracts;
using BussinessLayer.Wrappers;
using DataLayer.Models.WhatsAppFeature;
using Microsoft.Identity.Client;
namespace BussinessLayer.Services.WhatsAppService.Implementations
{
    /// <summary>
    /// Servicio que maneja la configuración de WhatsApp para los negocios
    /// </summary>
    
    /// <remarks>
    /// Este servicio proporciona funcionalidades para:
    /// - Crear nuevas configuraciones de WhatsApp
    /// - Obtener todas las configuraciones asociadas a un negocio
    /// </remarks>

    public class WhatsAppConfigurationService : IWhatsAppConfigurationService
    {
        private readonly IWhatsAppConfigurationRepository _whatsAppConfigurationRepository;
        private readonly IMapper _mapper;
        public WhatsAppConfigurationService(IWhatsAppConfigurationRepository whatsAppConfigurationRepository, IMapper mapper)
        {
            _whatsAppConfigurationRepository = whatsAppConfigurationRepository;
            _mapper = mapper;
        }
        public async Task<Response<WhatsAppConfigurationDto>> CreateWhatsAppConfiguration(WhatsAppConfigurationDto request)
        {
            try
            {
                CmpWhatsAppConfiguration whatsAppConfiguration = _mapper.Map<CmpWhatsAppConfiguration>(request);
                whatsAppConfiguration.UsuarioAdicion = request.AddedBy;
                
                await _whatsAppConfigurationRepository.CreateWhatsAppConfigurationAsync(whatsAppConfiguration);
                return Response<WhatsAppConfigurationDto>.Success(request);
            }
            catch (Exception ex)
            {
                return Response<WhatsAppConfigurationDto>.ServerError(ex.Message);
            }
        }

        public async  Task<Response<List<WhatsAppConfigurationDto>>> GetAllWhatsAppConfigurations(int bussinessId)
        {
            try{

                if(bussinessId <= 0){
                    return Response<List<WhatsAppConfigurationDto>>
                    .BadRequest(new List<string> { "El ID del negocio debe ser mayor a 0" });
                }
                List<CmpWhatsAppConfiguration> whatsAppConfigurations = await _whatsAppConfigurationRepository.GetAllWhatsAppConfigurationAsync(bussinessId);

                if(whatsAppConfigurations.Count == 0){
                    return Response<List<WhatsAppConfigurationDto>>.NoContent();
                }

                List<WhatsAppConfigurationDto> whatsAppConfigurationsDto = whatsAppConfigurations.Select(x => new WhatsAppConfigurationDto(
                    x.ConfigurationId,
                    x.BussinessId,
                    x.AccountSid,
                    x.AuthToken,
                    x.WhatsAppNumber,
                    x.UsuarioAdicion
                )).ToList();

                return Response<List<WhatsAppConfigurationDto>>.Success(whatsAppConfigurationsDto);
            }
            catch (Exception ex)
            {
                
                return Response<List<WhatsAppConfigurationDto>>
                .ServerError(@$"Ocurrio un error al obtener las configuracions de WhatsApp");
            }
        }

        /// <summary>
        /// Obtiene una configuración específica de WhatsApp por su ID y el ID del negocio
        /// </summary>
        /// <param name="configurationId">ID de la configuración de WhatsApp</param>
        /// <param name="businessId">ID del negocio</param>
        /// <returns>Respuesta que contiene la configuración de WhatsApp solicitada</returns>
        /// <remarks>
        /// Este método:
        /// - Busca una configuración específica de WhatsApp usando el ID de configuración y el ID del negocio
        /// - Retorna NotFound si no se encuentra la configuración
        /// - Mapea la configuración encontrada a un DTO antes de retornarla
        /// </remarks>
        public async Task<Response<WhatsAppConfigurationDto>> GetWhatsAppConfiguration(int configurationId, int businessId)
        {
            try
            {
                if(configurationId <= 0 || businessId <= 0){
                    return Response<WhatsAppConfigurationDto>
                    .BadRequest(new List<string> { "El ID de la configuración y el ID del negocio deben ser mayores a 0" });
                }
                var whatsAppConfiguration = await _whatsAppConfigurationRepository.GetWhatsAppConfigurationAsync(configurationId, businessId);

                if (whatsAppConfiguration == null)
                {
                    return Response<WhatsAppConfigurationDto>.NoContent();
                }

                WhatsAppConfigurationDto whatsAppConfigurationDto = new WhatsAppConfigurationDto(
                    whatsAppConfiguration.ConfigurationId,
                    whatsAppConfiguration.BussinessId,
                    whatsAppConfiguration.AccountSid,
                    whatsAppConfiguration.AuthToken,
                    whatsAppConfiguration.WhatsAppNumber,
                    whatsAppConfiguration.UsuarioAdicion
                );
                return Response<WhatsAppConfigurationDto>.Success(whatsAppConfigurationDto);
            }
            catch (Exception ex)
            {
                return Response<WhatsAppConfigurationDto>.ServerError(ex.Message);
            }
        }
    }
}
