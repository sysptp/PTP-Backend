using BussinessLayer.Interfaces.Repository.ModuloGeneral.Seguridad;
using BussinessLayer.Repository.ROtros;
using DataLayer.Models.ModuloGeneral.Seguridad;
using DataLayer.PDbContex;

namespace BussinessLayer.Repository.ModuloGeneral.Seguridad
{
    public class GnScheduleRepository : GenericRepository<GnSchedule>, IGnScheduleRepository
    {
        public GnScheduleRepository(PDbContext dbContext, ITokenService tokenService) : base(dbContext, tokenService)
        {
        }
    }
}
