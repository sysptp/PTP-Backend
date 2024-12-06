using BussinessLayer.DTOs.Cliente;
using BussinessLayer.Wrappers;

namespace BussinessLayer.Interfaces.ModuloVentas.IClient
{
    public interface IClientService
    {
        Task<Response<CreateClientDto>> CreateAsync(CreateClientDto dto);
    }
}
