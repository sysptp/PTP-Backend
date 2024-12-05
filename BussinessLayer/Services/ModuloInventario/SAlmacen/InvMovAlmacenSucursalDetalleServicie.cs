using BussinessLayer.Interfaces.ModuloInventario.Almacen;
using AutoMapper;
using BussinessLayer.Interfaces.Repository.Inventario.Almacenes;
using BussinessLayer.DTOs.ModuloInventario.Almacenes;
using DataLayer.Models.ModuloInventario.Almacen;


namespace BussinessLayer.Services.ModuloInventario.SAlmacen
{
    public class InvMovAlmacenSucursalDetalleServicie : GenericService<InvMovAlmacenSucursalDetalleRequest, InvMovAlmacenSucursalDetalleReponse, InvMovAlmacenSucursalDetalle>, IInvMovAlmacenSucursalDetalleService
    {
        private readonly IInvMovAlmacenSucursalDetalleRepository _repository;
        private readonly IMapper _mapper;

        public InvMovAlmacenSucursalDetalleServicie(IInvMovAlmacenSucursalDetalleRepository repository, IMapper mapper) : base(repository, mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
    }
}
