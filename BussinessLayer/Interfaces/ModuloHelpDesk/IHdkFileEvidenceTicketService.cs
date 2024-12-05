using BussinessLayer.DTOs.ModuloHelpDesk;
using BussinessLayer.Interfaces.IOtros;
using DataLayer.Models.ModuloHelpDesk;

namespace BussinessLayer.Interfaces.ModuloHelpDesk
{
    public interface IHdkFileEvidenceTicketService : IGenericService<HdkFileEvidenceTicketRequest, HdkFileEvidenceTicketReponse, HdkFileEvidenceTicket>
    {
    }
}
