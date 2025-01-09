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

        public async Task<List<InvMovInventarioSucursalReponse>> GetAllByFilters(long? idSucursal, long? idCompany)
        {
            var inventarioSucursal = await _repository.GetAllWithIncludeAsync(new List<string> { "GnSucursal" });
           
            if (idCompany.HasValue) 
            { 
                inventarioSucursal.Where(x => x.GnSucursal.CodigoEmp == idCompany.Value).ToList();
            }

            if (idSucursal.HasValue)
            {
                inventarioSucursal.Where(x => x.IdSucursal == idSucursal.Value).ToList();
            }
            return _mapper.Map<List<InvMovInventarioSucursalReponse>>(inventarioSucursal);
        }
    }
}
