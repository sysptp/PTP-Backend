using AutoMapper;
using BussinessLayer.DTOs.Geografia.DRegion;
using BussinessLayer.Interfaces.IGeografia;
using BussinessLayer.Interfaces.Repositories;
using BussinessLayer.Interfaces.Repository.Geografia;
using DataLayer.Models.Geografia;

namespace BussinessLayer.Services.SGeografia
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
