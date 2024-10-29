using DataLayer.Models.Caja;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BussinessLayer.Interfaces.ICaja
{
    public interface IBilletes_MonedaService
    {
        Task Add(Billetes_Moneda model);

        Task Delete(Billetes_Moneda model);

        Task<List<Billetes_Moneda>> GetAll();

        Task<Billetes_Moneda> GetById(int id);

        Task Update(Billetes_Moneda model);
    }
}
