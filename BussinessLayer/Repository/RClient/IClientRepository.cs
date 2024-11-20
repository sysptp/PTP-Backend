using BussinessLayer.DTOs.Cliente;
using DataLayer.Models.Clients;

namespace BussinessLayer.Repository.RClient
{
    public interface IClientRepository
    {
        Task CreateAsync(CreateClientDto client);
    }
}
