using BussinessLayer.DTOs.ModuloCampaña.CmpCliente;
using BussinessLayer.Wrappers;

namespace BussinessLayer.Interfaces.IModuloCampaña
{
    public interface ICmpClientService
    {
        Task<Response<List<CmpClienteDto>>> GetClientsAsync(int idEmpresa);
        Task<Response<CmpClienteDto>> GetClientByIdAsync(int idCliente, int idEmpresa);
        Task<Response<CmpClientCreateDto>> CreateClientAsync(CmpClientCreateDto cliente);
        Task<Response<string>> DeleteClientAsync(int idCliente, int idEmpresa);
        Task<Response<string>> UpdateClientAsync(int idCliente, CmpClienteUpdateDto cliente);
    }
}
