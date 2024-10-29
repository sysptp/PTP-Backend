using System.Collections.Generic;
using System.Threading.Tasks;
using BussinessLayer.ViewModels;
using DataLayer.Models.Facturas;

namespace BussinessLayer.Interface.IFacturacion
{
    public interface IFacturacionService 
    {
        Task Create(FacturacionViewModel vm);

        Task<IList<Facturacion>> GetAll();

        Task Delete(Facturacion model);

        Task<Facturacion> GetById(int id);

        Task Update(Facturacion model);

    }
}
