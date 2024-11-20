using BussinessLayer.DTOs.Configuracion.Geografia.DRegion;
using BussinessLayer.Interfaces.IOtros;
using DataLayer.Models.Geografia;

namespace BussinessLayer.Interfaces.IGeografia
{
    public interface IRegionService : IGenericService<RegionRequest, RegionResponse,Region>
    {

        Task<List<Region>> GetAllIndex();

    }
}