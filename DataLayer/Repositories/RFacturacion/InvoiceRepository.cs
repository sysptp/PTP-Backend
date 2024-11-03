using DataLayer.Data;
using DataLayer.Models.Facturas;
using DataLayer.PDbContex;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BussinessLayer.Repository.RFacturacion
{
    public class InvoiceRepository(IConfiguration configuration, PDbContext dbContext) : IInvoiceRepository
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly PDbContext _dbContext = dbContext;
        public async Task AddAsync(Facturacion invoice)
        {
            try
            {
                await _dbContext.AddAsync(invoice);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message, ex);
            }
        }

        public Task<List<Facturacion>> GetAllAsync(int bussinesId)
        {
            throw new NotImplementedException();
        }

        public Task<Facturacion> GetByIdAsync(string invoiceNumber)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAync(Facturacion invoice)
        {
            throw new NotImplementedException();
        }
    }
}
