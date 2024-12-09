using DataLayer.PDbContex;
using Microsoft.EntityFrameworkCore;

public class CuentaPorCobrarService : ICuentasPorCobrar
{
    private readonly PDbContext _context;

    public CuentaPorCobrarService()
    {
        _context = new PDbContext();
    }

    public async Task Add(CuentasPorCobrar entity)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));

        try
        {
            // Obtenemos el primer detalle de la lista, si está presente
            var detalleCuentaCxc = entity.DetalleCuentasPorCobrar?.SingleOrDefault();

            // Validamos si hay un detalle disponible
            if (detalleCuentaCxc != null)
            {
                detalleCuentaCxc.FacturacionId = entity.FacturacionId;
                _context.DetalleCuentasPorCobrar.Add(detalleCuentaCxc);
            }

            // Agregamos la cuenta por cobrar y guardamos los cambios
            _context.CuentasPorCobrar.Add(entity);
            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            // Utilizar 'throw;' mantiene el stack trace original
            Console.WriteLine(e);
            throw;
        }
    }

    public Task Edit(CuentasPorCobrar entity)
    {
        throw new NotImplementedException();
    }

    public async Task<CuentasPorCobrar> GetById(int id, long idempresa) => await _context.CuentasPorCobrar.Include(x => x.Facturacion)
        .SingleOrDefaultAsync(x => x.FacturacionId == id && x.IdEmpresa == idempresa);

    public async Task<IList<CuentasPorCobrar>> GetAll(long idempresa)
    {
        try
        {
            return await _context.CuentasPorCobrar.Where(x => x.Borrado != true && x.IdEmpresa == idempresa).ToListAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public Task Delete(int id, long idempresa)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<CuentasPorCobrar>> GetAllPaids()
    {
        try
        {
            return await _context.CuentasPorCobrar.Where(x => x.IsPaid).ToListAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}

