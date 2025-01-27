using BussinessLayer.DTOs.ModuloVentas.Cliente;
using BussinessLayer.Wrappers;
using DataLayer.Models.Clients;

namespace BussinessLayer.Interfaces.Services.ModuloVentas.IClient
{
    public interface IClientService
    {
        Task<Response<CreateClientDto>> CreateAsync(CreateClientDto dto);
        Task<Response<List<Client>>> GetAllAsync(int bussinesId, int pageSize, int pageCount);
        Task<Response<Client>> GetByIdAsync(int clientId);
    }
}
