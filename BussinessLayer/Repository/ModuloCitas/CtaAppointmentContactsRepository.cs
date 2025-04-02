using BussinessLayer.Interfaces.Repository.ModuloCitas;
using BussinessLayer.Repository.ROtros;
using DataLayer.Models.ModuloCitas;
using DataLayer.PDbContex;
using Microsoft.EntityFrameworkCore;

namespace BussinessLayer.Repository.ModuloCitas
{
    public class CtaAppointmentContactsRepository : GenericRepository<CtaAppointmentContacts>, ICtaAppointmentContactsRepository
    {
        public CtaAppointmentContactsRepository(PDbContext dbContext, ITokenService tokenService) : base(dbContext, tokenService)
        {
        }

        public async Task<List<CtaContacts>> GetAllContactsByAppointmentId(int appointmentId)
        {
            return await _context.Set<CtaAppointmentContacts>()
                .Where(ac => ac.AppointmentId == appointmentId)
                .Select(ac => ac.Contact)
                .Where(c => c != null)
                .ToListAsync();
        }
    }
}
