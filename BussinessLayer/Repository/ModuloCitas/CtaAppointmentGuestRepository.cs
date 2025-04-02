using BussinessLayer.Interfaces.Repository.ModuloCitas;
using BussinessLayer.Repository.ROtros;
using DataLayer.Models.ModuloCitas;
using DataLayer.PDbContex;
using Microsoft.EntityFrameworkCore;

namespace BussinessLayer.Repository.ModuloCitas
{
    public class CtaAppointmentGuestRepository : GenericRepository<CtaAppointmentGuest>, ICtaAppointmentGuestRepository
    {
        public CtaAppointmentGuestRepository(PDbContext dbContext, ITokenService tokenService) : base(dbContext, tokenService)
        {
        }

        public async Task<List<CtaGuest>> GetAllGuestByAppointmentId(int appointmentId)
        {
            return await _context.Set<CtaAppointmentGuest>()
                .Where(ac => ac.AppointmentId == appointmentId)
                .Select(ac => ac.Guest)
                .Where(c => c != null)
                .ToListAsync();
        }
    }
}
