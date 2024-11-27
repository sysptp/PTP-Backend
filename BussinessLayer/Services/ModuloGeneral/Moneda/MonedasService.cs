using BussinessLayer.DTOs.ModuloGeneral.Monedas;
using DataLayer.Models.ModuloGeneral.Monedas;
using DataLayer.PDbContex;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

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

    public async Task<int> Add(CreateCurrencyDTO model)
    {
        var newDta = _mapper.Map<Moneda>(model);

        newDta.FechaCreacion = DateTime.Now;
        newDta.Borrado = false;
        newDta.UsuarioCreacion = _tokenService.GetClaimValue("sub") ?? "UsuarioDesconocido";
        newDta.Activo = false;

        _context.Monedas.Add(newDta);
        await _context.SaveChangesAsync();

        return newDta.Id;
    }

    public async Task Delete(int Id)
    {
        var producto = await _context.Monedas
            .FirstOrDefaultAsync(x => x.Id == Id 
            && x.Borrado == false);

        if (producto != null)
        {
            producto.Borrado = true;
            _context.Update(producto);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<List<ViewCurrencyDTO>> GetAll()
    {
        var list = await _context.Monedas
            .Where(x => x.Borrado == false)
            .ToListAsync();

        return _mapper.Map<List<ViewCurrencyDTO>>(list);
    }

    public async Task<ViewCurrencyDTO> GetById(int id)
    {
        var product = await _context.Monedas
            .FirstOrDefaultAsync(x => x.Id == id 
            && x.Borrado == false);

        return _mapper.Map<ViewCurrencyDTO>(product);
    }

    public async Task<List<ViewCurrencyDTO>> GetByCompany(int idEmpresa)
    {
        var product = await _context.Monedas
            .FirstOrDefaultAsync(x => x.Borrado == false 
            && x.IdEmpresa == idEmpresa);

        return _mapper.Map<List<ViewCurrencyDTO>>(product);
    }

    public async Task Update(EditCurrencyDTO model)
    {
        var existing = await _context.Monedas
            .FirstOrDefaultAsync(x => x.Id == model.Id 
            && x.Borrado == false);

        var activox = existing.Activo;

        if (existing != null)
        {
            //_mapper.Map(producto, existing);
            existing.FechaModificacion = DateTime.Now;
            existing.UsuarioModificacion = _tokenService.GetClaimValue("sub") ?? "UsuarioDesconocido";
            existing.Activo = activox;
            _context.Monedas.Update(existing);
            await _context.SaveChangesAsync();
        }
    }
}

