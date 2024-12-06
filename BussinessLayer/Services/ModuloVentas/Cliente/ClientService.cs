using BussinessLayer.DTOs.ModuloVentas.Cliente;
using BussinessLayer.Interfaces.ModuloVentas.IClient;
using BussinessLayer.Wrappers;

namespace BussinessLayer.Services.ModuloVentas.Cliente
{
    public class ClientService : IClientService
    {
        public Task<Response<CreateClientDto>> CreateAsync(CreateClientDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
