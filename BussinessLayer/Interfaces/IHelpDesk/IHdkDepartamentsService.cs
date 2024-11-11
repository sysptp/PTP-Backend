using BussinessLayer.DTOs.HelpDesk;
using BussinessLayer.Interfaces.IOtros;
using DataLayer.Models.HelpDesk;

namespace BussinessLayer.Interfaces.IHelpDesk
{
    public interface IHdkDepartamentsService : IGenericService<HdkDepartamentsRequest, HdkDepartamentsReponse, HdkDepartaments>
    {
    }
}
