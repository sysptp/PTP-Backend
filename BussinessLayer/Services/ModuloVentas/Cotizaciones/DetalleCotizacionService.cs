using BussinessLayer.Interfaces.Services.ModuloVentas.ICotizaciones;
using DataLayer.Models.ModuloVentas.Cotizaciones;
using DataLayer.PDbContex;
using Microsoft.EntityFrameworkCore;

namespace BussinessLayer.Services.ModuloVentas.Cotizaciones
{
    public class DetalleCotizacionService : IDetalleCotizacionService
    {
        private readonly PDbContext _context;

        public DetalleCotizacionService(PDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task Add(DetalleCotizacion entity)
        {
            try
            {
                _context.DetalleCotizacion.Add(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public Task Edit(DetalleCotizacion entity)
        {
            throw new NotImplementedException();
        }

        public Task<DetalleCotizacion> GetById(int id, long idEMpresa)
        {
            throw new NotImplementedException();
        }

        public Task<IList<DetalleCotizacion>> GetAll(long idEMpresa)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id, long idEMpresa)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<DetalleCotizacion>> GetAllByCotizacionId(int cotizacionId) => await _context.DetalleCotizacion.Include(x => x.Cotizacion)
            .Where(x => x.CotizacionId == cotizacionId && x.Borrado != true).ToListAsync();

    }
}
