using Microsoft.EntityFrameworkCore;
using DataLayer.PDbContex;
using AutoMapper;
using BussinessLayer.DTOs.ModuloInventario.Suplidores;

public class SuplidoresService : ISuplidoresService
{
    private readonly PDbContext _context;
    private readonly IMapper _mapper;
    private readonly ITokenService _tokenService;

    public SuplidoresService(PDbContext dbContext,
        IMapper mapper,
        ITokenService tokenService)
    {
        _context = dbContext;
        _mapper = mapper;
        _tokenService = tokenService;
    }

    // Obtener data por su id
    public async Task<ViewSuppliersDto> GetById(int id)
    {
        var data = await _context.Suplidores
            .Where(x => x.Id == id && x.Borrado == false)
            .FirstOrDefaultAsync();
        return _mapper.Map<ViewSuppliersDto>(data);
    }

    // Obtener data por su empresa
    public async Task<List<ViewSuppliersDto>> GetByCompany(int id)
    {
        var data = await _context.Suplidores
            .Where(x => x.IdEmpresa == id && x.Borrado == false)
            .ToListAsync();

        return _mapper.Map<List<ViewSuppliersDto>>(data);
    }

    // Crear un nuevo 
    public async Task<int?> Create(CreateSuppliersDto create)
    {
        var newObject = _mapper.Map<Suplidores>(create);
        newObject.FechaCreacion = DateTime.Now;
        newObject.Borrado = false;
        //newObject.Activo = false;
        newObject.UsuarioCreacion = _tokenService.GetClaimValue("sub") ?? "UsuarioDesconocido";

        _context.Suplidores.Add(newObject);
        await _context.SaveChangesAsync();

        return newObject.Id;
    }

    // Editar existente
    public async Task Edit(EditSuppliersDto edit)
    {
        var existing = await _context.Suplidores.FirstOrDefaultAsync(x => x.Id == edit.Id);
        //var activox = existing?.Activo;
        if (existing != null)
        {
            _mapper.Map(edit, existing);
            existing.UsuarioModificacion = _tokenService.GetClaimValue("sub") ?? "UsuarioDesconocido";
            existing.FechaModificacion = DateTime.Now;
           // existing.Activo = activox;
            _context.Suplidores.Update(existing);
            await _context.SaveChangesAsync();
        }
    }

    // Servicio para eliminar por id unico
    public async Task DeleteById(int id)
    {
        var data = await _context.Suplidores.Where(x => x.Id == id).FirstOrDefaultAsync();

        if (data != null)
        {
            data.Borrado = true;
            _context.Suplidores.Update(data);
            await _context.SaveChangesAsync();
        }
    }
}

