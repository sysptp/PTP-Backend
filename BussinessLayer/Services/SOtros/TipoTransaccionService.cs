using Microsoft.EntityFrameworkCore;
using BussinessLayer.Interface.IOtros;
using DataLayer.Models.Otros;
using DataLayer.PDbContex;
using DataLayer.Enums;

namespace BussinessLayer.Services.SOtros
{
    public class TipoTransaccionService : ITipoTransaccionService
    {
        private readonly PDbContext _context;

        public TipoTransaccionService(PDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task Add(TipoTransaccion entity)
        {
            try
            {
                _context.TipoTransaccion.Add(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task Edit(TipoTransaccion entity)
        {
            if (entity != null)
            {
                _context.Entry(entity).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<TipoTransaccion> GetById(int id, long idEMpresa)
        {
            return await _context.TipoTransaccion.FindAsync(id);
        }

        public Task<IList<TipoTransaccion>> GetAll(long idEmpresa)
        {
            List<TipoTransaccion> lista_tipo = new List<TipoTransaccion>
            {
                new TipoTransaccion() { Nombre = TipoTransaccionEnum.Efectivo.ToString(), Id = 1 },
                new TipoTransaccion() { Nombre = TipoTransaccionEnum.Tarjeta.ToString(), Id = 3 },
                new TipoTransaccion() { Nombre = TipoTransaccionEnum.Cheques.ToString(), Id = 2 },
                new TipoTransaccion() { Nombre = TipoTransaccionEnum.Cupones.ToString(), Id = 4 },
                new TipoTransaccion() { Nombre = TipoTransaccionEnum.TarjetayEfectivo.ToString(), Id = 5 }
            };

            return Task.FromResult((IList<TipoTransaccion>)lista_tipo);
        }


        public IList<TipoTransaccion> GetAllE()
        { 
                    /*
                        Efectivo =1,
                        Cheques =2,
                        Tarjeta =3,
                        Cupones =4,
                        TarjetayEfectivo = 5
                     */
        
            List<TipoTransaccion> lista_tipo = new List<TipoTransaccion>
            {
                new TipoTransaccion() { Nombre = TipoTransaccionEnum.Efectivo.ToString(), Id = 1 },
                new TipoTransaccion() { Nombre = TipoTransaccionEnum.Tarjeta.ToString(), Id = 3 },
                new TipoTransaccion() { Nombre = TipoTransaccionEnum.Cheques.ToString(), Id = 2 },
                new TipoTransaccion() { Nombre = TipoTransaccionEnum.Cupones.ToString(), Id = 4 },
                new TipoTransaccion() { Nombre = TipoTransaccionEnum.TarjetayEfectivo.ToString(), Id = 5 }
            };

            return  lista_tipo;
            // _context.TipoTransaccion.Where(x => x.Borrado != true).ToListAsync();         
        }

        public async Task Delete(int id, long idEMpresa)
        {
            var tt= await _context.TipoTransaccion.FindAsync(id);
            if (tt != null)
            {
                tt.Borrado = true;
                await   _context.SaveChangesAsync();
            }
        }
    }
}
