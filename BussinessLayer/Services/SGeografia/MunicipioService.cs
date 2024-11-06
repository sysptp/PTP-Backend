using AutoMapper;
using BussinessLayer.DTOs.Configuracion.Geografia.DMunicipio;
using BussinessLayer.Interfaces.IGeografia;
using BussinessLayer.Interfaces.Repositories;
using BussinessLayer.Interfaces.Repository.Geografia;
using DataLayer.Models.Geografia;

namespace BussinessLayer.Services.SGeografia
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
