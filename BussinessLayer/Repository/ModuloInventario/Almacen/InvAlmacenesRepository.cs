using BussinessLayer.Interfaces.Repository.Inventario.Almacenes;
using BussinessLayer.Repository.ROtros;
using DataLayer.Models.ModuloInventario.Almacen;
using DataLayer.PDbContex;

namespace BussinessLayer.Repository.ModuloInventario.Almacen
{
    public class InvAlmacenesRepository : GenericRepository<InvAlmacenes>, IInvAlmacenesRepository
    {
        private readonly PDbContext _dbContext;
        private readonly ITokenService _tokenService;
        public InvAlmacenesRepository(PDbContext dbContext, ITokenService tokenService) : base(dbContext, tokenService)
        {
            _dbContext = dbContext;
            _tokenService = tokenService;
        }
    }
}
