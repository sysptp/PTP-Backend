using AutoMapper;
using BussinessLayer.DTOs.ModuloReporteria;
using BussinessLayer.Interfaces.ModuloReporteria;
using DataLayer.Models.ModuloReporteria;
using DataLayer.PDbContex;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.Services.ModuloReportes
{
    public class RepReportesVariableService : IRepReportesVariableService
    {
        private readonly PDbContext _context;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;

        public RepReportesVariableService(PDbContext context, IMapper mapper, ITokenService tokenService)
        {
            _context = context;
            _mapper = mapper;
            _tokenService = tokenService;
        }

        public async Task<int> Add(CreateRepReportesVariableDto model)
        {
            var entity = _mapper.Map<RepReportesVariables>(model);
            entity.FechaAdicion = DateTime.Now;
            entity.UsuarioAdicion = _tokenService.GetClaimValue("sub") ?? "UsuarioDesconocido";
            _context.RepReportesVariables.Add(entity);
            await _context.SaveChangesAsync();
            return entity.Id;
        }

        public async Task Update(EditRepReportesVariableDto model)
        {
            var entity = await _context.RepReportesVariables.FindAsync(model.Id);
            if (entity == null) throw new KeyNotFoundException("Variable no encontrada.");

            _mapper.Map(model, entity);
            entity.FechaModificacion = DateTime.Now;
            entity.UsuarioModificacion = _tokenService.GetClaimValue("sub") ?? "UsuarioDesconocido";
            _context.RepReportesVariables.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var entity = await _context.RepReportesVariables.FindAsync(id);
            if (entity == null) throw new KeyNotFoundException("Variable no encontrada.");

            entity.Borrado = true;
            _context.RepReportesVariables.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<ViewRepReportesVariableDto> GetById(int id)
        {
            var entity = await _context.RepReportesVariables.FindAsync(id);
            return _mapper.Map<ViewRepReportesVariableDto>(entity);
        }

        public async Task<List<ViewRepReportesVariableDto>> GetAll()
        {
            var entities = await _context.RepReportesVariables.Where(x => !x.Borrado).ToListAsync();
            return _mapper.Map<List<ViewRepReportesVariableDto>>(entities);
        }
    }
}
