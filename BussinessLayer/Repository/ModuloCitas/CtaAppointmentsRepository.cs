using BussinessLayer.Interface.Repository.ModuloCitas;
using BussinessLayer.Repository.ROtros;
using DataLayer.Models.Modulo_Citas;
using DataLayer.PDbContex;
using Microsoft.EntityFrameworkCore;

namespace BussinessLayer.Repository.Modulo_Citas
{
    public class CtaAppointmentsRepository : GenericRepository<CtaAppointments>, ICtaAppointmentsRepository
    {
        public CtaAppointmentsRepository(PDbContext dbContext, ITokenService tokenService) : base(dbContext, tokenService)
        {
        }

        public async Task<List<CtaAppointments>> GetAppointmentsByDate(DateTime date, long companyId)
        {
            return await _context.CtaAppointments
                .Where(a => a.AppointmentDate == date && a.CompanyId == companyId && a.Borrado == false)
                .ToListAsync();
        }

    }
}
