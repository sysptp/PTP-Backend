using BussinessLayer.Interfaces.ModuloCampaña.Repository;
using DataLayer.Models.ModuloCampaña;
using DataLayer.PDbContex;
using Microsoft.EntityFrameworkCore;

namespace BussinessLayer.Repository.RCampaña
{
    public class CmpFrecuenciaRepository(PDbContext context) : ICmpFrecuenciaRepository
    {
        public async Task<List<CmpFrecuencia>> GetAll()
        {
            try
            {
                return await context.CmpFrecuencias.Where(x=> !x.Borrado).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message, ex);
            }
        }

        public async Task<CmpFrecuencia> GetById(int id)
        {

            try
            {
                return await context.CmpFrecuencias.FirstOrDefaultAsync(x=> !x.Borrado && x.Id == id);
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message, ex);
            }
        }
    }
}
