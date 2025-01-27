using BussinessLayer.DTOs.Contactos.TypeContact;
using BussinessLayer.Wrappers;

namespace BussinessLayer.Services.SContactos
{
    public interface IContactService
    {
        Task<Response<List<TypeContactDto>>> GetTypeContactAsync(int bussinesId);
        Task<Response<TypeContactRequest>> CreateContactAsync(TypeContactRequest typeContactRequest);
    }
}
