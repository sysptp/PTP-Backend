using BussinessLayer.Interfaces.Repositories;
using DataLayer.Models.ModuloCitas;

namespace BussinessLayer.Interfaces.Repository.ModuloCitas
{
    public interface ICtaBookingPortalAreasRepository : IGenericRepository<CtaBookingPortalAreas>
    {
        Task<List<CtaBookingPortalAreas>> GetByPortalIdAsync(int portalId);
        Task<List<CtaBookingPortalAreas>> GetByAreaIdAsync(int areaId);
        Task<CtaBookingPortalAreas?> GetDefaultAreaByPortalIdAsync(int portalId);
        Task DeleteByPortalIdAsync(int portalId);
        Task DeleteByPortalAndAreaAsync(int portalId, int areaId);
    }
}