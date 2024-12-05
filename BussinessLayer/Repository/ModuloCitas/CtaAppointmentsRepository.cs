using BussinessLayer.Repository.ROtros;
using DataLayer.PDbContex;

namespace DataLayer.Models.Modulo_Citas
{
    public class CtaAppointmentsRepository : GenericRepository<CtaAppointments>, ICtaAppointmentsRepository
    {
        public CtaAppointmentsRepository(PDbContext dbContext, ITokenService tokenService) : base(dbContext, tokenService)
        {
        }
    }
}
