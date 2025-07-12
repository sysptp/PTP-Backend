using BussinessLayer.Interface.Repository.Modulo_Citas;
using BussinessLayer.Repository.ROtros;
using DataLayer.Models.Modulo_Citas;
using DataLayer.PDbContex;
using Microsoft.EntityFrameworkCore;

namespace BussinessLayer.Repository.Modulo_Citas
{
    public class CtaStateRepository : GenericRepository<CtaState>, ICtaStateRepository
    {
        public CtaStateRepository(PDbContext dbContext, ITokenService tokenService) : base(dbContext, tokenService)
        {
        }

        public async Task<CtaState?> GetDefaultStateByCompanyAndAreaAsync(long companyId, int areaId)
        {
            return await _context.CtaState
                .FirstOrDefaultAsync(x => x.CompanyId == companyId && x.AreaId == areaId && x.IsDefault);
        }

        public async Task<CtaState?> GetClosureStateByCompanyAndAreaAsync(long companyId, int areaId)
        {
            return await _context.CtaState
                .FirstOrDefaultAsync(x => x.CompanyId == companyId && x.AreaId == areaId && x.IsClosure);
        }
        public override async Task<CtaState> Add(CtaState entity)
        {
            return await base.Add(entity);
        }

    }
}
