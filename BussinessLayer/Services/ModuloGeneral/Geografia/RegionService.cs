using AutoMapper;
using BussinessLayer.DTOs.ModuloGeneral.Geografia.DRegion;
using BussinessLayer.Interfaces.ModuloGeneral.Geografia;
using BussinessLayer.Interfaces.Repositories;
using BussinessLayer.Interfaces.Repository.ModuloGeneral.Geografia;
using DataLayer.Models.ModuloGeneral.Geografia;

namespace BussinessLayer.Services.ModuloGeneral.Geografia
{
    public class RegionService : GenericService<RegionRequest, RegionResponse, Region>, IRegionService
    {
        private readonly IRegionRepository _regionRepository;
        public RegionService(IRegionRepository repository, IMapper mapper) : base(repository, mapper)
        {
            _regionRepository = repository;
        }

        public async Task<List<Region>> GetAllIndex()
        {
            return await _regionRepository.GetAllIndex();
        }
    }
}
