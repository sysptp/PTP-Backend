using BussinessLayer.DTOs.ModuloCitas.CtaMeetingPlace;
using BussinessLayer.Interfaces.IOtros;
using DataLayer.Models.Modulo_Citas;

namespace BussinessLayer.Interface.Modulo_Citas
{
    public interface ICtaMeetingPlaceService : IGenericService<CtaMeetingPlaceRequest, CtaMeetingPlaceResponse, CtaMeetingPlace>
    {
    }
}
