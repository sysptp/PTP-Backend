using DataLayer.Models.Boveda;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BussinessLayer.Interfaces.IBoveda
{
    public interface IBovedaCajaDesglosesService
    {
        Task Add(BovedaCajaDesglose model);

        Task Delete(BovedaCajaDesglose model);

        Task<List<BovedaCajaDesglose>> GetAll();

        Task<BovedaCajaDesglose> GetById(int id);

        Task Update(BovedaCajaDesglose model);
    }
}
