using AutoMapper;
using BussinessLayer.DTOs.ModuloInventario.Otros;
using BussinessLayer.Interfaces.ModuloInventario.Otros;
using DataLayer.Models.ModuloInventario.Otros;
using DataLayer.PDbContex;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.Services.ModuloInventario.Otros
{
    public class InvProductoImpuestoService : IInvProductoImpuestoService
    {
        private readonly PDbContext _context;
        private readonly IMapper _mapper;

        public InvProductoImpuestoService(PDbContext dbContext, IMapper mapper)
        {
            _context = dbContext;
            _mapper = mapper;
        }

        public async Task<int> Add(CreateInvProductoImpuestoDto model)
        {
            var newData = _mapper.Map<InvProductoImpuesto>(model);
            _context.InvProductoImpuestos.Add(newData);
            await _context.SaveChangesAsync();
            return newData.Id;
        }

        public async Task Update(EditInvProductoImpuestoDto model)
        {
            var existing = await _context.InvProductoImpuestos.FirstOrDefaultAsync(x => x.Id == model.Id);
            if (existing != null)
            {
                _mapper.Map(model, existing);
                _context.InvProductoImpuestos.Update(existing);
                await _context.SaveChangesAsync();
            }
        }

        public async Task Delete(int id)
        {
            var entity = await _context.InvProductoImpuestos.FirstOrDefaultAsync(x => x.Id == id);
            if (entity != null)
            {
                _context.InvProductoImpuestos.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<ViewInvProductoImpuestoDto> GetById(int id)
        {
            var entity = await _context.InvProductoImpuestos
                .Include(x => x.Producto)
                .Include(x => x.Impuesto)
                .FirstOrDefaultAsync(x => x.Id == id);

            return _mapper.Map<ViewInvProductoImpuestoDto>(entity);
        }

        public async Task<List<ViewInvProductoImpuestoDto>> GetAll()
        {
            var list = await _context.InvProductoImpuestos
                .Include(x => x.Producto)
                .Include(x => x.Impuesto)
                .ToListAsync();

            return _mapper.Map<List<ViewInvProductoImpuestoDto>>(list);
        }
    }
}
