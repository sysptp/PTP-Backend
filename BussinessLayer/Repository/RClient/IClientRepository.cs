using BussinessLayer.DTOs.Cliente;
using DataLayer.Models.Clients;

namespace BussinessLayer.Repository.RClient
{
    public interface IClientRepository
    {
        Task<Client> AddAsync(Client client);
        Task<List<Client>> GetAllAsync(int bussinesId, int position, int count);
    }
}
