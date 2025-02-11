using BussinessLayer.Repository.ROtros;
using DataLayer.Models.ModuloCitas;
using DataLayer.PDbContex;

namespace BussinessLayer.Repository.ModuloCitas
{
    public class CtaAppointmentGuestRepository : GenericRepository<CtaAppointmentGuest>
    {
        public CtaAppointmentGuestRepository(PDbContext dbContext, ITokenService tokenService) : base(dbContext, tokenService)
        {
        }
    }
}
