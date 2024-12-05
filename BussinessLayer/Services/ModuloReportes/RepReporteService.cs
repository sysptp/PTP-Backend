using AutoMapper;
using BussinessLayer.DTOs.ModuloReporteria;
using BussinessLayer.Interfaces.ModuloReporteria;
using DataLayer.Models.ModuloReporteria;
using DataLayer.PDbContex;
using Microsoft.EntityFrameworkCore;

namespace BussinessLayer.Services.ModuloReportes
{
    public class RepReporteService : IRepReporteService
    {
        private readonly PDbContext _context;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;

        public RepReporteService(PDbContext context, IMapper mapper, ITokenService tokenService)
        {
            _context = context;
            _mapper = mapper;
            _tokenService = tokenService;
        }

        public async Task<int> Add(CreateRepReporteDto model)
        {
            var newEntity = _mapper.Map<RepReporte>(model);
            newEntity.NumQuery = await GetNumQuery(newEntity.IdEmpresa);
            newEntity.FechaAdicion = DateTime.Now;
            newEntity.Borrado = false;
            newEntity.UsuarioAdicion = _tokenService.GetClaimValue("sub") ?? "UsuarioDesconocido";
            _context.RepReportes.Add(newEntity);
            await _context.SaveChangesAsync();
            return newEntity.Id;
        }

        public async Task Update(EditRepReporteDto model)
        {
            var existing = await _context.RepReportes.FindAsync(model.Id);
            if (existing == null) throw new Exception("Reporte no encontrado.");

            _mapper.Map(model, existing);
            existing.FechaModificacion = DateTime.Now;
            existing.UsuarioModificacion = _tokenService.GetClaimValue("sub") ?? "UsuarioDesconocido";
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var existing = await _context.RepReportes.FindAsync(id);
            if (existing == null) throw new Exception("Reporte no encontrado.");

            existing.Borrado = true;
            existing.Activo = false;
            await _context.SaveChangesAsync();
        }

        public async Task<ViewRepReporteDto?> GetById(int id)
        {
            var entity = await _context.RepReportes.FirstOrDefaultAsync(x => x.Id == id && x.Borrado == false);
            return _mapper.Map<ViewRepReporteDto?>(entity);
        }

        public async Task<List<ViewRepReporteDto>> GetAll()
        {
            var list = await _context.RepReportes.Where(x => x.Borrado == false).ToListAsync();
            return _mapper.Map<List<ViewRepReporteDto>>(list);
        }

        private async Task<int> GetNumQuery(int empresaId)
        {
            // Buscar el último registro creado para la empresa específica
            var lastEntity = await _context.RepReportes
                .Where(x => x.IdEmpresa == empresaId && x.Borrado == false)
                .OrderByDescending(x => x.NumQuery) // Ordenar por ID descendente
                .FirstOrDefaultAsync();

            // Si no hay registros previos, retornar 1 como el siguiente ID
            return lastEntity == null ? 1 : lastEntity.NumQuery + 1;
        }
    }
}
