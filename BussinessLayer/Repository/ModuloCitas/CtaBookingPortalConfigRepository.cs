using BussinessLayer.Interfaces.Repository.ModuloCitas;
using BussinessLayer.Repository.ROtros;
using DataLayer.Models.ModuloCitas;
using DataLayer.PDbContex;
using Microsoft.EntityFrameworkCore;

namespace BussinessLayer.Repository.ModuloCitas
{
    public class CtaBookingPortalConfigRepository : GenericRepository<CtaBookingPortalConfig>, ICtaBookingPortalConfigRepository
    {
        public CtaBookingPortalConfigRepository(PDbContext dbContext, ITokenService tokenService) : base(dbContext, tokenService)
        {
        }

        public async Task<CtaBookingPortalConfig?> GetBySlugAsync(string slug)
        {
            return await _context.Set<CtaBookingPortalConfig>()
                .Include(p => p.Company)
                .Include(p => p.DefaultReason)
                .Include(p => p.DefaultPlace)
                .Include(p => p.DefaultState)
                .FirstOrDefaultAsync(p => p.CustomSlug == slug && p.IsActive && !p.Borrado);
        }

        public async Task<CtaBookingPortalConfig?> GetByCompanyAndAreaAsync(long companyId, int? areaId)
        {
            return await _context.Set<CtaBookingPortalConfig>()
                .FirstOrDefaultAsync(p => p.CompanyId == companyId &&
                                         p.IsActive &&
                                         !p.Borrado);
        }

        public async Task<List<CtaBookingPortalConfig>> GetActivePortalsByCompanyAsync(long companyId)
        {
            return await _context.Set<CtaBookingPortalConfig>()
                .Where(p => p.CompanyId == companyId && p.IsActive && !p.Borrado)
                .ToListAsync();
        }

        public async Task<bool> SlugExistsAsync(string slug, int? excludeId = null)
        {
            var query = _context.Set<CtaBookingPortalConfig>()
                .Where(p => p.CustomSlug == slug && !p.Borrado);

            if (excludeId.HasValue)
                query = query.Where(p => p.Id != excludeId.Value);

            return await query.AnyAsync();
        }
    }
}