using BussinessLayer.Interface.IOtros;
using DataLayer.Models.Caja;
using DataLayer.PDbContex;
using Microsoft.EntityFrameworkCore;

namespace BussinessLayer.Services.SOtros
{
    public class ConciliacionTCTFsService : IConciliacionTCTFsService
    {
        private readonly PDbContext _context;

        public ConciliacionTCTFsService(PDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task Add(ConciliacionTCTF model)
        {
            _context.ConciliacionTCTFs.Add(model);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(ConciliacionTCTF model)
        {
            _context.ConciliacionTCTFs.Remove(model);
            await _context.SaveChangesAsync();
        }

        public async Task<List<ConciliacionTCTF>> GetAll()
        {
            return await _context.ConciliacionTCTFs.ToListAsync();
        }

        public async Task<List<ConciliacionTCTF>> GetAllIndex()
        {
            var data = _context.ConciliacionTCTFs
                .Include(c => c.caja)
                .Include(c => c.empresa)
                .Include(c => c.facturacion)
                .Include(c => c.moneda)
                .Include(c => c.sucursal);

            return await data.ToListAsync();
        }

        public async Task<ConciliacionTCTF> GetById(int id)
        {
            var result = await _context.ConciliacionTCTFs.FindAsync(id);

            return result;
        }

        public async Task Update(ConciliacionTCTF model)
        {
            _context.Entry(model).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
