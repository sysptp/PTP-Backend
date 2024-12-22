using BussinessLayer.Interfaces.Services.ICuentas;
using DataLayer.Models.Cuentas;
using DataLayer.PDbContex;
using Microsoft.EntityFrameworkCore;

namespace BussinessLayer.Services.SCuentas
{
    public class CuentasPorPagarService : ICuentaPorPagarService
    {
        private readonly PDbContext _context;

        public CuentasPorPagarService(PDbContext dbContext)
        {
            _context = dbContext;
        }

        public async  Task Add(CuentasPorPagar entity)
        {
            try
            {
                _context.CuentasPorPagar.Add(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task AddWithInicial(CuentasPorPagar entity)
        {
            try
            {
                if (entity.MontoInicial > 0)
                {
                    CuentasPorPagar cpp = new CuentasPorPagar
                    {

                        Descripcion = entity.Descripcion,
                        MontoInicial = entity.MontoInicial,
                        MontoDeuda = entity.MontoDeuda,
                        FechaUltimoPago = entity.FechaUltimoPago,
                        FechaLimitePago = entity.FechaLimitePago,
                        IsPaid = entity.IsPaid,
                        IdEmpresa= entity.IdEmpresa,
                        IdUsuario = entity.IdUsuario,
                        IdMovimientoAlmacen = entity.IdMovimientoAlmacen,
                        Restante = entity.MontoDeuda - entity.MontoInicial,
                        DetalleCuentasPorPagar = new List<DetalleCuentaPorPagar>

                        {
                            new DetalleCuentaPorPagar
                            {
                                Monto = entity.MontoInicial,
                                FechaPago = DateTime.Now,
                                IdUsuario = entity.IdUsuario,
                                IsCanceled = false,
                                IdEmpresa=entity.IdEmpresa,
                                IdMovAlmacen=entity.IdMovimientoAlmacen
                            }
                        }
                    };

                    _context.CuentasPorPagar.Add(cpp);

                    foreach (var a in cpp.DetalleCuentasPorPagar)
                    {
                        _context.DetalleCuentaPorPagar.Add(a);
                    }

                    await _context.SaveChangesAsync();
                }
                else
                {
                    _context.CuentasPorPagar.Add(entity);
                    await _context.SaveChangesAsync();
                }
             
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public Task Create(CuentasPorPagar dcp)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id, long idEMpresa)
        {
            throw new NotImplementedException();
        }

        public async Task Edit(CuentasPorPagar entity)
        {
            try
            {
                _context.Entry(entity).State = EntityState.Modified;
               await _context.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IList<CuentasPorPagar>> GetAll(long idEMpresa)
        {
            return await _context.CuentasPorPagar.Where(x => x.Borrado != true && x.IdEmpresa==idEMpresa).Include(x => x.MovimientoAlmacen).ToListAsync();
        }

        public async Task<CuentasPorPagar> GetById(int id, long idEMpresa)
        {
            //.Include(x=> x.MovimientoAlmacen.Suplidor)
            return await _context.CuentasPorPagar.Include(x=> x.MovimientoAlmacen).SingleOrDefaultAsync(x=> x.Id == id);
        }
    }
}
