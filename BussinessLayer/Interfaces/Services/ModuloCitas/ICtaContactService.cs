using BussinessLayer.DTOs.ModuloCitas.CtaContacts;
using BussinessLayer.Interfaces.Services.IOtros;
using DataLayer.Models.ModuloCitas;

namespace BussinessLayer.Interfaces.Services.ModuloCitas
{
    public interface ICtaContactService : IGenericService<CtaContactRequest,CtaContactResponse, CtaContacts>
    {
    }
}
