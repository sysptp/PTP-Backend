using BussinessLayer.DTOs.ModuloHelpDesk;
using BussinessLayer.Interfaces.IOtros;
using DataLayer.Models.ModuloHelpDesk;

namespace BussinessLayer.Interfaces.ModuloHelpDesk
{
    public interface IHdkTicketsService : IGenericService<HdkTicketsRequest, HdkTicketsReponse, HdkTickets>
    {
    }
}
