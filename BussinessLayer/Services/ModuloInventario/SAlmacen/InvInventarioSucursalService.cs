using BussinessLayer.Interfaces.ModuloInventario.Almacen;
using AutoMapper;
using BussinessLayer.Interfaces.Repository.Inventario.Almacenes;
using BussinessLayer.DTOs.ModuloInventario.Almacenes;
using DataLayer.Models.ModuloInventario.Almacen;

namespace BussinessLayer.Services.ModuloInventario.SAlmacen
{
    public class InvInventarioSucursalService : GenericService<InvInventarioSucursalRequest, InvInventarioSucursalReponse, InvInventarioSucursal>, IInvInventarioSucursalService
    {
        private readonly IInvInventarioSucursalRepository _repository;
        private readonly IMapper _mapper;

        public InvInventarioSucursalService(IInvInventarioSucursalRepository repository, IMapper mapper) : base(repository, mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
    }
}
