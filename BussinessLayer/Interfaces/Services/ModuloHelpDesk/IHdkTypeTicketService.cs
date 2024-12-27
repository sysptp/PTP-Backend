using BussinessLayer.DTOs.ModuloHelpDesk;
using BussinessLayer.Interfaces.Services.IOtros;
using DataLayer.Models.ModuloHelpDesk;

namespace BussinessLayer.Interfaces.Services.ModuloHelpDesk
{
    public interface IHdkTypeTicketService : IGenericService<HdkTypeTicketRequest, HdkTypeTicketReponse, HdkTypeTicket>
    {
    }
}
