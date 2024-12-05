using BussinessLayer.DTOs.ModuloHelpDesk;
using BussinessLayer.Interfaces.IOtros;
using DataLayer.Models.ModuloHelpDesk;

namespace BussinessLayer.Interfaces.ModuloHelpDesk
{
    public interface IHdkSolutionTicketService : IGenericService<HdkSolutionTicketRequest, HdkSolutionTicketReponse, HdkSolutionTicket>
    {
    }
}
