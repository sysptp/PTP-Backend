using BussinessLayer.Interfaces.ModuloCampaña.Repository;
using DataLayer.Models.ModuloCampaña;
using DataLayer.PDbContex;
using Microsoft.EntityFrameworkCore;

namespace BussinessLayer.Repository.RCampaña
{
    public class CmpConfiguracionesSmtpRepository : ICmpConfiguracionesSmtpRepository
    {
        private readonly PDbContext _dbContext;

        public CmpConfiguracionesSmtpRepository(PDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(CmpConfiguracionesSmtp configuracionesSmtp)
        {
            try
            {
                configuracionesSmtp.FechaAdicion = DateTime.Now;
                configuracionesSmtp.FechaModificacion = DateTime.Now;
                await _dbContext.CmpConfiguracionesSmtps.AddAsync(configuracionesSmtp);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message, ex);
            }
        }

        public async Task DeleteAsync(int id, int empresaId, string usuarioModificacion)
        {
            try
            {
                CmpConfiguracionesSmtp cmpConfiguraciones = await GetyByIdAsync(id, empresaId);
                if (cmpConfiguraciones != null)
                {
                    cmpConfiguraciones.FechaModificacion = DateTime.Now;
                    cmpConfiguraciones.Borrado = true;
                    cmpConfiguraciones.UsuarioModificacion = usuarioModificacion;

                    _dbContext.Update(cmpConfiguraciones);
                    await _dbContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message, ex);
            }
        }

        public async Task<List<CmpConfiguracionesSmtp>> GetAllAsync(int empresaId)
        {
            try
            {
                List<CmpConfiguracionesSmtp> cmpConfiguraciones = await _dbContext
                    .CmpConfiguracionesSmtps.Include(x => x.ServidoresSmtp)
                    .Where(x => x.EmpresaId == empresaId && !x.Borrado)
                    .ToListAsync();

                return cmpConfiguraciones;
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message, ex);
            }
        }

        public async Task<CmpConfiguracionesSmtp> GetyByIdAsync(int id, int empresaId)
        {
            try
            {
                CmpConfiguracionesSmtp? cmpConfiguraciones = await _dbContext
                .CmpConfiguracionesSmtps.Include(x => x.ServidoresSmtp)
                .FirstOrDefaultAsync(x => x.EmpresaId == empresaId
                && !x.Borrado && x.ConfiguracionId == id);

                return cmpConfiguraciones;
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message, ex);
            }
        }

        public async Task UpdateAsync(CmpConfiguracionesSmtp configuracionesSmtp)
        {
            try
            {
                _dbContext.CmpConfiguracionesSmtps.Update(configuracionesSmtp);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message, ex);
            }
        }
    }
}
