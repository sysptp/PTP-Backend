using DataLayer.Models.Facturas;
using DataLayer.PDbContex;
using Microsoft.EntityFrameworkCore;

namespace BussinessLayer.Repository.ModuloFacturacion
{
    public class InvoiceRepository(PDbContext dbContext) : IInvoiceRepository
    {
        private readonly PDbContext _dbContext = dbContext;
        public async Task AddAsync(Facturacion invoice)
        {
            try
            {
                await _dbContext.Set<Facturacion>().AddAsync(invoice);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message, ex);
            }
        }

        public async Task<List<Facturacion>> GetAllAsync(int bussinesId)
        {
            try
            {
                return await _dbContext.Set<Facturacion>().Where(x => !x.Borrado && x.IdEmpresa == bussinesId).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message, ex);
            }
        }
        public async Task<Facturacion> GetByIdAsync(string invoiceNumber)
        {
            try
            {
                return await _dbContext.Set<Facturacion>()
                    .FirstOrDefaultAsync(x => !x.Borrado && x.NoFactura == invoiceNumber);
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message, ex);
            }
        }

        public Task UpdateAync(Facturacion invoice)
        {
            throw new NotImplementedException();
        }
    }
}
