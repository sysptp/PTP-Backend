using AutoMapper;
using BussinessLayer.DTOs.Configuracion.Geografia.DProvincia;
using BussinessLayer.Interfaces.IGeografia;
using BussinessLayer.Interfaces.Repository.Geografia;
using DataLayer.Models.Geografia;

namespace BussinessLayer.Services.SGeografia
{
    public class ProvinciaService : GenericService<ProvinceRequest, ProvinceResponse, Provincia>, IProvinciaService
    {
        private readonly IProvinciaRepository _repository;
        public ProvinciaService(IProvinciaRepository repository, IMapper mapper) : base(repository, mapper)
        {
            _repository = repository;
        }

        public async Task<List<Provincia>> GetAllIndex()
        {
           return await _repository.GetAllIndex();
        }

        public async Task<IEnumerable<Provincia>> GetProvinciasByRegionId(int regionId)
        {
            return await _repository.GetProvinciasByRegionId(regionId);
        }
    }
}
