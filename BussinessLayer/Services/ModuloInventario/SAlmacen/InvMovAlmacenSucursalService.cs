using BussinessLayer.Interfaces.ModuloInventario.Almacen;
using AutoMapper;
using BussinessLayer.Interfaces.Repository.Inventario.Almacenes;
using BussinessLayer.DTOs.ModuloInventario.Almacenes;
using DataLayer.Models.ModuloInventario.Almacen;

namespace BussinessLayer.Services.ModuloInventario.SAlmacen
{
    public class InvMovAlmacenSucursalService : GenericService<InvMovAlmacenSucursalRequest, InvMovAlmacenSucursalReponse, InvMovAlmacenSucursal>, IInvMovAlmacenSucursalService
    {
        private readonly IInvMovAlmacenSucursalRepository _repository;
        private readonly IMapper _mapper;

        public InvMovAlmacenSucursalService(IInvMovAlmacenSucursalRepository repository, IMapper mapper) : base(repository, mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
    }
}
