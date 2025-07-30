using BussinessLayer.Interfaces.Repository.ModuloCitas;
using BussinessLayer.Repository.ROtros;
using DataLayer.Models.ModuloCitas;
using DataLayer.PDbContex;

namespace BussinessLayer.Repository.ModuloCitas
{
    public class CtaWhatsAppTemplatesRepository : GenericRepository<CtaWhatsAppTemplates>, ICtaWhatsAppTemplatesRepository
    {
        public CtaWhatsAppTemplatesRepository(PDbContext dbContext, ITokenService tokenService) : base(dbContext, tokenService)
        {
        }
    }
}
