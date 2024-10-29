using DataLayer.PDbContex;
using DataLayer.Models.Cuentas;
using BussinessLayer.Interfaces.ICuentas;
using Microsoft.EntityFrameworkCore;

namespace BussinessLayer.Services.SCuentas
{
    public class DetalleCuentaPorPagarService : IDetalleCuentaPorPagar
    {
        private readonly PDbContext _context;

        public DetalleCuentaPorPagarService(PDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task Add(DetalleCuentaPorPagar entity)
        {
            try
            {
                CuentasPorPagar cta =  await _context.CuentasPorPagar.Where(x=> x.IdMovimientoAlmacen==entity.IdMovAlmacen && x.IdEmpresa==entity.IdEmpresa).FirstAsync();
                if(cta.Restante == entity.Monto)
                {
                    cta.IsPaid = true;
                    cta.FechaUltimoPago = DateTime.Now;
                    cta.Restante -= entity.Monto;

                    _context.Entry(cta).State = EntityState.Modified;
                    
                    _context.DetalleCuentaPorPagar.Add(entity);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    cta.Restante -= entity.Monto;
                    cta.FechaUltimoPago = DateTime.Now;
                    _context.Entry(cta).State = EntityState.Modified;
                    _context.DetalleCuentaPorPagar.Add(entity);
                    await _context.SaveChangesAsync();
                }              
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        
        public Task Delete(int id, long idEMpresa)
        {
            throw new NotImplementedException();
        }

        public Task Edit(DetalleCuentaPorPagar entity)
        {
            throw new NotImplementedException();
        }

        public async Task<IList<DetalleCuentaPorPagar>> GetAll(long idEMpresa)
        {
            try
            {
                return await _context.DetalleCuentaPorPagar.ToListAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<IEnumerable<DetalleCuentaPorPagar>> GetAllByIdCtaPorPagar(int idcta)
        {
            return await _context.DetalleCuentaPorPagar.Where(x => x.IdMovAlmacen == idcta && x.Borrado != true).ToListAsync();
        }

        public Task<DetalleCuentaPorPagar> GetById(int id, long idEMpresa)
        {
            throw new NotImplementedException();
        }
    }
}
