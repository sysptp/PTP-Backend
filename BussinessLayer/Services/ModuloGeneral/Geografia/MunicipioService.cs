using AutoMapper;
using BussinessLayer.DTOs.ModuloGeneral.Geografia.DMunicipio;
using BussinessLayer.Interfaces.Repositories;
using BussinessLayer.Interfaces.Repository.ModuloGeneral.Geografia;
using BussinessLayer.Interfaces.Services.ModuloGeneral.Geografia;
using DataLayer.Models.ModuloGeneral.Geografia;

namespace BussinessLayer.Services.ModuloGeneral.Geografia
{
    public class MunicipioService : GenericService<MunicipioRequest, MunicipioResponse, Municipio>, IMunicipioService
    {
        private readonly IMunicipioRepository _repository;
        public MunicipioService(IMunicipioRepository repository, IMapper mapper) : base(repository, mapper)
        {
            _repository = repository;
        }

        public async Task<List<Municipio>> GetIndex()
        {
            return await _repository.GetIndex();
        }

        public IEnumerable<Municipio> GetMunicipiosByProvinciaId(int provinciaId)
        {
            return _repository.GetMunicipiosByProvinciaId(provinciaId);
        }
    }
}
