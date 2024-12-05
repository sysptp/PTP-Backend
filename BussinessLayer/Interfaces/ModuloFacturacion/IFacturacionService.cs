using BussinessLayer.ViewModels;
using DataLayer.Models.Facturas;

public interface IFacturacionService 
{
    Task Create(FacturacionViewModel vm);

    Task<IList<Facturacion>> GetAll();

    Task Delete(Facturacion model);

    Task<Facturacion> GetById(int id);

    Task Update(Facturacion model);

}

