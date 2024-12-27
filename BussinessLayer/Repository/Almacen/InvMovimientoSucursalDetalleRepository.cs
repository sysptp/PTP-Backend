using BussinessLayer.Interfaces.Repository.Inventario.Almacenes;
using BussinessLayer.Repository.ROtros;
using DataLayer.Models.ModuloInventario.Almacen;
using DataLayer.PDbContex;

namespace BussinessLayer.Repository.Almacen
{
    public class InvMovimientoSucursalDetalleRepository : GenericRepository<InvMovimientoSucursalDetalle>, IInvMovimientoSucursalDetalleRepository
    {
        private readonly PDbContext _dbContext;
        private readonly ITokenService _tokenService;
        public InvMovimientoSucursalDetalleRepository(PDbContext dbContext, ITokenService tokenService) : base(dbContext, tokenService)
        {
            _dbContext = dbContext;
            _tokenService = tokenService;
        }
    }
}
