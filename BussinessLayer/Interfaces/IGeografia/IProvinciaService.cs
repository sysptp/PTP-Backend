using BussinessLayer.DTOs.Geografia.DProvincia;
using BussinessLayer.Interfaces.IOtros;
using DataLayer.Models.Geografia;

namespace BussinessLayer.Interfaces.IGeografia
{
    public interface IProvinciaService : IGenericService<ProvinceRequest, ProvinceResponse, Provincia>
    {
        Task<IEnumerable<Provincia>> GetProvinciasByRegionId(int regionId);
        Task<List<Provincia>> GetAllIndex();
    }
}