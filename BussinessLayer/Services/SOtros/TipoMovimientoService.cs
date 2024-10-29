using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using BussinessLayer.Interface.IOtros;
using DataLayer.Models.Otros;
using DataLayer.PDbContex;

namespace BussinessLayer.Services.SOtros
{
    public class TipoMovimientoService : ITipoMovimientoService
    {
        private readonly PDbContext _context;

        public TipoMovimientoService(PDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task Add(TipoMovimiento entity)
        {
            try
            {
                _context.TipoMovimientos.Add(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task Edit(TipoMovimiento entity)
        {
            try
            {
                _context.Entry(entity).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<TipoMovimiento> GetById(int id, long idEMpresa)
        {
            return await _context.TipoMovimientos.Where(x=>x.Id==id && x.IdEmpresa== idEMpresa).FirstOrDefaultAsync();
        }

        public async Task<IList<TipoMovimiento>> GetAll(long idEMpresa)
        {
            try
            {
                return await _context.TipoMovimientos.Where(x => x.Borrado != true).ToListAsync();
                 
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task Delete(int id, long idEMpresa)
        {

            var entity = await _context.TipoMovimientos.FindAsync(id);
            if (entity != null)
            {
                entity.Borrado = true;
                await _context.SaveChangesAsync();
            }

        }
    }
}
