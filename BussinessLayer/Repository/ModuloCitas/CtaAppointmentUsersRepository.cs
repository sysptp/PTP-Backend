using BussinessLayer.Interfaces.Repository.ModuloCitas;
using BussinessLayer.Repository.ROtros;
using DataLayer.Models.ModuloCitas;
using DataLayer.Models.ModuloGeneral.Seguridad;
using DataLayer.PDbContex;
using Microsoft.EntityFrameworkCore;

namespace BussinessLayer.Repository.ModuloCitas
{
    public class CtaAppointmentUsersRepository : GenericRepository<CtaAppointmentUsers>, ICtaAppointmentUsersRepository
    {
        public CtaAppointmentUsersRepository(PDbContext dbContext, ITokenService tokenService) : base(dbContext, tokenService)
        {
        }

        public async Task<List<Usuario>> GetAllUserByAppointmentId(int appointmentId)
        {
            return await _context.Set<CtaAppointmentUsers>()
                .Where(ac => ac.AppointmentId == appointmentId)
                .Select(ac => ac.Usuario)
                .Where(c => c != null)
                .ToListAsync();
        }
    }
}
