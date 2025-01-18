using AutoMapper;
using BussinessLayer.DTOs.ModuloCampaña.CmpCliente;
using BussinessLayer.FluentValidations.Generic;
using BussinessLayer.Interfaces.ModuloCampaña.Repository;
using BussinessLayer.Interfaces.ModuloCampaña.Services;
using BussinessLayer.Wrappers;
using DataLayer.Models.Empresa;
using DataLayer.Models.ModuloCampaña;
using FluentValidation;

namespace BussinessLayer.Services.SModuloCampaña
{
    public class CmpClientService(ICmpClienteRepository cmpClienteRepository, IMapper mapper, IValidateService<CmpClientCreateDto> validator) : ICmpClientService
    {
        public async Task<Response<CmpClientCreateDto>> CreateClientAsync(CmpClientCreateDto cliente)
        {
            try
            {
                List<string> errors = validator.Validate(cliente);
                if (errors != null && errors.Count > 0) return Response<CmpClientCreateDto>.BadRequest(errors);

                CmpCliente cmpCliente = mapper.Map<CmpCliente>(cliente);
                await cmpClienteRepository.AddAsync(cmpCliente);
                return Response<CmpClientCreateDto>.Created(cliente);
            }
            catch (Exception ex)
            {
                return Response<CmpClientCreateDto>.ServerError(ex.Message);
            }
        }
        public async Task<Response<string>> DeleteClientAsync(int idCliente, int idEmpresa)
        {
            try
            {
                CmpCliente? cmpCliente = await cmpClienteRepository.GetByIdAsync(idCliente, idEmpresa);

                if (cmpCliente == null) return Response<string>.BadRequest(new List<string> { $"Usuario con ID: {idCliente} no fue encontrado" });

                await cmpClienteRepository.DeleteAsync(idCliente);
                return Response<string>.NoContent("Recurso eliminado");
            }
            catch (Exception ex)
            {
                return Response<string>.ServerError(ex.Message);
            }
        }

        public async Task<Response<CmpClienteDto>> GetClientByIdAsync(int idCliente, int idEmpresa)
        {
            try
            {
                CmpCliente? client = await cmpClienteRepository.GetByIdAsync(idCliente, idEmpresa);

                if (client != null)
                {
                    CmpClienteDto clienteDto = mapper.Map<CmpClienteDto>(client);
                    return Response<CmpClienteDto>.Success(clienteDto);
                }
                return Response<CmpClienteDto>.NoContent();
            }
            catch (Exception ex)
            {
                return Response<CmpClienteDto>.ServerError(ex.Message);
            }
        }
        public async Task<Response<List<CmpClienteDto>>> GetClientsAsync(int idEmpresa)
        {
            try
            {
                IEnumerable<CmpCliente>? clients = await cmpClienteRepository.GetAllAsync(idEmpresa);

                if (clients != null)
                {

                    List<CmpClienteDto> cmpClienteDtos = mapper.Map<List<CmpClienteDto>>(clients.ToList());
                    return Response<List<CmpClienteDto>>.Success(cmpClienteDtos);
                }
                return Response<List<CmpClienteDto>>.NoContent();
            }
            catch (Exception ex)
            {
                return Response<List<CmpClienteDto>>.ServerError(ex.Message);
            }
        }

        public async Task<Response<string>> UpdateClientAsync(int idCliente, CmpClienteUpdateDto cliente)
        {
            try
            {
                CmpCliente? existingClient = await cmpClienteRepository.GetByIdAsync(idCliente, cliente.EmpresaId);
                if (existingClient == null) return Response<string>.BadRequest(new List<string> { $"Usuario con ID: {idCliente} no fue encontrado" });
                CmpCliente cmpCliente = mapper.Map<CmpCliente>(cliente);
                await cmpClienteRepository.UpdateAsync(cmpCliente);
                return Response<string>.NoContent("Usuario editado");
            }
            catch (Exception ex)
            {
                return Response<string>.ServerError(ex.Message);
            }
        }
    }
}
