//using BussinessLayer.ViewModels;
using DataLayer.PDbContex;
using Microsoft.EntityFrameworkCore;

namespace BussinessLayer.Services.ModuloVentas.Cotizaciones
{
    public class CotizacionService : ICotizacionService
    {
        //private readonly PDbContext _context;
        //private readonly IDetalleCotizacionService _detalleCotizacion;

        //public CotizacionService(PDbContext dbContext, IDetalleCotizacionService detalleCotizacionService)
        //{
        //    _context = dbContext;
        //    _detalleCotizacion = detalleCotizacionService;
        //}

        //public async Task Add(Cotizacion entity)
        //{
        //    try
        //    {
        //        _context.Cotizacion.Add(entity);
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e);
        //        throw;
        //    }
        //}

        //public async Task Create(CotizacionViewModel cotizacion)
        //{
        //    try
        //    {
        //        await  Add(cotizacion.Cotizacion);
        //        int cotizacionId = cotizacion.Cotizacion.Id;

        //        foreach (var d in cotizacion.DetalleCotizacion)
        //        {
        //            d.CotizacionId = cotizacionId;
        //            d.IdEmpresa = cotizacion.Cotizacion.IdEmpresa;

        //           await _detalleCotizacion.Add(d);    
        //        }

        //        await _context.SaveChangesAsync();
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e);
        //        throw;
        //    }
        //}

        //public Task Edit(Cotizacion entity)
        //{
        //    throw new NotImplementedException();
        //}

        //public async Task<Cotizacion> GetById(int id, long idempresa) => await _context.Cotizacion.Include(x => x.DetalleCotizacion)
        //    .SingleOrDefaultAsync(x => x.Id == id && x.Borrado != true);

        //public async Task<IList<Cotizacion>> GetAll(long idempresa) =>
        //    await _context.Cotizacion.Include(x=> x.DetalleCotizacion).Where(x => x.Borrado != true && x.IdEmpresa==idempresa).ToListAsync();

        //public Task Delete(int id, long idempresa)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
