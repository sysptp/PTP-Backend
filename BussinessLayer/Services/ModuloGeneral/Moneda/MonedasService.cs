using AutoMapper;
using BussinessLayer.DTOs.ModuloGeneral.Monedas;
using DataLayer.Models.ModuloGeneral.Monedas;
using DataLayer.PDbContex;
using Microsoft.EntityFrameworkCore;

public class MonedasService : IMonedasService
{
    private readonly PDbContext _context;
    private readonly IMapper _mapper;
    private readonly ITokenService _tokenService;

    public MonedasService(PDbContext dbContext,
        IMapper mapper, ITokenService tokenService)
    {
        _context = dbContext;
        _mapper = mapper;
        _tokenService = tokenService;
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

    public async Task<List<ViewCurrencyDTO>> GetAll()
    {
        var list = await _context.Monedas
            .ToListAsync();

        return _mapper.Map<List<ViewCurrencyDTO>>(list);
    }

    public async Task<ViewCurrencyDTO> GetById(int id)
    {
        var product = await _context.Monedas
            .FirstOrDefaultAsync(x => x.Id == id);

        return _mapper.Map<ViewCurrencyDTO>(product);
    }

    public async Task Update(Moneda model)
    {
        _context.Entry(model).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }
}

