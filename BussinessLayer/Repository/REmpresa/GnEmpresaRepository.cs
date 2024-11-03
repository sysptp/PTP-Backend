using BussinessLayer.Interfaces.IAutenticacion;
using BussinessLayer.Interfaces.Repository.Empresa;
using BussinessLayer.Repository.ROtros;
using DataLayer.Models.Empresa;
using DataLayer.PDbContex;

namespace BussinessLayer.Repository.REmpresa
{
    public class GnEmpresaRepository : GenericRepository<GnEmpresa>, IGnEmpresaRepository
    {
        private readonly PDbContext _dbContext;
        public GnEmpresaRepository(PDbContext dbContext, ITokenService tokenService) : base(dbContext, tokenService) 
        {
           _dbContext = dbContext;
        }

        public async Task<GnEmpresa> GetByEmpCode(long empCode)
        {
            try
            {

                var entity = await _context.Set<GnEmpresa>().FindAsync(empCode);
                return entity?.Borrado == true ? null : entity;
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }
    }
}
