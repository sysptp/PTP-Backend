using Microsoft.EntityFrameworkCore;
using DataLayer.PDbContex;
using AutoMapper;
using BussinessLayer.DTOs.ModuloInventario.Versiones;

public class VersionService : IVersionService
{
    private readonly PDbContext _context;
    private readonly IMapper _mapper;
    private readonly ITokenService _tokenService;

    public VersionService(PDbContext dbContext,
        IMapper mapper,
        ITokenService tokenService)
    {
        _context = dbContext;
        _mapper = mapper;
        _tokenService = tokenService;
    }

    // Obtener data por su id
    public async Task<ViewVersionsDto> GetVersionById(int id)
    {
        var data = await _context.Versiones
            .Include(x => x.Marca)
            .Where(x => x.Id == id && x.Borrado == false)
            .FirstOrDefaultAsync();
        return _mapper.Map<ViewVersionsDto>(data);
    }

    // Obtener data por su empresa
    public async Task<List<ViewVersionsDto>> GetVersionByCompany(int id)
    {
        var data = await _context.Versiones
            .Include(x => x.Marca)
            .Where(x => x.IdEmpresa == id && x.Borrado == false)
            .ToListAsync();

        return _mapper.Map<List<ViewVersionsDto>>(data);
    }

    // Crear un nuevo 
    public async Task<int?> CreateVersion(CreateVersionsDto create)
    {
        var newObject = _mapper.Map<Versiones>(create);
        newObject.FechaCreacion = DateTime.Now;
        newObject.Activo = false;
        newObject.Borrado = false;
        newObject.UsuarioCreacion = _tokenService.GetClaimValue("sub") ?? "UsuarioDesconocido";

        _context.Versiones.Add(newObject);
        await _context.SaveChangesAsync();

        return newObject.Id;
    }

    // Editar existente
    public async Task EditVersion(EditVersionsDto edit)
    {
        var existing = await _context.Versiones.FirstOrDefaultAsync(x => x.Id == edit.Id);

        if (existing != null)
        {
            _mapper.Map(edit, existing);
            existing.UsuarioModificacion = _tokenService.GetClaimValue("sub") ?? "UsuarioDesconocido";
            existing.FechaModificacion = DateTime.Now;
            _context.Versiones.Update(existing);
            await _context.SaveChangesAsync();
        }
    }

    // Servicio para eliminar por id unico
    public async Task DeleteVersionById(int id)
    {
        var data = await _context.Versiones.Where(x => x.Id == id).FirstOrDefaultAsync();

        if (data != null)
        {
            data.Borrado = true;
            data.Activo = false;
            _context.Update(data);
            await _context.SaveChangesAsync();
        }
    }
}

