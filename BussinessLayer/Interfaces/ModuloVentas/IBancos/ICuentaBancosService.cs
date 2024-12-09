using DataLayer.Models.ModuloVentas.Bancos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BussinessLayer.Interfaces.ModuloVentas.IBancos
{
    public interface ICuentaBancosService
    {
        Task Add(CuentaBancos model);

        Task Delete(CuentaBancos model);

        Task<List<CuentaBancos>> GetAll();

        Task<List<CuentaBancos>> GetAllIndex();

        Task<CuentaBancos> GetById(int id);

        Task Update(CuentaBancos model);
    }
}
