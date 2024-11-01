using DataLayer.Data;
using DataLayer.Models.Facturas;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace BussinessLayer.Repository.RFacturacion
{
    public class InvoiceRepository(IConfiguration configuration) : IInvoiceRepository
    {
        private readonly IConfiguration _configuration = configuration;
        public Task AddAsync(Facturacion invoice)
        {
            try
            {
                using SqlConnection context = new(_configuration.GetConnectionString("POS_CONN"));
                const string query = @"INSERT INTO";
                return null;
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
