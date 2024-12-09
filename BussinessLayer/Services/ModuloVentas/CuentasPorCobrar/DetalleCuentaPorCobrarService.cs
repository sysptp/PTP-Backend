using DataLayer.PDbContex;
using Microsoft.EntityFrameworkCore;

public class DetalleCuentaPorCobrarService : IDetalleCuentasPorCobrar
{
    private readonly PDbContext _context;

    public DetalleCuentaPorCobrarService(PDbContext dbContext)
    {
        _context = dbContext;
    }

    public async Task Add(DetalleCuentasPorCobrar entity)
    {
        try
        {
            CuentasPorCobrar dc = await _context.CuentasPorCobrar.Where(x => x.FacturacionId == entity.FacturacionId).SingleAsync();

            if (entity.Monto >= dc.MontoPendiente)
            {
                dc.IsPaid = true;
                dc.MontoPendiente -= entity.Monto;
                _context.Entry(dc).State = EntityState.Modified;
                _context.DetalleCuentasPorCobrar.Add(entity);
                await _context.SaveChangesAsync();
            }
            else
            {
                dc.MontoPendiente -= entity.Monto;
                dc.FechaUltimoPago = DateTime.Now;
                _context.Entry(dc).State = EntityState.Modified;
                _context.DetalleCuentasPorCobrar.Add(entity);
                await _context.SaveChangesAsync();
            }

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    //OJO CON ESTE METODO
    public async Task<IEnumerable<DetalleCuentasPorCobrar>> GetAllByCuentaPorCobrarId(int id) => await _context
        .DetalleCuentasPorCobrar.Where(x => x.Borrado != true && x.FacturacionId == id).ToListAsync();

    public Task Edit(DetalleCuentasPorCobrar entity)
    {
        throw new NotImplementedException();
    }

    public Task<DetalleCuentasPorCobrar> GetById(int id, long idEMpresa)
    {
        throw new NotImplementedException();
    }

    public Task<IList<DetalleCuentasPorCobrar>> GetAll(long idEMpresa)
    {
        throw new NotImplementedException();
    }

    public Task Delete(int id, long idEMpresa)
    {
        throw new NotImplementedException();
    }
}

