using BussinessLayer.Interfaces.Repository.ModuloGeneral.Geografia;
using BussinessLayer.Repository.ROtros;
using DataLayer.Models.ModuloGeneral.Geografia;
using DataLayer.PDbContex;
using Microsoft.EntityFrameworkCore;

namespace BussinessLayer.Repository.ModuloGeneral.Geografia
{
    public class MunicipioRepository : GenericRepository<Municipio>, IMunicipioRepository
    {
        private readonly PDbContext _context;

        public MunicipioRepository(PDbContext dbContext, ITokenService tokenService) : base(dbContext, tokenService)
        {
            _context = dbContext;
        }

        public IEnumerable<Municipio> GetMunicipiosByProvinciaId(int provinciaId)
        {

            return _context.Municipio.ToList().Where(x => x.Borrado != true && x.IdProvincia == provinciaId);
        }

        public async Task<List<Municipio>> GetIndex()
        {
            var municipios = await _context.Municipio.Include(m => m.Provincia).ToListAsync();

            return municipios;
        }

    }
}
