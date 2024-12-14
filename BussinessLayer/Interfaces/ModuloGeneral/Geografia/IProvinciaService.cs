using BussinessLayer.DTOs.ModuloGeneral.Geografia.DProvincia;
using BussinessLayer.Interfaces.IOtros;
using DataLayer.Models.ModuloGeneral.Geografia;

namespace BussinessLayer.Interfaces.ModuloGeneral.Geografia
{
    public interface IProvinciaService : IGenericService<ProvinceRequest, ProvinceResponse, Provincia>
    {
        Task<IEnumerable<Provincia>> GetProvinciasByRegionId(int regionId);
        Task<List<Provincia>> GetAllIndex();
    }
}