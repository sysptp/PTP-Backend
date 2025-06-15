using BussinessLayer.Interfaces.Repository.Configuracion.ParametrosGenerales;
using BussinessLayer.Repository.ROtros;
using DataLayer.Models.ModuloGeneral;
using DataLayer.PDbContex;


namespace BussinessLayer.Services.RConfiguracion.ParametrosGenerales
{
    public class GnParametrosGeneralesRepository : GenericRepository<GnParametrosGenerales>, IGnParametrosGeneralesRepository
    {
        private readonly PDbContext _dbContext;
        private readonly ITokenService _tokenService;
        public GnParametrosGeneralesRepository(PDbContext dbContext, ITokenService tokenService) : base(dbContext, tokenService)
        {
            _dbContext = dbContext;
            _tokenService = tokenService;
        }
    }
}
