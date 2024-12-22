using BussinessLayer.DTOs.ModuloCitas.CtaMeetingPlace;
using BussinessLayer.Interfaces.Services.IOtros;
using DataLayer.Models.Modulo_Citas;

namespace BussinessLayer.Interfaces.Services.ModuloCitas
{
    public interface ICtaMeetingPlaceService : IGenericService<CtaMeetingPlaceRequest, CtaMeetingPlaceResponse, CtaMeetingPlace>
    {
    }
}
