using DataLayer.Models.Clients;
using DataLayer.PDbContex;
using Microsoft.EntityFrameworkCore;

namespace BussinessLayer.Repository.RClient
{
    public class ClientRepository(PDbContext context) : IClientRepository
    {
        private readonly PDbContext _context = context;
        public async Task<Client> AddAsync(Client client)
        {
            try
            {
                client.DateAdded = DateTime.Now;
                client.DateModified = new DateTime(1793, 1, 1);
                await _context.Clients.AddAsync(client);
                await _context.SaveChangesAsync();
                return client;
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message, ex);
            }
        }

        public async Task<List<Client>> GetAllAsync(int bussinesId,int pageSize,int pageCount)
        {
            try
            {
                return await _context.Clients
                    .Where(x=> x.IsDeleted != 1 && string.Equals(x.CodeBussines,bussinesId))
                    .OrderBy(c=> c.Id)
                    .Skip((pageSize - 1) * pageCount)
                    .Take(pageCount)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message, ex);
            }
        }
    }
}
