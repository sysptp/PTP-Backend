using BussinessLayer.Interfaces.IAutenticacion;
using BussinessLayer.Interfaces.Repository.Empresa;
using BussinessLayer.Repository.ROtros;
using DataLayer.Models.Empresa;
using DataLayer.PDbContex;

namespace BussinessLayer.Repository.REmpresa
{
    public class SC_EMP001Repository : GenericRepository<SC_EMP001>, ISC_EMP001Repository
    {
        public SC_EMP001Repository(PDbContext dbContext, IClaimsService claimsService) : base(dbContext, claimsService)
        {
        }
    }
}
