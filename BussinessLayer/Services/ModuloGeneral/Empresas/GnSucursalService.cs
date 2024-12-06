using AutoMapper;
using BussinessLayer.DTOs.ModuloGeneral.Sucursal;
using BussinessLayer.Interfaces.ModuloGeneral.Empresas;
using BussinessLayer.Interfaces.Repository.ModuloGeneral.Empresa;
using DataLayer.Models.ModuloGeneral.Empresa;

namespace BussinessLayer.Services.ModuloGeneral.Empresas
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
