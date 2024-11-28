using BussinessLayer.DTOs.HelpDesk;
using BussinessLayer.Interfaces.IOtros;
using DataLayer.Models.HelpDesk;

namespace BussinessLayer.Interfaces.IHelpDesk
{
    public interface IHdkCategoryTicketService : IGenericService<HdkCategoryTicketRequest, HdkCategoryTicketReponse, HdkCategoryTicket>
    {
        Task<List<HdkCategoryTicketReponse>> GetAllWithInclude();
    }
}
