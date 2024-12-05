using BussinessLayer.Repository.ROtros;
using DataLayer.PDbContex;

namespace DataLayer.Models.Modulo_Citas
{
    public class CtaAppointmentReasonRepository : GenericRepository<CtaAppointmentReason>, ICtaAppointmentReasonRepository
    {
        public CtaAppointmentReasonRepository(PDbContext dbContext, ITokenService tokenService) : base(dbContext, tokenService)
        {
        }
    }
}
