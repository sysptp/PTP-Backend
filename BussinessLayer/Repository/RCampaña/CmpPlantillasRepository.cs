using BussinessLayer.Interfaces.ModuloCampaña.Repository;
using DataLayer.Models.ModuloCampaña;
using DataLayer.PDbContex;
using Microsoft.EntityFrameworkCore;
using static Dapper.SqlMapper;

namespace BussinessLayer.Repository.RCampaña
{
    public class CmpPlantillasRepository(PDbContext dbContext) : ICmpPlantillaRepository
    {

        public async Task AddAsync(CmpPlantillas plantilla)
        {
            try
            {
                await dbContext.CmpPlantillas.AddAsync(plantilla);
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
                CmpPlantillas? entity = await dbContext.CmpPlantillas.FindAsync(id);
                entity.Borrado = true;
                dbContext.CmpPlantillas.Update(entity);
                await dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message, ex);
            }
        }

        public async Task<List<CmpPlantillas>> GetAllAsync(long empresaId)
        {
            try
            {
                return await dbContext.CmpPlantillas
                    .Where(x => x.EmpresaId == empresaId && !x.Borrado)
                    .Include(x => x.CmpTipoPlantilla)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message, ex);
            }
        }

        public async Task<CmpPlantillas> GetByIdAsync(int id, long empresaId)
        {
            try
            {
                return await dbContext.CmpPlantillas
                    .Include(x => x.CmpTipoPlantilla)
                    .Where(x => x.EmpresaId == empresaId && !x.Borrado && x.Id == id)
                    .FirstOrDefaultAsync();

            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message, ex);
            }
        }

        public async Task UpdateAsync(CmpPlantillas plantilla)
        {
            try
            {
                CmpPlantillas entry = await dbContext.CmpPlantillas.FindAsync(plantilla.Id);
                plantilla.UsuarioAdicion = entry.UsuarioAdicion;
                plantilla.FechaAdicion = entry.FechaAdicion;
                dbContext.Entry(entry).CurrentValues.SetValues(plantilla);
                await dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message, ex);
            }
        }
    }
}
