using BussinessLayer.Interfaces.ModuloCampaña.Repository;
using DataLayer.Models.ModuloCampaña;
using DataLayer.PDbContex;
using Microsoft.EntityFrameworkCore;

namespace BussinessLayer.Repository.RCampaña
{
    public class CmpCampanaRepository(PDbContext context) : ICmpCampanaRepository
    {
        public async Task CreateAsync(CmpCampana campana)
        {
            try
            {
                await context.CmpCampanas.AddAsync(campana);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                CmpCampana? cmpCampana = await context.FindAsync<CmpCampana>(id);
                cmpCampana.Borrado = true;
                context.Update(cmpCampana);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task<List<CmpCampana>> GetAllAsync(int empresaId)
        {
            try
            {
                return await context.CmpCampanas.Include(x => x.Plantilla)
                    .Include(x => x.Estado).Where(x => !x.Borrado && x.EmpresaId == empresaId).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task<CmpCampana> GetByIdAsync(int id, int empresaId)
        {
            try
            {
                return await context.CmpCampanas.Include(x => x.Plantilla)
                    .Include(x => x.Estado).FirstOrDefaultAsync(x => !x.Borrado && x.EmpresaId == empresaId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task UpdateAsync(CmpCampana campana)
        {
            try
            {
                CmpCampana entry = await context.CmpCampanas.FindAsync(campana.CampanaId);
                campana.UsuarioAdicion = entry.UsuarioAdicion;
                campana.FechaAdicion = entry.FechaAdicion;
                context.Entry(entry).CurrentValues.SetValues(campana);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task UpdateStatus(int id, int status)
        {
            try
            {
                CmpCampana entry = await context.CmpCampanas.FindAsync(id);
                entry.EstadoId = status;
                context.Update(entry);
                await context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}
