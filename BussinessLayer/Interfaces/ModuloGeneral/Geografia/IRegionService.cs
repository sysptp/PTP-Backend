using BussinessLayer.DTOs.ModuloGeneral.Geografia.DRegion;
using BussinessLayer.Interfaces.IOtros;
using DataLayer.Models.ModuloGeneral.Geografia;

namespace BussinessLayer.Interfaces.ModuloGeneral.Geografia
{
    public interface IRegionService : IGenericService<RegionRequest, RegionResponse, Region>
    {

        Task<List<Region>> GetAllIndex();

    }
}