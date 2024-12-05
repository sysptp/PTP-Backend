using BussinessLayer.DTOs.ModuloHelpDesk;
using BussinessLayer.Interfaces.IOtros;
using DataLayer.Models.ModuloHelpDesk;

namespace BussinessLayer.Interfaces.ModuloHelpDesk
{
    public interface IHdkCategoryTicketService : IGenericService<HdkCategoryTicketRequest, HdkCategoryTicketReponse, HdkCategoryTicket>
    {
        Task<List<HdkCategoryTicketReponse>> GetAllWithInclude();
    }
}
