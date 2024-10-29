using BussinessLayer.Interface.IAlmacenes;
using DataLayer.Models.Almacen;
using DataLayer.PDbContex;
using Microsoft.EntityFrameworkCore;

namespace BussinessLayer.Services.SALmacenes
{
    public  class DetalleMovimientoAlmacenService : IDetalleMovimientoAlmacenService
    {
        private readonly PDbContext _context;

        public DetalleMovimientoAlmacenService(PDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task Add(DetalleMovimientoAlmacen[] entity)
        {
            try
            {
                foreach(var d in entity)
                {
                    _context.DetalleMovimientoAlmacenes.Add(d);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task Edit(DetalleMovimientoAlmacen entity)
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

        public async Task<DetalleMovimientoAlmacen> GetById(int id, long idEMpresa)
        {
            try
            {
                return await _context.DetalleMovimientoAlmacenes.FindAsync(id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<IList<DetalleMovimientoAlmacen>> GetAll(long idEMpresa)
        {
            return await _context.DetalleMovimientoAlmacenes.Where(x => x.Borrado != true).ToListAsync();
        }

        public async Task Delete(int id, long idEMpresa)
        {
            var entity = await _context.DetalleMovimientoAlmacenes.FindAsync(id);
            if (entity != null)
            {
                entity.Borrado = true;
                await _context.SaveChangesAsync();
            }
        }

        public Task Add(DetalleMovimientoAlmacen entity)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<DetalleMovimientoAlmacen>> GetDetalleMovimientoByMovimientoId(int id, long idEmpresa)
        {
            return await _context.DetalleMovimientoAlmacenes.Where(x => x.IdMovimiento == id && x.IdEmpresa== idEmpresa).ToListAsync();
        }
    }
}
