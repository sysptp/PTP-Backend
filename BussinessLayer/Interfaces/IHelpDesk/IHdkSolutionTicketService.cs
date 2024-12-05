using BussinessLayer.DTOs.HelpDesk;
using BussinessLayer.Interfaces.IOtros;
using DataLayer.Models.ModuloHelpDesk;

namespace BussinessLayer.Interfaces.IHelpDesk
{
    public interface IHdkSolutionTicketService:IGenericService<HdkSolutionTicketRequest, HdkSolutionTicketReponse, HdkSolutionTicket>
    {
    }
}
