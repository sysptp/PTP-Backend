using DataLayer.Models.ModuloVentas.Boveda;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BussinessLayer.Interfaces.Services.ModuloVentas.IBoveda
{
    public interface IBovedaCajasService
    {
        Task Add(BovedaCaja model);

        Task Delete(BovedaCaja model);

        Task<List<BovedaCaja>> GetAll();

        Task<BovedaCaja> GetById(int id);

        Task Update(BovedaCaja model);
    }
}
