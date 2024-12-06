using BussinessLayer.Interfaces.ModuloVentas.ICaja;
using DataLayer.Models.ModuloVentas.Caja;
using DataLayer.PDbContex;
using Microsoft.EntityFrameworkCore;

namespace BussinessLayer.Services.ModuloVentas.Caja
{
    public class AperturaCierreCajasService : IAperturaCierreCajasService
    {
        // CREADO POR MANUEL 17/10/2024
        private readonly PDbContext _context;

        public AperturaCierreCajasService(PDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task Add(AperturaCierreCaja model)
        {
            _context.AperturaCierreCajas.Add(model);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(AperturaCierreCaja model)
        {
            _context.AperturaCierreCajas.Remove(model);
            await _context.SaveChangesAsync();
        }

        public async Task<List<AperturaCierreCaja>> GetAll()
        {
            return await _context.AperturaCierreCajas.ToListAsync();
        }

        public async Task<List<AperturaCierreCaja>> GetAllIndex()
        {
            var data = _context.AperturaCierreCajas
                .Include(a => a.caja)
                .Include(a => a.empresa)
                .Include(a => a.sucursal)
                .Include(a => a.usuarioConfirmaCierreA)
                .Include(a => a.usuarioConfirmaTF)
                .Include(a => a.usuarioRespCaja);

            return await data.ToListAsync();
        }

        public async Task<AperturaCierreCaja> GetById(int id)
        {
            var result = await _context.AperturaCierreCajas.FindAsync(id);

            return result;
        }

        public async Task Update(AperturaCierreCaja model)
        {
            _context.Entry(model).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
