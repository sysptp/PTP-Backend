using BussinessLayer.Interfaces.Repository.ModuloCitas;
using BussinessLayer.Repository.ROtros;
using DataLayer.Models.ModuloCitas;
using DataLayer.PDbContex;
using Microsoft.EntityFrameworkCore;

namespace BussinessLayer.Repository.ModuloCitas
{
    public class CtaBookingPortalUsersRepository : GenericRepository<CtaBookingPortalUsers>, ICtaBookingPortalUsersRepository
    {

        public CtaBookingPortalUsersRepository(PDbContext dbContext, ITokenService tokenService) : base(dbContext, tokenService)
        {
        }

        public async Task<List<CtaBookingPortalUsers>> GetByPortalIdAsync(int portalId)
        {
            return await _context.Set<CtaBookingPortalUsers>()
                .Include(pu => pu.User)
                .Where(pu => pu.PortalId == portalId && !pu.Borrado)
                .ToListAsync();
        }

        public async Task<List<CtaBookingPortalUsers>> GetByUserIdAsync(int userId)
        {
            return await _context.Set<CtaBookingPortalUsers>()
                .Include(pu => pu.Portal)
                .Where(pu => pu.UserId == userId && !pu.Borrado)
                .ToListAsync();
        }

        public async Task<CtaBookingPortalUsers?> GetMainAssigneeByPortalIdAsync(int portalId)
        {
            return await _context.Set<CtaBookingPortalUsers>()
                .Include(pu => pu.User)
                .FirstOrDefaultAsync(pu => pu.PortalId == portalId && pu.IsMainAssignee && !pu.Borrado);
        }

        public async Task DeleteByPortalIdAsync(int portalId)
        {
            var portalUsers = await _context.Set<CtaBookingPortalUsers>()
                .Where(pu => pu.PortalId == portalId && !pu.Borrado)
                .ToListAsync();

            foreach (var pu in portalUsers)
            {
                pu.Borrado = true;
                pu.FechaModificacion = DateTime.Now;
                pu.UsuarioModificacion = _tokenService.GetClaimValue("sub") ?? "System";
            }

            await _context.SaveChangesAsync();
        }

        public async Task DeleteByPortalAndUserAsync(int portalId, int userId)
        {
            var portalUser = await _context.Set<CtaBookingPortalUsers>()
                .FirstOrDefaultAsync(pu => pu.PortalId == portalId && pu.UserId == userId && !pu.Borrado);

            if (portalUser != null)
            {
                portalUser.Borrado = true;
                portalUser.FechaModificacion = DateTime.Now;
                portalUser.UsuarioModificacion = _tokenService.GetClaimValue("sub") ?? "System";
                await _context.SaveChangesAsync();
            }
        }
    }
}