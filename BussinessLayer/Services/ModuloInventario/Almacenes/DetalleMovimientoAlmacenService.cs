using DataLayer.Models.ModuloInventario.Almacen;
using DataLayer.PDbContex;
using Microsoft.EntityFrameworkCore;

public class DetalleMovimientoAlmacenService : IDetalleMovimientoAlmacenService
{
    private readonly PDbContext _context;

    public DetalleMovimientoAlmacenService(PDbContext dbContext)
    {

        _context = dbContext;
    }

    public async Task Add(List<DetalleMovimientoAlmacen> entities)
    {
        try
        {
            if (entities == null || !entities.Any())
            {
                throw new ArgumentException("La lista de registro no puede estar null.");
            }

            await _context.DetalleMovimientoAlmacenes.AddRangeAsync(entities);
            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task Edit(DetalleMovimientoAlmacen entity)
    {
        try
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity), "El registro no puede estar null");
            }

            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<DetalleMovimientoAlmacen> GetById(int id, long idEmpresa)
    {
        try
        {
            var data = await _context.DetalleMovimientoAlmacenes
                .FirstOrDefaultAsync(x => x.Id == id && x.IdEmpresa == idEmpresa);

            return data ?? throw new KeyNotFoundException("Registro no encontrado.");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<List<DetalleMovimientoAlmacen>> GetAll()
    {
        try
        {
            return await _context.DetalleMovimientoAlmacenes
                .Where(x => !x.Borrado)
                .ToListAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task Delete(int id)
    {
        try
        {
            var entity = await _context.DetalleMovimientoAlmacenes.FindAsync(id);

            if (entity == null)
            {
                throw new KeyNotFoundException("Registro no encontrado.");
            }

            entity.Borrado = true;
            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<List<DetalleMovimientoAlmacen>> GetDetalleMovimientoByMovimientoId(int idMovimiento, long idEmpresa)
    {
        try
        {
            return await _context.DetalleMovimientoAlmacenes
                .Where(x => x.IdMovimiento == idMovimiento && x.IdEmpresa == idEmpresa && !x.Borrado)
                .ToListAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

}

