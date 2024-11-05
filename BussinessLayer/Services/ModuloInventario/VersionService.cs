using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.PDbContex;
using DataLayer.Models.ModuloInventario;
using BussinessLayer.Interfaces.ModuloInventario;

namespace BussinessLayer.Services.ModuloInventario
{
    public class VersionService : IVersionService
    {
        private readonly PDbContext _context;

        public VersionService(PDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task Add(Versiones entity)
        {
            try
            {
                _context.Versiones.Add(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task Edit(Versiones entity)
        {
            try
            {
                var oldVersion = await _context.Versiones.FindAsync(entity.Id);
                if (oldVersion == null) return;

                oldVersion.Nombre = entity.Nombre;
                oldVersion.Activo = entity.Activo;
                oldVersion.FechaModificacion = DateTime.Now;
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<Versiones> GetById(int id, long idEMpresa)
        {
            return await _context.Versiones.Include(x => x.Marca.IdEmpresa == idEMpresa).SingleOrDefaultAsync(x => x.Id.Equals(id));

        }

        public async Task<IList<Versiones>> GetAll(long idEMpresa)
        {
            try
            {
                return await _context.Versiones.Include(x => x.Marca).Where(x => x.Borrado != true && x.IdEmpresa == idEMpresa).ToListAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task Delete(int id, long idEMpresa)
        {
            var version = await _context.Versiones.Where(x => x.IdEmpresa == idEMpresa && x.Id == id).FirstOrDefaultAsync();
            if (version == null) return;

            version.Borrado = true;
            await _context.SaveChangesAsync();
        }

        public async Task<IList<Versiones>> GetVersionesByMarca(int? id, long idempresa)
        {
            if (id == null) return new List<Versiones>();
            return await _context.Versiones.Include(x => x.Marca).Where(x => x.IdMarca == id && x.IdEmpresa == idempresa)
                .OrderBy(x => x.Nombre).ToListAsync();

        }
    }
}
