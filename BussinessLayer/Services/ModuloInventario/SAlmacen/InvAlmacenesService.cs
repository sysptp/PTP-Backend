using BussinessLayer.Interfaces.ModuloInventario.Almacen;
using AutoMapper;
using BussinessLayer.Interfaces.Repository.Inventario.Almacenes;
using BussinessLayer.DTOs.ModuloInventario.Almacenes;
using DataLayer.Models.ModuloInventario.Almacen;

namespace BussinessLayer.Services.ModuloInventario.SAlmacen
{
    public class InvAlmacenesService : GenericService<InvAlmacenesRequest, InvAlmacenesReponse, InvAlmacenes>, IInvAlmacenesService
    {
        private readonly IInvAlmacenesRepository _repository;
        private readonly IMapper _mapper;

        public InvAlmacenesService(IInvAlmacenesRepository repository, IMapper mapper) : base(repository, mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
    }
}
