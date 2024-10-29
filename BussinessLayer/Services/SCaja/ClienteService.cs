using BussinessLayer.Interfaces.ICaja;
using DataLayer.Models;
using DataLayer.PDbContex;
using Microsoft.EntityFrameworkCore;

namespace BussinessLayer.Services.SCaja
{
    public class ClienteService : IClientesService
    {
        private readonly PDbContext _context;

        public ClienteService(PDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task Add(Cliente entity)
        {
            try
            {
                _context.Clientes.Add(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;     
            }             
        }

        public async Task<Cliente> GetById(int id,long idEmpresa)
        {
            return await _context.Clientes.Where(x=>x.IdEmpresa== idEmpresa && x.Id==id).SingleAsync();
            
        }

        public async  Task<IList<Cliente>> GetAll(long idEmpresa)
        {     
            try
            {
                return await _context.Clientes.Where(x => x.Borrado != true && x.IdEmpresa== idEmpresa).ToListAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task Delete(int id, long empresa)
        {
            var cl = await _context.Clientes.FindAsync(id);
            if (cl == null) return;

            cl.Borrado = true;

            await _context.SaveChangesAsync();    
        }

        public async Task Edit(Cliente entity)
        {
            try
            {
                //var oldCliente = await context.Clientes.FindAsync(entity.Id);
                //if (oldCliente == null) return;

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
