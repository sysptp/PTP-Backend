using BussinessLayer.Interfaces.Repository.Inventario.Almacenes;
using BussinessLayer.Repository.ROtros;
using DataLayer.Models.ModuloInventario.Almacen;
using DataLayer.PDbContex;

namespace BussinessLayer.Repository.Almacen
{
    public class InvInventarioSucursalRepository : GenericRepository<InvInventarioSucursal>, IInvInventarioSucursalRepository
    {
        private readonly PDbContext _dbContext;
        private readonly ITokenService _tokenService;
        public InvInventarioSucursalRepository(PDbContext dbContext, ITokenService tokenService) : base(dbContext, tokenService)
        {
            _dbContext = dbContext;
            _tokenService = tokenService;
        }
    }
}
