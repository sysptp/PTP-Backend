using AutoMapper;
using BussinessLayer.DTOs.ModuloGeneral.ParametroGenerales;
using BussinessLayer.Interfaces.Repository.Configuracion.ParametrosGenerales;
using BussinessLayer.Interfaces.Services.IModuloGeneral.IParametrosGenerales;
using DataLayer.Models.ModuloGeneral;


namespace BussinessLayer.Services.SModuloGeneral.SParametrosGenerales
{
    internal class GnParametrosGeneralesService : GenericService<GnParametrosGeneralesRequest, GnParametrosGeneralesReponse, GnParametrosGenerales>, IGnParametrosGeneralesService
    {
        private readonly IGnParametrosGeneralesRepository _repository;
        private readonly IMapper _mapper;

        public GnParametrosGeneralesService(IGnParametrosGeneralesRepository repository, IMapper mapper) : base(repository, mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
    }
}
