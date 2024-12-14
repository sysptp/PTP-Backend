using AutoMapper;
using BussinessLayer.DTOs.ModuloGeneral.Geografia.DProvincia;
using BussinessLayer.Interfaces.ModuloGeneral.Geografia;
using BussinessLayer.Interfaces.Repository.ModuloGeneral.Geografia;
using DataLayer.Models.ModuloGeneral.Geografia;

namespace BussinessLayer.Services.ModuloGeneral.Geografia
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
