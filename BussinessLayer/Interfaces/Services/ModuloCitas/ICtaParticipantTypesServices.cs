
using BussinessLayer.DTOs.ModuloCitas.CtaParticipantTypes;
using BussinessLayer.Interfaces.Services.IOtros;
using DataLayer.Models.ModuloCitas;

namespace BussinessLayer.Interfaces.Services.ModuloCitas
{
    public interface ICtaParticipantTypesServices : IGenericService<CtaParticipantTypesRequest, CtaParticipantTypesResponse, CtaParticipantTypes>
    {
    }
}
