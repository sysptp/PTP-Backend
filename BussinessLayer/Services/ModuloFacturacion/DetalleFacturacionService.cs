using BussinessLayer.Interfaces.ModuloFacturacion;
using DataLayer.Models.Facturas;
using DataLayer.PDbContex;
using Microsoft.EntityFrameworkCore;

namespace BussinessLayer.Services.ModuloFacturacion
{
    public class DetalleFacturacionService : IDetalleFacturacionService
    {
        private readonly PDbContext _context;

        public DetalleFacturacionService(PDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task Add(DetalleFacturacion entity)
        {
            if (entity != null)
            {
                try
                {
                    entity.Borrado = false;
                    _context.DetalleFacturacion.Add(entity);
                    await _context.SaveChangesAsync();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }

        public async Task Edit(DetalleFacturacion entity)
        {
            if (entity != null)
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
        }

        public Task<DetalleFacturacion> GetById(int id, long idEMpresa)
        {
            throw new NotImplementedException();
        }

        public Task<IList<DetalleFacturacion>> GetAll(long idEMpresa)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id, long idEMpresa)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<DetalleFacturacion>> GetDetalleByFacturacionId(int facturacionId)
        {
            try
            {
                return await _context.DetalleFacturacion.Where(x => x.FacturacionId == facturacionId && x.Borrado != true).Include(x => x.Facturacion)
                        .ToListAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
