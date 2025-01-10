using AutoMapper;
using BussinessLayer.DTOs.ModuloCampaña.CmpCliente;
using BussinessLayer.Interfaces.IModuloCampaña;
using BussinessLayer.Interfaces.ModuloCampaña;
using BussinessLayer.Wrappers;
using DataLayer.Models.ModuloCampaña;

namespace BussinessLayer.Services.SModuloCampaña
{
    public class CmpClientService(ICmpClienteRepository cmpClienteRepository, IMapper mapper) : ICmpClientService
    {
        public async Task<Response<CmpClientCreateDto>> CreateClientAsync(CmpClientCreateDto cliente)
        {
            try
            {
                
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

        public Task<Response<string>> UpdateClientAsync(int idCliente, CmpClienteUpdateDto cliente)
        {
            throw new NotImplementedException();
        }
    }
}
