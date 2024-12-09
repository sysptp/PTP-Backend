using BussinessLayer.Interfaces.Repository.Inventario.Almacenes;
using BussinessLayer.Repository.ROtros;
using DataLayer.Models.ModuloInventario.Almacen;
using DataLayer.PDbContex;

namespace BussinessLayer.Repository.ModuloInventario.Almacen
{
    public class InvAlmacenInventarioRepository : GenericRepository<InvAlmacenInventario>, IInvAlmacenInventarioRepository
    {
        private readonly PDbContext _dbContext;
        private readonly ITokenService _tokenService;
        public InvAlmacenInventarioRepository(PDbContext dbContext, ITokenService tokenService) : base(dbContext, tokenService)
        {
            _dbContext = dbContext;
            _tokenService = tokenService;
        }
    }
}
