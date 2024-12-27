using AutoMapper;
using BussinessLayer.DTOs.ModuloGeneral.Archivos;
using BussinessLayer.Interfaces.Services.ModuloGeneral.Archivos;
using DataLayer.Models.ModuloGeneral.Archivos;
using DataLayer.PDbContex;
using Microsoft.EntityFrameworkCore;

namespace BussinessLayer.Services.ModuloGeneral.Archivos
{
    public class GnTecnoAlmacenExternoService : IGnTecnoAlmacenExternoService
    {
        private readonly PDbContext _context;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;

        public GnTecnoAlmacenExternoService(PDbContext dbContext,
            IMapper mapper, ITokenService tokenService)
        {
            _context = dbContext;
            _mapper = mapper;
            _tokenService = tokenService;
        }

        // Método para agregar un nuevo "GnTecnoAlmacenExterno"
        public async Task<int> Add(CreateGnTecnoAlmacenExternoDto model)
        {
            var newEntity = _mapper.Map<GnTecnoAlmacenExterno>(model);

            newEntity.FechaAdicion = DateTime.Now;
            newEntity.Borrado = false;
            newEntity.UsuarioAdicion = _tokenService.GetClaimValue("sub") ?? "UsuarioDesconocido";
            newEntity.Estado = true;

            _context.GnTecnoAlmacenExterno.Add(newEntity);
            await _context.SaveChangesAsync();

            return newEntity.Id;
        }

        // Método para eliminar un "GnTecnoAlmacenExterno"
        public async Task Delete(int id)
        {
            var entity = await _context.GnTecnoAlmacenExterno
                .FirstOrDefaultAsync(x => x.Id == id && x.Borrado == false);

            if (entity != null)
            {
                entity.Borrado = true;
                entity.Estado = false;
                _context.Update(entity);
                await _context.SaveChangesAsync();
            }
        }

        // Método para obtener todos los "GnTecnoAlmacenExterno"
        public async Task<List<ViewGnTecnoAlmacenExternoDto>> GetAll()
        {
            var list = await _context.GnTecnoAlmacenExterno
                .Where(x => x.Borrado == false)
            .ToListAsync();

            return _mapper.Map<List<ViewGnTecnoAlmacenExternoDto>>(list);
        }

        // Método para obtener un "GnTecnoAlmacenExterno" por ID
        public async Task<ViewGnTecnoAlmacenExternoDto> GetById(int id)
        {
            var entity = await _context.GnTecnoAlmacenExterno
                .FirstOrDefaultAsync(x => x.Id == id && x.Borrado == false);

            return _mapper.Map<ViewGnTecnoAlmacenExternoDto>(entity);
        }

        // Método para obtener "GnTecnoAlmacenExterno" por Empresa
        public async Task<List<ViewGnTecnoAlmacenExternoDto>> GetByCompany(int idEmpresa)
        {
            var entities = await _context.GnTecnoAlmacenExterno
                .Where(x => x.Borrado == false && x.IdEmpresa == idEmpresa)
            .ToListAsync();

            return _mapper.Map<List<ViewGnTecnoAlmacenExternoDto>>(entities);
        }

        // Método para actualizar un "GnTecnoAlmacenExterno"
        public async Task Update(EditGnTecnoAlmacenExternoDto model)
        {
            var existing = await _context.GnTecnoAlmacenExterno
                .FirstOrDefaultAsync(x => x.Id == model.Id && x.Borrado == false);

            var estado = existing?.Estado;

            if (existing != null)
            {
                existing.FechaModificacion = DateTime.Now;
                existing.UsuarioModificacion = _tokenService.GetClaimValue("sub") ?? "UsuarioDesconocido";
                existing.Estado = estado ?? false;

                _context.GnTecnoAlmacenExterno.Update(existing);
                await _context.SaveChangesAsync();
            }
        }
    }

}
