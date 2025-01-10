using BussinessLayer.Interface.Repository.Modulo_Citas;
using BussinessLayer.Repository.ROtros;
using DataLayer.Models.Modulo_Citas;
using DataLayer.PDbContex;

namespace BussinessLayer.Repository.Modulo_Citas
{
    public class CtaAppointmentMovementsRepository : GenericRepository<CtaAppointmentMovements>, ICtaAppointmentMovementsRepository
    {
        public CtaAppointmentMovementsRepository(PDbContext dbContext, ITokenService tokenService) : base(dbContext, tokenService)
        {
        }
    }
}
