using AutoMapper;
using BussinessLayer.DTOs.ModuloCampaña.CmpContacto;
using BussinessLayer.FluentValidations;
using BussinessLayer.Interfaces.IModuloCampaña;
using BussinessLayer.Interfaces.ModuloCampaña;
using BussinessLayer.Repository.RCampaña;
using BussinessLayer.Wrappers;
using DataLayer.Models.ModuloCampaña;
using FluentValidation;

namespace BussinessLayer.Services.SModuloCampaña
{
    

    public class CmpContactoService(ICmpContactosRepository cmpContactoRepository, IMapper mapper, IValidateService<CmpContactoCreateDto> validator) : ICmpContactoService
    {
        public async Task<Response<CmpContactoCreateDto>> CreateContactoAsync(CmpContactoCreateDto contacto)
        {
            try
            {
                List<string> errors = validator.Validate(contacto);
                if (errors != null && errors.Count > 0) return Response<CmpContactoCreateDto>.BadRequest(errors);

                CmpContactos cmpContacto = mapper.Map<CmpContactos>(contacto);
                await cmpContactoRepository.AddAsync(cmpContacto);
                return Response<CmpContactoCreateDto>.Created(contacto);
            }
            catch (Exception ex)
            {
                return Response<CmpContactoCreateDto>.ServerError(ex.Message);
            }
        }

        public async Task<Response<string>> DeleteContactoAsync(long idContacto, long idEmpresa)
        {
            try
            {
                CmpContactos? cmpContacto = await cmpContactoRepository.GetByIdAsync(idContacto, idEmpresa);

                if (cmpContacto == null) return Response<string>.BadRequest(new List<string> { $"Contacto con ID: {idContacto} no fue encontrado" });

                await cmpContactoRepository.DeleteAsync(idContacto);
                return Response<string>.NoContent("Recurso eliminado");
            }
            catch (Exception ex)
            {
                return Response<string>.ServerError(ex.Message);
            }
        }

        public async Task<Response<CmpContactoDto>> GetContactoByIdAsync(long idContacto, long idEmpresa)
        {
            try
            {
                CmpContactos? contacto = await cmpContactoRepository.GetByIdAsync(idContacto, idEmpresa);

                if (contacto != null)
                {
                    CmpContactoDto contactoDto = mapper.Map<CmpContactoDto>(contacto);
                    return Response<CmpContactoDto>.Success(contactoDto);
                }
                return Response<CmpContactoDto>.NoContent();
            }
            catch (Exception ex)
            {
                return Response<CmpContactoDto>.ServerError(ex.Message);
            }
        }

        public async Task<Response<List<CmpContactoDto>>> GetContactosAsync(long idEmpresa)
        {
            try
            {
                IEnumerable<CmpContactos>? contactos = await cmpContactoRepository.GetAllAsync(idEmpresa);

                if (contactos != null)
                {
                    List<CmpContactoDto> cmpContactoDtos = mapper.Map<List<CmpContactoDto>>(contactos.ToList());
                    return Response<List<CmpContactoDto>>.Success(cmpContactoDtos);
                }
                return Response<List<CmpContactoDto>>.NoContent();
            }
            catch (Exception ex)
            {
                return Response<List<CmpContactoDto>>.ServerError(ex.Message);
            }
        }

        public async Task<Response<string>> UpdateContactoAsync(long idContacto, CmpContactoUpdateDto contacto)
        {
            try
            {
                CmpContactos? existingContacto = await cmpContactoRepository.GetByIdAsync(idContacto, contacto.EmpresaId);
                if (existingContacto == null) return Response<string>.BadRequest(new List<string> { $"Contacto con ID: {idContacto} no fue encontrado" });

                CmpContactos cmpContacto = mapper.Map<CmpContactos>(contacto);
                await cmpContactoRepository.UpdateAsync(cmpContacto);
                return Response<string>.NoContent("Contacto editado");
            }
            catch (Exception ex)
            {
                return Response<string>.ServerError(ex.Message);
            }
        }
    }
}
