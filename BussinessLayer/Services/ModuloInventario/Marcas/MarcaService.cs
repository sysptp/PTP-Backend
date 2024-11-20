using Microsoft.EntityFrameworkCore;
using DataLayer.PDbContex;
using DataLayer.Models.ModuloInventario.Marcas;
using BussinessLayer.DTOs.ModuloInventario.Marcas;
using AutoMapper;

public class MarcaService : IMarcaService
{
    private readonly PDbContext _context;
    private readonly IMapper _mapper;
    private readonly ITokenService _tokenService;

    public MarcaService(PDbContext dbContext,
        IMapper mapper,
        ITokenService tokenService)
    {
        _context = dbContext;
        _mapper = mapper;
        _tokenService = tokenService;
    }

    // Obtener data por su id
    public async Task<ViewBrandDto> GetBrandById(int id)
    {
        var data = await _context.Marcas
            .Where(x => x.Id == id && x.Borrado == false)
            .FirstOrDefaultAsync();
        return _mapper.Map<ViewBrandDto>(data);
    }

    // Obtener data por su empresa
    public async Task<List<ViewBrandDto>> GetBrandsByCompany(int id)
    {
        var data = await _context.Marcas
            .Where(x => x.IdEmpresa == id && x.Borrado == false)
            .ToListAsync();

        return _mapper.Map<List<ViewBrandDto>>(data);
    }

    // Crear un nuevo 
    public async Task<int?> CreateBrand(CreateBrandDto create)
    {
        var newObject = _mapper.Map<InvMarcas>(create);
        newObject.Activo = false;
        newObject.FechaCreacion = DateTime.Now;
        newObject.Borrado = false;
        newObject.UsuarioCreacion = _tokenService.GetClaimValue("sub") ?? "UsuarioDesconocido";

        _context.Marcas.Add(newObject);
        await _context.SaveChangesAsync();

        return newObject.Id;
    }

    // Editar existente
    public async Task EditBrand(EditBrandDto edit)
    {
        var existing = await _context.Marcas.FirstOrDefaultAsync(x => x.Id == edit.Id);

        if (existing != null)
        {
            _mapper.Map(edit, existing);
            existing.UsuarioModificacion = _tokenService.GetClaimValue("sub") ?? "UsuarioDesconocido";
            existing.FechaModificacion = DateTime.Now;
            _context.Marcas.Update(existing);
            await _context.SaveChangesAsync();
        }
    }

    // Servicio para eliminar por id unico
    public async Task DeleteBrandById(int id)
    {
        var data = await _context.Marcas.Where(x => x.Id == id).FirstOrDefaultAsync();

        if (data != null)
        {
            data.Borrado = true;
            data.Activo = false;
            _context.Update(data);
            await _context.SaveChangesAsync();
        }
    }
}

