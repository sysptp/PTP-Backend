using DataLayer.Models.ModuloInventario.Monedas;
using DataLayer.PDbContex;
using Microsoft.EntityFrameworkCore;

public class MonedasService : IMonedasService
{
    private readonly PDbContext _context;

    public MonedasService(PDbContext dbContext)
    {
        _context = dbContext;
    }

    public async Task Add(Moneda model)
    {
        _context.Monedas.Add(model);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(Moneda model)
    {
        _context.Monedas.Remove(model);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Moneda>> GetAll()
    {
        return await _context.Monedas.ToListAsync();
    }

    public async Task<Moneda> GetById(int id)
    {
        var result = await _context.Monedas.FindAsync(id);

        return result;
    }

    public async Task Update(Moneda model)
    {
        _context.Entry(model).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }
}

