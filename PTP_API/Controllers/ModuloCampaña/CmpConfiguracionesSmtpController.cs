using AutoMapper;
using BussinessLayer.DTOs.ModuloCampaña.CmpConfiguraciones;
using BussinessLayer.DTOs.ModuloCampaña.CmpEmail;
using BussinessLayer.FluentValidations.Generic;
using BussinessLayer.Interfaces.ModuloCampaña.Repository;
using BussinessLayer.Wrappers;
using DataLayer.Models.ModuloCampaña;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PTP_API.Controllers.ModuloCampaña
{
    [Route("api/[controller]")]
    [ApiController]
    public class CmpConfiguracionesSmtpController(ICmpConfiguracionesSmtpRepository repository, IMapper mapper, IValidateService<CmpConfiguracionCreateDto> postValidate) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll(int empresaId)
        {
            try
            {
                List<CmpConfiguracionesSmtp> configuracionesSmtps = await repository.GetAllAsync(empresaId);
                if (configuracionesSmtps.Count > 0)
                {
                    List<CmpConfiguracionDto> configuracionDtos = mapper.Map<List<CmpConfiguracionDto>>(configuracionesSmtps);
                    return Ok(Response<List<CmpConfiguracionDto>>.Success(configuracionDtos));
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message, ex);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Create(CmpConfiguracionCreateDto cmpConfiguracionCreateDto)
        {
            try
            {

                List<string> errors = postValidate.Validate(cmpConfiguracionCreateDto);

                if (errors != null) return BadRequest(Response<CmpConfiguracionDto>.BadRequest(errors));

                CmpConfiguracionesSmtp configuracionesSmtp = mapper.Map<CmpConfiguracionesSmtp>(cmpConfiguracionCreateDto);
                await repository.AddAsync(configuracionesSmtp);
                return Created("Recurso creado", Response<CmpConfiguracionCreateDto>.Created(cmpConfiguracionCreateDto));


            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message, ex);
            }
        }
    }
}
