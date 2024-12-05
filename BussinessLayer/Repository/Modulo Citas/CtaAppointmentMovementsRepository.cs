
using BussinessLayer.Repository.ROtros;
using DataLayer.PDbContex;

namespace DataLayer.Models.Modulo_Citas
{
    public class CtaAppointmentMovementsRepository : GenericRepository<CtaAppointmentMovements>, ICtaAppointmentMovementsRepository
    {
        public CtaAppointmentMovementsRepository(PDbContext dbContext, ITokenService tokenService) : base(dbContext, tokenService)
        {
        }
    }
}
