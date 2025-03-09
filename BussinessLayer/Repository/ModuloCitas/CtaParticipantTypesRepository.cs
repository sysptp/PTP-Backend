using BussinessLayer.Interfaces.Repository.ModuloCitas;
using BussinessLayer.Repository.ROtros;
using DataLayer.Models.ModuloCitas;
using DataLayer.PDbContex;

namespace BussinessLayer.Repository.ModuloCitas
{
    public class CtaParticipantTypesRepository : GenericRepository<CtaParticipantTypes>, ICtaParticipantTypesRepository
    {
        public CtaParticipantTypesRepository(PDbContext dbContext, ITokenService tokenService) : base(dbContext, tokenService)
        {
        }
    }
}
