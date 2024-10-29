using BussinessLayer.Interface.IAlmacenes;
using DataLayer.Models.Almacen;
using DataLayer.PDbContex;
using Microsoft.EntityFrameworkCore;

namespace BussinessLayer.Services.SALmacenes
{
    public class AlmacenesService : IAlmacenesService
    {
        private readonly PDbContext _context;

        public AlmacenesService(PDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task Add(Almacenes entity)
        {
            try
            {
                _context.Almacenes.Add(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                throw;
            } 
        }

        public async Task<Almacenes> GetById(int id, long idEmpresa)
        {

            return await _context.Almacenes.Where(x=>x.Id==id && x.IDEmpresa==idEmpresa).FirstOrDefaultAsync();
        }

        public async Task<IList<Almacenes>> GetPrincipal(long idEmpresa)
        {

            return await _context.Almacenes.Where(x => x.Borrado != true && x.AlmacenPrincipal == "S" && x.IDEmpresa == idEmpresa).ToListAsync();
        }

        public async Task<IList<Almacenes>> GetAll(long idEmpresa)
        {
            
            try
            {
                
                return await _context.Almacenes.Where(x => x.Borrado != true && x.IDEmpresa == idEmpresa).ToListAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            } 
        }

        public async Task Delete(int id,long idEmpresa)
        {

            Almacenes alm = await _context.Almacenes.Where(x=>x.Id==id && x.IDEmpresa==idEmpresa).FirstOrDefaultAsync();
            if (alm != null)
            {
                alm.Borrado = true;
                await _context.SaveChangesAsync();
            }
        }

        public async Task Edit(Almacenes entity)
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
}
