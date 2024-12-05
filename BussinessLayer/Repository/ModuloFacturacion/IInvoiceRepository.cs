using DataLayer.Models.Facturas;

namespace BussinessLayer.Repository.ModuloFacturacion
{
    public interface IInvoiceRepository
    {
        Task AddAsync(Facturacion invoice);
        Task<List<Facturacion>> GetAllAsync(int bussinesId);
        Task<Facturacion> GetByIdAsync(string invoiceNumber);
        Task UpdateAync(Facturacion invoice);


    }
}
