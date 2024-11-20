using Microsoft.EntityFrameworkCore;
using DataLayer.PDbContex;
using AutoMapper;
using DataLayer.Models.ModuloInventario.Descuento;
using BussinessLayer.DTOs.ModuloInventario.Descuentos;

public class DescuentoService : IDescuentoService
{
    private readonly PDbContext _context;
    private readonly IMapper _mapper;
    private readonly ITokenService _tokenService;

    public DescuentoService(PDbContext dbContext,
        IMapper mapper,
        ITokenService tokenService)
    {
        _context = dbContext;
        _mapper = mapper;
        _tokenService = tokenService;
    }

    // Obtener data por su id
    public async Task<ViewDiscountDto> GetDiscountById(int id)
    {
        var data = await _context.Descuentos
            .Where(x => x.Id == id && x.Borrado == false)
            .FirstOrDefaultAsync();
        return _mapper.Map<ViewDiscountDto>(data);
    }

    // Obtener data por su empresa
    public async Task<List<ViewDiscountDto>> GetDiscountByCompany(int id)
    {
        var data = await _context.Descuentos
            .Where(x => x.IdEmpresa == id && x.Borrado == false)
            .ToListAsync();

        return _mapper.Map<List<ViewDiscountDto>>(data);
    }

    // Crear un nuevo 
    public async Task<int?> CreateDiscount(CreateDiscountDto create)
    {
        var newObject = _mapper.Map<Descuentos>(create);
        newObject.FechaCreacion = DateTime.Now;
        newObject.Borrado = false;
        newObject.Activo = false;
        newObject.UsuarioCreacion = _tokenService.GetClaimValue("sub") ?? "UsuarioDesconocido";

        _context.Descuentos.Add(newObject);
        await _context.SaveChangesAsync();

        return newObject.Id;    
    }

    // Editar existente
    public async Task EditDiscount(EditDiscountDto edit)
    {
        var existing = await _context.Descuentos.FirstOrDefaultAsync(x => x.Id == edit.Id);
        var activox = existing?.Activo;
        if (existing != null)
        {
            _mapper.Map(edit, existing);
            existing.UsuarioModificacion = _tokenService.GetClaimValue("sub") ?? "UsuarioDesconocido";
            existing.FechaModificacion = DateTime.Now;
            existing.Activo = activox;
            _context.Descuentos.Update(existing);
            await _context.SaveChangesAsync();
        }
    }

    // Servicio para eliminar por id unico
    public async Task DeleteDiscountById(int id)
    {
        var data = await _context.Descuentos.Where(x => x.Id == id).FirstOrDefaultAsync();

        if (data != null)
        {
            data.Borrado = true;
            _context.Descuentos.Update(data);
            await _context.SaveChangesAsync();
        }
    }
}
