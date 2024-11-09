using Microsoft.EntityFrameworkCore;
using DataLayer.PDbContex;
using DataLayer.Models.ModuloInventario.Descuento;

public class DescuentoService : IDescuentoService
{
    private readonly PDbContext _context;

    public DescuentoService(PDbContext dbContext)
    {
        _context = dbContext;
    }

    public async Task Add(Descuentos entity)
    {
        try
        {
            entity.Activo = DateTime.Today >= entity.FechaInicio && DateTime.Today <= entity.FechaFin;
            _context.Descuentos.Add(entity);
            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task Edit(Descuentos entity)
    {
        throw new NotImplementedException();
    }

    public async Task<Descuentos> GetById(int id, long idEMpresa)
    {
        return await _context.Descuentos.Include(x => x.Producto).SingleOrDefaultAsync(x => x.Id.Equals(id));
    }

    public async Task<IList<Descuentos>> GetAll(long idEMpresa)
    {
        throw new NotImplementedException();
    }

    public async Task Delete(int id, long idEMpresa)
    {
        var descuento = await GetById(id, idEMpresa);
        if (descuento == null) return;

        descuento.Borrado = true;
        await Edit(descuento);
    }
}
