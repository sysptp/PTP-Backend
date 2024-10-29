using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using BussinessLayer.Interface.ISuplidores;
using DataLayer.Models.Suplidor;
using DataLayer.PDbContex;

namespace BussinessLayer.Services.SSuplidores
{
    public class ContactosSuplidoresService : IContactosSuplidoresService
    {
        private readonly PDbContext _context;

        public ContactosSuplidoresService(PDbContext dbContext)
        {
            _context = dbContext;
        }

        public async  Task Add(ContactosSuplidores entity)
        {
            try
            {
                _context.ContactosSuplidores.Add(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task Edit(ContactosSuplidores entity)
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

        public async Task<ContactosSuplidores> GetById(int id, long idempresa)
        {
            return await _context.ContactosSuplidores.FindAsync(id);
        }

        public async  Task<IList<ContactosSuplidores>> GetAll(long idempresa)
        {
            return await _context.ContactosSuplidores.Where(x => x.Borrado != true).ToListAsync();
        }

        public async Task Delete(int id, long idempresa)
        {
            try
            {
                var c = await _context.ContactosSuplidores.FindAsync(id);
                if (c != null)
                {
                    c.Borrado = true;
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
