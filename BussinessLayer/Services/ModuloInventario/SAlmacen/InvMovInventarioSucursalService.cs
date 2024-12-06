using BussinessLayer.Interfaces.ModuloInventario.Almacen;
using AutoMapper;
using BussinessLayer.Interfaces.Repository.Inventario.Almacenes;
using BussinessLayer.DTOs.ModuloInventario.Almacenes;
using DataLayer.Models.ModuloInventario.Almacen;



namespace BussinessLayer.Services.ModuloInventario.SAlmacen
{
    public class InvMovInventarioSucursalService : GenericService<InvMovInventarioSucursalRequest, InvMovInventarioSucursalReponse, InvMovInventarioSucursal>, IInvMovInventarioSucursalService
    {
        private readonly IInvMovInventarioSucursalRepository _repository;
        private readonly IMapper _mapper;

        public InvMovInventarioSucursalService(IInvMovInventarioSucursalRepository repository, IMapper mapper) : base(repository, mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
    }
}
