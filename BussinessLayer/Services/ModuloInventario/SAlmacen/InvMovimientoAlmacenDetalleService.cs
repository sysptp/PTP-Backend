using BussinessLayer.Interfaces.ModuloInventario.Almacen;
using AutoMapper;
using BussinessLayer.Interfaces.Repository.Inventario.Almacenes;
using BussinessLayer.DTOs.ModuloInventario.Almacenes;
using DataLayer.Models.ModuloInventario.Almacen;

namespace BussinessLayer.Services.ModuloInventario.SAlmacen
{
    public class InvMovimientoAlmacenDetalleService : GenericService<InvMovimientoAlmacenDetalleRequest, InvMovimientoAlmacenDetalleReponse, InvMovimientoAlmacenDetalle>, IInvMovimientoAlmacenDetalleService
    {
        private readonly IInvMovimientoAlmacenDetalleRepository _repository;
        private readonly IMapper _mapper;

        public InvMovimientoAlmacenDetalleService(IInvMovimientoAlmacenDetalleRepository repository, IMapper mapper) : base(repository, mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
    }
}
