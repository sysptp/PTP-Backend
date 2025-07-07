using BussinessLayer.Interfaces.Repository.ModuloCitas;
using BussinessLayer.Repository.ROtros;
using DataLayer.Models.ModuloCitas;
using DataLayer.PDbContex;
using Microsoft.EntityFrameworkCore;

namespace BussinessLayer.Repository.ModuloCitas
{
    public class CtaBookingPortalAreasRepository : GenericRepository<CtaBookingPortalAreas>, ICtaBookingPortalAreasRepository
    {
        public CtaBookingPortalAreasRepository(PDbContext dbContext, ITokenService tokenService) : base(dbContext, tokenService)
        {
        }

        public async Task<List<CtaBookingPortalAreas>> GetByPortalIdAsync(int portalId)
        {
            return await _context.Set<CtaBookingPortalAreas>()
                .Include(pa => pa.Area)
                .Where(pa => pa.PortalId == portalId && !pa.Borrado)
                .ToListAsync();
        }

        public async Task<List<CtaBookingPortalAreas>> GetByAreaIdAsync(int areaId)
        {
            return await _context.Set<CtaBookingPortalAreas>()
                .Include(pa => pa.Portal)
                .Where(pa => pa.AreaId == areaId && !pa.Borrado)
                .ToListAsync();
        }

        public async Task<CtaBookingPortalAreas?> GetDefaultAreaByPortalIdAsync(int portalId)
        {
            return await _context.Set<CtaBookingPortalAreas>()
                .Include(pa => pa.Area)
                .FirstOrDefaultAsync(pa => pa.PortalId == portalId && pa.IsDefault && !pa.Borrado);
        }

        public async Task DeleteByPortalIdAsync(int portalId)
        {
            var portalAreas = await _context.Set<CtaBookingPortalAreas>()
                .Where(pa => pa.PortalId == portalId && !pa.Borrado)
                .ToListAsync();

            foreach (var pa in portalAreas)
            {
                pa.Borrado = true;
                pa.FechaModificacion = DateTime.Now;
                pa.UsuarioModificacion = _tokenService.GetClaimValue("sub") ?? "System";
            }

            await _context.SaveChangesAsync();
        }

        public async Task DeleteByPortalAndAreaAsync(int portalId, int areaId)
        {
            var portalArea = await _context.Set<CtaBookingPortalAreas>()
                .FirstOrDefaultAsync(pa => pa.PortalId == portalId && pa.AreaId == areaId && !pa.Borrado);

            if (portalArea != null)
            {
                portalArea.Borrado = true;
                portalArea.FechaModificacion = DateTime.Now;
                portalArea.UsuarioModificacion = _tokenService.GetClaimValue("sub") ?? "System";
                await _context.SaveChangesAsync();
            }
        }
    }
}