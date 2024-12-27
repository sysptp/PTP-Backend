using BussinessLayer.DTOs.ModuloVentas.Cliente;

namespace BussinessLayer.Repository.ModuloVentas.RClient
{
    public interface IClientRepository
    {
        Task CreateAsync(CreateClientDto client);
    }
}
