using BussinessLayer.Interfaces.ModuloCampaña.Repository;
using DataLayer.Models.ModuloCampaña;
using DataLayer.PDbContex;
using Microsoft.EntityFrameworkCore;

namespace BussinessLayer.Repository.RCampaña
{
    public class CmpCampanaDetalleRepository(PDbContext context) : ICmpCampanaDetalleRepository
    {
        public async Task Add(CmpCampanaDetalle campanaDetalle)
        {
            try
            {
                await context.CmpCampanaDetalles.AddAsync(campanaDetalle);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message, ex);
            }
        }

        public async Task Delete(int id)
        {
            try
            {
                CmpCampanaDetalle? cmpCampanaDetalle = await context.CmpCampanaDetalles.FindAsync(id);
                if (cmpCampanaDetalle != null)
                {
                    cmpCampanaDetalle.Borrado = true;
                    cmpCampanaDetalle.FechaModificacion = DateTime.Now;
                    context.Update(cmpCampanaDetalle);
                    await context.SaveChangesAsync();
                }

            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message, ex);
            }
        }

        public async Task<List<CmpCampanaDetalle>> GetAll(int empresaId)
        {
            try
            {
                return await context.CmpCampanaDetalles.Include(x => x.Campana).Include(x => x.Cliente)
               .Where(x => x.EmpresaId == empresaId && !x.Borrado).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message, ex);
            }
        }

        public async Task<CmpCampanaDetalle> GetById(int id)
        {
            try
            {
                return await context.CmpCampanaDetalles.Include(x => x.Campana).Include(x => x.Cliente)
               .FirstOrDefaultAsync(x => x.CampanaDetalleId == id && !x.Borrado);
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message, ex);
            }
        }

        public async Task Update(CmpCampanaDetalle campanaDetalle)
        {
            try
            {
                context.CmpCampanaDetalles.Update(campanaDetalle);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message, ex);
            }
        }
    }
}
