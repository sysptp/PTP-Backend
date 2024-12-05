
using BussinessLayer.Repository.ROtros;
using DataLayer.PDbContex;

namespace DataLayer.Models.Modulo_Citas
{
    public class CtaAppointmentManagementRepository : GenericRepository<CtaAppointmentManagement>, ICtaAppointmentManagementRepository
    {
        public CtaAppointmentManagementRepository(PDbContext dbContext, ITokenService tokenService) : base(dbContext, tokenService)
        {
        }
    }
}
