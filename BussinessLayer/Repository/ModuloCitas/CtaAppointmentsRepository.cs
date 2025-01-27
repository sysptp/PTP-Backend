using BussinessLayer.Interface.Repository.ModuloCitas;
using BussinessLayer.Repository.ROtros;
using DataLayer.Models.Modulo_Citas;
using DataLayer.PDbContex;

namespace BussinessLayer.Repository.Modulo_Citas
{
    public class CtaAppointmentsRepository : GenericRepository<CtaAppointments>, ICtaAppointmentsRepository
    {
        public CtaAppointmentsRepository(PDbContext dbContext, ITokenService tokenService) : base(dbContext, tokenService)
        {
        }
    }
}
