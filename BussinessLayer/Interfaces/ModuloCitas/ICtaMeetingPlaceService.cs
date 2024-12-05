using BussinessLayer.DTOs.ModuloCitas.CtaMeetingPlace;
using BussinessLayer.Interfaces.IOtros;

namespace DataLayer.Models.Modulo_Citas
{
    public interface ICtaMeetingPlaceService : IGenericService<CtaMeetingPlaceRequest, CtaMeetingPlaceResponse, CtaMeetingPlace>
    {
    }
}
