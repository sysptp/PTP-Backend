using BussinessLayer.Interfaces.ModuloCampaña.Repository;
using DataLayer.Models.ModuloCampaña;
using DataLayer.PDbContex;
using Microsoft.EntityFrameworkCore;

namespace BussinessLayer.Repository.RCampaña
{
    public class CmpEstadoRepository(PDbContext dbContext) : ICmpEstadoRepository
    {

        public async Task<List<CmpEstado>> GetAllAsync()
        {
            try
            {
                return await dbContext.CmpEstados.Where(x => !x.Borrado).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message, ex);
            }
        }

        public async Task<CmpEstado> GetByIdAsync(int id)
        {
            try
            {
                return await dbContext.CmpEstados.FirstOrDefaultAsync(x => x.EstadoId == id && !x.Borrado);
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message, ex);
            }
        }
    }
}
