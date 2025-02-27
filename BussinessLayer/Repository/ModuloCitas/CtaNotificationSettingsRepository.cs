using BussinessLayer.Interfaces.Repository.ModuloCitas;
using BussinessLayer.Repository.ROtros;
using DataLayer.Models.ModuloCitas;
using DataLayer.PDbContex;

namespace BussinessLayer.Repository.ModuloCitas
{
    public class CtaNotificationSettingsRepository : GenericRepository<CtaNotificationSettings>, ICtaNotificationSettingsRepository
    {
        public CtaNotificationSettingsRepository(PDbContext dbContext, ITokenService tokenService) : base(dbContext, tokenService)
        {
        }
    }
}
