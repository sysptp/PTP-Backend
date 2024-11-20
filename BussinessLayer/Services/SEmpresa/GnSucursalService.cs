using AutoMapper;
using BussinessLayer.DTOs.ModuloGeneral.Sucursal;
using BussinessLayer.Interfaces.IEmpresa;
using BussinessLayer.Interfaces.Repository.Empresa;
using DataLayer.Models.Empresa;

namespace BussinessLayer.Services.SEmpresa
{

    // CREADO POR DOMINGO 11/02/2024
    public class GnSucursalService : GenericService<GnSucursalRequest, GnSucursalResponse, GnSucursal>, IGnSucursalService
    {
        private readonly IGnSucursalRepository _repository;
        private readonly IMapper _mapper;
        public GnSucursalService(IGnSucursalRepository repository, IMapper mapper) : base(repository, mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

    }
}
