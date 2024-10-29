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
    public class TipoPagoService : ITipoPagoService
    {
        private readonly PDbContext _context;

        public TipoPagoService(PDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task Add(TipoPago entity)
        {
            try
            {
                _context.TipoPagos.Add(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task Edit(TipoPago entity)
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

        public async Task<TipoPago> GetById(int id, long idEMpresa)
        {
            return await _context.TipoPagos.Where(x=>x.Id==id && x.IdEmpresa== idEMpresa).FirstOrDefaultAsync();
        }

        public async Task<IList<TipoPago>> GetAll(long idEMpresa)
        {
            try
            {
                return await _context.TipoPagos.Where(x => x.Borrado != true && x.IdEmpresa== idEMpresa).ToListAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task Delete(int id, long idEMpresa)
        {
            var entity = await _context.TipoPagos.FindAsync(id);
            if (entity != null)
            {
                entity.Borrado = true;
                await _context.SaveChangesAsync();
            }
        }
    }
}
