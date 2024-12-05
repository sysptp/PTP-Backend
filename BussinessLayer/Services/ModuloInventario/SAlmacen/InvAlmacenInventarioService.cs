using BussinessLayer.Interfaces.ModuloInventario.Almacen;
using AutoMapper;
using BussinessLayer.Interfaces.Repository.Inventario.Almacenes;
using BussinessLayer.DTOs.ModuloInventario.Almacenes;
using DataLayer.Models.ModuloInventario.Almacen;

namespace BussinessLayer.Services.ModuloInventario.SAlmacen
{
    public class InvAlmacenInventarioService : GenericService<InvAlmacenInventarioRequest, InvAlmacenInventarioReponse, InvAlmacenInventario>, IInvAlmacenInventarioService
    {
        private readonly IInvAlmacenInventarioRepository _repository;
        private readonly IMapper _mapper;

        public InvAlmacenInventarioService(IInvAlmacenInventarioRepository repository, IMapper mapper) : base(repository, mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
    }
}
