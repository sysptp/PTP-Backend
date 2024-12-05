using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.Models.Otros;
using DataLayer.PDbContex;
using AutoMapper;
using BussinessLayer.DTOs.ModuloInventario.Otros;
using BussinessLayer.Interfaces.ModuloInventario.Otros;

namespace BussinessLayer.Services.ModuloInventario.Otros
{
    public class TipoMovimientoService : ITipoMovimientoService
    {
        private readonly PDbContext _context;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;

        public TipoMovimientoService(PDbContext context, IMapper mapper, ITokenService tokenService)
        {
            _context = context;
            _mapper = mapper;
            _tokenService = tokenService;
        }

        public async Task<int> Add(CreateTipoMovimientoDto model)
        {
            // Mapea el DTO a la entidad
            var newData = _mapper.Map<TipoMovimiento>(model);

            // Asigna valores predeterminados
            newData.FechaCreacion = DateTime.Now;
            newData.Borrado = false;
            newData.UsuarioCreacion = _tokenService.GetClaimValue("sub") ?? "UsuarioDesconocido";

            // Agrega la entidad al contexto
            _context.TipoMovimientos.Add(newData);
            await _context.SaveChangesAsync();

            // Retorna el ID de la entidad recién creada
            return newData.Id;
        }

        public async Task Delete(int Id)
        {
            // Busca el tipo de movimiento por su ID y no debe estar borrado
            var existing = await _context.TipoMovimientos
                .FirstOrDefaultAsync(x => x.Id == Id && x.Borrado == false);

            // Si existe, marca como borrado
            if (existing != null)
            {
                existing.Borrado = true;
                existing.FechaModificacion = DateTime.Now;
                existing.UsuarioModificacion = _tokenService.GetClaimValue("sub") ?? "UsuarioDesconocido";

                // Actualiza la entidad en el contexto
                _context.TipoMovimientos.Update(existing);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<ViewTipoMovimientoDto>> GetAll()
        {
            // Obtiene todos los tipos de movimiento que no estén borrados
            var list = await _context.TipoMovimientos
                .Where(x => x.Borrado == false)
                .ToListAsync();

            // Mapea las entidades a los DTOs y los retorna
            return _mapper.Map<List<ViewTipoMovimientoDto>>(list);
        }

        public async Task<ViewTipoMovimientoDto> GetById(int id)
        {
            // Busca un tipo de movimiento por su ID y no debe estar borrado
            var existing = await _context.TipoMovimientos
                .FirstOrDefaultAsync(x => x.Id == id && x.Borrado == false);

            // Mapea la entidad a un DTO y lo retorna
            return _mapper.Map<ViewTipoMovimientoDto>(existing);
        }

        public async Task<List<ViewTipoMovimientoDto>> GetByCompany(int idEmpresa)
        {
            // Busca los tipos de movimiento de una empresa específica que no estén borrados
            var list = await _context.TipoMovimientos
                .Where(x => x.Borrado == false && x.IdEmpresa == idEmpresa)
                .ToListAsync();

            // Mapea las entidades a DTOs y los retorna
            return _mapper.Map<List<ViewTipoMovimientoDto>>(list);
        }

        public async Task Update(EditTipoMovimientoDto model)
        {
            // Busca el tipo de movimiento por su ID y no debe estar borrado
            var existing = await _context.TipoMovimientos
                .FirstOrDefaultAsync(x => x.Id == model.Id && x.Borrado == false);

            // Si la entidad existe, se actualiza
            if (existing != null)
            {
                // Mantiene el valor de IN_OUT
                var inOut = existing.IN_OUT;

                // Actualiza los campos necesarios
                existing.Nombre = model.Nombre;
                existing.IN_OUT = model.IN_OUT; // Mantiene el valor actual si el nuevo valor es null
                existing.FechaModificacion = DateTime.Now;
                existing.UsuarioModificacion = _tokenService.GetClaimValue("sub") ?? "UsuarioDesconocido";

                // Actualiza la entidad en el contexto
                _context.TipoMovimientos.Update(existing);
                await _context.SaveChangesAsync();
            }
        }
    }

}
