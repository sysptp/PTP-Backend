using AutoMapper;
using BussinessLayer.DTOs.ModuloGeneral.Archivos;
using BussinessLayer.Interfaces.ModuloGeneral.Archivos;
using DataLayer.Models.ModuloGeneral.Archivos;
using DataLayer.PDbContex;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.Services.ModuloGeneral.Archivos
{
    public class GnUploadFileParametroService : IGnUploadFileParametroService
    {
        private readonly PDbContext _context;
        private readonly IMapper _mapper;

        public GnUploadFileParametroService(PDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> Add(CreateGnUploadFileParametroDto model)
        {
            var entity = _mapper.Map<GnUploadFileParametro>(model);
            _context.GnUploadFileParametro.Add(entity);
            await _context.SaveChangesAsync();
            return entity.Id;
        }

        public async Task Update(EditGnUploadFileParametroDto model)
        {
            var entity = await _context.GnUploadFileParametro.FindAsync(model.Id);
            if (entity == null) throw new KeyNotFoundException("Parámetro no encontrado.");

            _mapper.Map(model, entity);
            _context.GnUploadFileParametro.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var entity = await _context.GnUploadFileParametro.FindAsync(id);
            if (entity == null) throw new KeyNotFoundException("Parámetro no encontrado.");

            entity.Borrado = true;
            _context.GnUploadFileParametro.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<ViewGnUploadFileParametroDto> GetById(int id)
        {
            var entity = await _context.GnUploadFileParametro.FindAsync(id);
            return _mapper.Map<ViewGnUploadFileParametroDto>(entity);
        }

        public async Task<List<ViewGnUploadFileParametroDto>> GetAll()
        {
            var entities = await _context.GnUploadFileParametro.Where(x => !x.Borrado).ToListAsync();
            return _mapper.Map<List<ViewGnUploadFileParametroDto>>(entities);
        }
    }

}
