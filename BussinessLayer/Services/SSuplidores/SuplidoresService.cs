using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using BussinessLayer.Interface.ISuplidores;
using DataLayer.Models.Suplidor;
using DataLayer.PDbContex;

namespace BussinessLayer.Services.SSuplidores
{
    public class SuplidoresService : ISuplidoresService
    {
        private readonly PDbContext _context;

        public SuplidoresService(PDbContext dbContext)
        {
            _context = dbContext;
        }
   
        public async Task AddSuplidores(Suplidores entity)
        {
            try
            {
                _context.Suplidores.Add(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }        
        }

        public async Task Edit(Suplidores entity)
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

        public async Task<Suplidores> GetById(int id, long idEMpresa)
        {
            return await _context.Suplidores.Where(x=>x.Id==id && x.IdEmpresa==idEMpresa).FirstOrDefaultAsync(); 
        }

        public async Task<IList<Suplidores>> GetAll(long idEMpresa)
        {
            return await  _context.Suplidores.Where(x => x.Borrado != true && x.IdEmpresa== idEMpresa).ToListAsync();
        }

        public async Task Delete(int id, long idEMpresa)
        {

            var cl = await _context.Suplidores.FindAsync(id);
            if (cl == null) return;

            cl.Borrado = true;
            await _context.SaveChangesAsync();
            
        }

        public Task Add(Suplidores entity)
        {
            throw new NotImplementedException();
        }
    }
}
