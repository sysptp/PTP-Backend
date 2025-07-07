using BussinessLayer.Interfaces.Repositories;
using DataLayer.Models.ModuloCitas;

namespace BussinessLayer.Interfaces.Repository.ModuloCitas
{
    public interface ICtaBookingPortalConfigRepository : IGenericRepository<CtaBookingPortalConfig>
    {
        Task<CtaBookingPortalConfig?> GetBySlugAsync(string slug);
        Task<CtaBookingPortalConfig?> GetByCompanyAndAreaAsync(long companyId, int? areaId);
        Task<List<CtaBookingPortalConfig>> GetActivePortalsByCompanyAsync(long companyId);
        Task<bool> SlugExistsAsync(string slug, int? excludeId = null);
    }
}
