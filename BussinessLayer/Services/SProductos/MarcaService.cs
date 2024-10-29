using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using BussinessLayer.Interface.IProductos;
using DataLayer.Models.Productos;
using DataLayer.PDbContex;

namespace BussinessLayer.Services.SProductos
{
    public class MarcaService : IMarcaService
    {
        private readonly PDbContext _context;

        public MarcaService(PDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task Add(Marca entity)
        {
            try
            {
                _context.Marcas.Add(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task Edit(Marca entity)
        {
            try
            {
                entity.FechaModificacion = DateTime.Now;
                _context.Entry(entity).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }        
        }

        public async Task<Marca> GetById(int id, long idEMpresa)
        {
            return await _context.Marcas.Where(x=>x.Id==id && x.IdEmpresa==idEMpresa).FirstOrDefaultAsync();
            
        }

        public async Task<IList<Marca>> GetAll(long idEMpresa)
        {
            try
            {
                return await _context.Marcas.Where(x => x.Borrado != true && x.IdEmpresa==idEMpresa).OrderBy(x => x.Nombre).ToListAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }      
        }

        public async Task Delete(int id, long idEMpresa)
        {
            var marca = await _context.Marcas.Where(x => x.Borrado != true && x.IdEmpresa == idEMpresa).FirstOrDefaultAsync();
            if(marca == null) return;
            marca.Borrado = true;
            await _context.SaveChangesAsync();         
        }
    }
}
