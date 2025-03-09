using BussinessLayer.Interfaces.Repository.ModuloCitas;
using BussinessLayer.Repository.ROtros;
using DataLayer.Models.ModuloCitas;
using DataLayer.PDbContex;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.IsisMtt.X509;

namespace BussinessLayer.Repository.ModuloCitas
{
    public class CtaEmailTemplatesRepository : GenericRepository<CtaEmailTemplates>, ICtaEmailTemplatesRepository
    {
        public CtaEmailTemplatesRepository(PDbContext dbContext, ITokenService tokenService) : base(dbContext, tokenService)
        {
        }

        public async Task<CtaEmailTemplates?> GetEmailTemplateByFilters(long? companyId, int? templateTypeId, bool appliesToParticipant = false, bool appliesToAssignee = false)
        {
            var query = _context.CtaEmailTemplates.AsQueryable();

            if (companyId.HasValue)
                query = query.Where(x => x.CompanyId == companyId);

            if (templateTypeId.HasValue)
                query = query.Where(x => x.TemplateTypeId == templateTypeId);

            if (appliesToParticipant)
                query = query.Where(x => x.AppliesToParticipant == true);

            if (appliesToAssignee)
                query = query.Where(x => x.AppliesToAssignee == true);

            return await query.FirstOrDefaultAsync().ConfigureAwait(false);
        }

    }
}
