using BussinessLayer.DTOs.Cliente;
using BussinessLayer.Interfaces.IClient;
using BussinessLayer.Wrappers;

namespace BussinessLayer.Services.SCliente
{
    public class ClientService : IClientService
    {
        public Task<Response<CreateClientDto>> CreateAsync(CreateClientDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
