
using BussinessLayer.Interfaces.Repository.Seguridad;
using BussinessLayer.Repository.ROtros;
using DataLayer.Models.Seguridad;
using DataLayer.PDbContex;

namespace BussinessLayer.Repository.RSeguridad
{
    public class GnScheduleRepository : GenericRepository<GnSchedule>, IGnScheduleRepository
    {
        public GnScheduleRepository(PDbContext dbContext, ITokenService tokenService) : base(dbContext, tokenService)
        {
        }
    }
}
