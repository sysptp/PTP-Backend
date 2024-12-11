using Azure;
using BussinessLayer.DTOs.Cliente;
using DataLayer.Models.Contactos;

namespace BussinessLayer.Services.SCliente
{
    public interface IClientContactRepository
    {
        Task<ClientContact> CreateAsync(ClientContactDto contact);
    }
}
