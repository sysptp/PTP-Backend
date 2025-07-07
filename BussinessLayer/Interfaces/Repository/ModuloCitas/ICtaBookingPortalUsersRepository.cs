using BussinessLayer.Interfaces.Repositories;
using DataLayer.Models.ModuloCitas;

namespace BussinessLayer.Interfaces.Repository.ModuloCitas
{
    public interface ICtaBookingPortalUsersRepository : IGenericRepository<CtaBookingPortalUsers>
    {
        Task<List<CtaBookingPortalUsers>> GetByPortalIdAsync(int portalId);
        Task<List<CtaBookingPortalUsers>> GetByUserIdAsync(int userId);
        Task<CtaBookingPortalUsers?> GetMainAssigneeByPortalIdAsync(int portalId);
        Task DeleteByPortalIdAsync(int portalId);
        Task DeleteByPortalAndUserAsync(int portalId, int userId);
    }
}