using BussinessLayer.Interfaces.Repository.ModuloCitas;
using BussinessLayer.Repository.ROtros;
using DataLayer.Models.ModuloCitas;
using DataLayer.PDbContex;

namespace BussinessLayer.Repository.ModuloCitas
{
    public class CtaNotificationTemplatesRepository : GenericRepository<CtaNotificationTemplates>, ICtaNotificationTemplatesRepository
    {
        public CtaNotificationTemplatesRepository(PDbContext dbContext, ITokenService tokenService) : base(dbContext, tokenService)
        {
        }
    }
}
