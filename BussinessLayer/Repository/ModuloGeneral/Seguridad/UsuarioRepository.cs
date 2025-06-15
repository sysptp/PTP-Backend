using System.Linq;
using BussinessLayer.Interfaces.Repository.ModuloGeneral.Seguridad;
using BussinessLayer.Repository.ROtros;
using DataLayer.Models.ModuloGeneral.Empresa;
using DataLayer.Models.ModuloGeneral.Seguridad;
using DataLayer.PDbContex;
using Microsoft.EntityFrameworkCore;

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

        public async Task<bool> VerifyCompanyUserLimit(long companyId)
        {
            var companyUsers = await _context.Set<Usuario>()
                .Where(x => x.CodigoEmp == companyId && !x.Borrado)
                .CountAsync();

            var companyLimitUser = await _context.Set<GnEmpresa>()
                .Where(x => x.CODIGO_EMP == companyId)
                .Select(x => x.CANT_USUARIO)
                .FirstOrDefaultAsync();

            return companyUsers < companyLimitUser;
        }
    }
}
