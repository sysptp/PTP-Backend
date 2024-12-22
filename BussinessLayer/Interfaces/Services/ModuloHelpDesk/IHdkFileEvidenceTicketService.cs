using BussinessLayer.DTOs.ModuloHelpDesk;
using BussinessLayer.Interfaces.Services.IOtros;
using DataLayer.Models.ModuloHelpDesk;

namespace BussinessLayer.Interfaces.Services.ModuloHelpDesk
{
    public interface IHdkFileEvidenceTicketService : IGenericService<HdkFileEvidenceTicketRequest, HdkFileEvidenceTicketReponse, HdkFileEvidenceTicket>
    {
    }
}
