using BussinessLayer.Interfaces.ModuloInventario.Almacen;
using AutoMapper;
using BussinessLayer.Interfaces.Repository.Inventario.Almacenes;
using BussinessLayer.DTOs.ModuloInventario.Almacenes;
using DataLayer.Models.ModuloInventario.Almacen;


namespace BussinessLayer.Services.ModuloInventario.SAlmacen
{
    public class InvMovimientoSucursalDetalleService : GenericService<InvMovimientoSucursalDetalleRequest, InvMovimientoSucursalDetalleReponse, InvMovimientoSucursalDetalle>, IInvMovimientoSucursalDetalleService
    {
        private readonly IInvMovimientoSucursalDetalleRepository _repository;
        private readonly IMapper _mapper;

        public InvMovimientoSucursalDetalleService(IInvMovimientoSucursalDetalleRepository repository, IMapper mapper) : base(repository, mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
    }
}
