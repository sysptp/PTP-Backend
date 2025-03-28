using BussinessLayer.Interface.Repository.Modulo_Citas;
using BussinessLayer.Repository.ROtros;
using DataLayer.Models.Modulo_Citas;
using DataLayer.PDbContex;

namespace BussinessLayer.Repository.Modulo_Citas
{
    public class CtaSessionDetailsRepository : GenericRepository<CtaSessionDetails>, ICtaSessionDetailsRepository
    {
        public CtaSessionDetailsRepository(PDbContext dbContext, ITokenService tokenService) : base(dbContext, tokenService)
        {
        }

        public List<CtaSessionDetails> GetAllAppointmentsBySessionId(int sessionId)
        {
            var sesionList = _context.Set<CtaSessionDetails>().Where(x => x.IdSession == sessionId).ToList();

            return sesionList;
        }
    }
}
