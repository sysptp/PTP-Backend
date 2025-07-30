using BussinessLayer.Interface.Repository.Modulo_Citas;
using BussinessLayer.Repository.ROtros;
using DataLayer.Models.Modulo_Citas;
using DataLayer.Models.ModuloCitas;
using DataLayer.PDbContex;
using Microsoft.EntityFrameworkCore;

namespace BussinessLayer.Repository.Modulo_Citas
{
    public class CtaSessionDetailsRepository : GenericRepository<CtaSessionDetails>, ICtaSessionDetailsRepository
    {
        public CtaSessionDetailsRepository(PDbContext dbContext, ITokenService tokenService) : base(dbContext, tokenService)
        {
        }

        public List<CtaSessionDetails> GetAllSessionDetailsBySessionId(int sessionId)
        {
            var sesionList = _context.Set<CtaSessionDetails>().Where(x => x.IdSession == sessionId).ToList();

            return sesionList;
        }

        public async Task<List<CtaAppointments>> GetAllAppointmentsBySessionId(int sessionId)
        {
            return await _context.Set<CtaSessionDetails>()
                .Where(ac => ac.IdSession == sessionId)
                .Select(ac => ac.CtaAppointments)
                .Where(c => c != null)
                .ToListAsync();
        }
    }
}
