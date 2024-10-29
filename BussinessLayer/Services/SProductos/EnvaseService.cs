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
    public class EnvaseService : IEnvaseService
    {
        private readonly PDbContext _context;

        public EnvaseService(PDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task Add(Envase entity)
        {
            try
            {
                _context.Envases.Add(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }       
        }

        public async Task Edit(Envase entity)
        {
            try
            {
                var oldEnvase = await _context.Envases.FindAsync(entity.Id);
                if (oldEnvase == null) return;

                oldEnvase.FechaModificacion = DateTime.Now;
                oldEnvase.Descripcion = entity.Descripcion;
                oldEnvase.Activo = entity.Activo;

                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }   
        }

        public async Task<Envase> GetById(int id, long idEMpresa)
        {
            return await _context.Envases.Where(x=>x.Id==id && x.IdEmpresa== idEMpresa).FirstOrDefaultAsync();
            
        }

        public async Task<IList<Envase>> GetAll(long idEMpresa)
        {
            try
            {
                return await _context.Envases.Where(x => x.Borrado != true && x.IdEmpresa==idEMpresa).OrderBy(x => x.Descripcion).ToListAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }  
        }

        public async Task Delete(int id, long idEMpresa)
        {
            var marca = await _context.Envases.Where(x=>x.IdEmpresa== idEMpresa && x.Id==id).FirstOrDefaultAsync();
            if (marca == null) return;

            marca.Borrado = true;
            await _context.SaveChangesAsync();     
        }
    }
}
