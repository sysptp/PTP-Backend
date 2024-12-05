using BussinessLayer.DTOs.Cliente;
using BussinessLayer.Wrappers;
using DataLayer.Models.Clients;

namespace BussinessLayer.Interfaces.IClient
{
    public interface IClientService
    {
        Task<Response<CreateClientDto>> CreateAsync(CreateClientDto dto);
        Task<Response<List<Client>>> GetAllAsync(int bussinesCode);
    }
}
