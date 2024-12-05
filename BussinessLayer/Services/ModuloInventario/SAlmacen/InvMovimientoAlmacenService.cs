using BussinessLayer.Interfaces.ModuloInventario.Almacen;
using AutoMapper;
using BussinessLayer.Interfaces.Repository.Inventario.Almacenes;
using BussinessLayer.DTOs.ModuloInventario.Almacenes;
using DataLayer.Models.ModuloInventario.Almacen;

namespace BussinessLayer.Services.ModuloInventario.SAlmacen
{
    public class InvMovimientoAlmacenService : GenericService<InvMovimientoAlmacenRequest, InvMovimientoAlmacenReponse, InvMovimientoAlmacen>, IInvMovimientoAlmacenService
    {
        private readonly IInvMovimientoAlmacenRepository _repository;
        private readonly IMapper _mapper;

        public InvMovimientoAlmacenService(IInvMovimientoAlmacenRepository repository, IMapper mapper) : base(repository, mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
    }
}
