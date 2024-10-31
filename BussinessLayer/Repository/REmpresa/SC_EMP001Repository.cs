using BussinessLayer.Interfaces.IAutenticacion;
using BussinessLayer.Interfaces.Repository.Empresa;
using BussinessLayer.Repository.ROtros;
using DataLayer.Models.Empresa;
using DataLayer.PDbContex;
using Microsoft.Identity.Client;
using System.Runtime.CompilerServices;

namespace BussinessLayer.Repository.REmpresa
{
    public class SC_EMP001Repository : GenericRepository<SC_EMP001>, ISC_EMP001Repository
    {
        private readonly PDbContext _dbContext;
        public SC_EMP001Repository(PDbContext dbContext, IClaimsService claimsService) : base(dbContext, claimsService)
        {
           _dbContext = dbContext;
        }

        public async Task<SC_EMP001> GetByEmpCode(long empCode)
        {
            try
            {

                var entity = await _context.Set<SC_EMP001>().FindAsync(empCode);
                return entity?.Borrado == true ? null : entity;
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }
    }
}
