using BussinessLayer.Interfaces.ModuloCampaña.Repository;
using DataLayer.Models.ModuloCampaña;
using DataLayer.PDbContex;
using Microsoft.EntityFrameworkCore;

namespace BussinessLayer.Repository.RCampaña
{
    public class CmpTipoPlantillaRepository(PDbContext dbContext) : ICmpTipoPlantillaRepository
    {
        public async Task AddAsync(CmpTipoPlantilla plantilla)
        {
            try
            {
                await dbContext.CmpTipoPlantillas.AddAsync(plantilla);
                await dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message, ex);
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                CmpTipoPlantilla? entity = await dbContext.CmpTipoPlantillas.FindAsync(id);
                entity.Borrado = true;
                dbContext.CmpTipoPlantillas.Update(entity);
                await dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message, ex);
            }
        }

        public async Task<List<CmpTipoPlantilla>> GetAllAsync(int empresaId)
        {
            try
            {
                return await dbContext.CmpTipoPlantillas
                    .Where(x => x.EmpresaId == empresaId && !x.Borrado)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message, ex);
            }
        }

        public async Task<CmpTipoPlantilla> GetByIdAsync(int id, int empresaId)
        {
            try
            {
                return await dbContext.CmpTipoPlantillas
                    .FirstOrDefaultAsync(x => x.EmpresaId == empresaId && !x.Borrado && x.Id == id);

            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message, ex);
            }
        }

        public async Task UpdateAsync(CmpTipoPlantilla plantilla)
        {
            try
            {
                dbContext.CmpTipoPlantillas.Update(plantilla);
                await dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message, ex);
            }
        }
    }
}
