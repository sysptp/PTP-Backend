using BussinessLayer.Interfaces.Repository.ModuloGeneral.Seguridad;
using BussinessLayer.Repository.ROtros;
using DataLayer.Models.ModuloGeneral.Seguridad;
using DataLayer.PDbContex;

namespace BussinessLayer.Repository.ModuloGeneral.Seguridad
{
    public class UsuarioRepository : GenericRepository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(PDbContext dbContext, ITokenService tokenService) : base(dbContext, tokenService)
        {
        }

        public bool EmailExists(string email)
        {
            return _context.Set<Usuario>().Any(x => x.Email == email);
        }
    }
}
