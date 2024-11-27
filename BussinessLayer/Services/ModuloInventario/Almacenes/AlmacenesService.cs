using DataLayer.Models.ModuloInventario.Almacen;
using DataLayer.PDbContex;
using Microsoft.EntityFrameworkCore;

public class AlmacenesService : IAlmacenesService
{
    private readonly PDbContext _context;

    public AlmacenesService(PDbContext dbContext)
    {
        _context = dbContext;
    }

    public async Task Add(Almacenes entity)
    {
        try
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity), "El registro no puede estar vacio.");
            }

            await _context.Almacenes.AddAsync(entity);
            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    public async Task<Almacenes> GetById(int id, long idEmpresa)
    {
        try
        {
            var almacen = await _context.Almacenes
                .FirstOrDefaultAsync(x => x.Id == id && x.IdEmpresa == idEmpresa);

            return almacen ?? throw new KeyNotFoundException("Almacén not found.");
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    public async Task<List<Almacenes>> GetPrincipal(long idEmpresa)
    {
        try
        {
            return await _context.Almacenes
                .Where(x => !x.Borrado && x.EsPrincipal && x.IdEmpresa == idEmpresa)
                .ToListAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    public async Task<List<Almacenes>> GetAll(long idEmpresa)
    {
        try
        {
            return await _context.Almacenes
                .Where(x => !x.Borrado && x.IdEmpresa == idEmpresa)
                .ToListAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    public async Task Delete(int id, long idEmpresa)
    {
        try
        {
            var alm = await _context.Almacenes
                .FirstOrDefaultAsync(x => x.Id == id && x.IdEmpresa == idEmpresa);

            if (alm == null)
            {
                throw new KeyNotFoundException("Almacén not found.");
            }

            alm.Borrado = true;
            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    public async Task Edit(Almacenes entity)
    {
        try
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity), "El registro no puede estar vacio.");
            }

            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }
}

