using BussinessLayer.DTOs.ModuloGeneral.Geografia.DRegion;
using BussinessLayer.Interfaces.Services.IOtros;
using DataLayer.Models.ModuloGeneral.Geografia;

namespace BussinessLayer.Interfaces.Services.ModuloGeneral.Geografia
{
    public interface IRegionService : IGenericService<RegionRequest, RegionResponse, Region>
    {

        Task<List<Region>> GetAllIndex();

    }
}