using BussinessLayer.Interfaces.ModuloCampaña.Repository;
using DataLayer.Models.ModuloCampaña;
using DataLayer.PDbContex;
using Microsoft.EntityFrameworkCore;

namespace BussinessLayer.Repository.RCampaña
{
    public class CmpLogsEnvioRepository(PDbContext context) : ICmpLogsEnvioRepository
    {
        public async Task AddAsync(CmpLogsEnvio cmpLogsEnvio)
        {
            try
            {
                await context.CmpLogsEnvios.AddAsync(cmpLogsEnvio);
                await context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message, ex);
            }
        }
        public async Task<List<CmpLogsEnvio>> GetAllAsync(long empresaId)
        {
            try
            {
                return await context.CmpLogsEnvios
                    .Where(x => x.EmpresaId == empresaId && !x.Borrado)
                    .ToListAsync();

            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message, ex);
            }
        }
        public async Task<CmpLogsEnvio> GetByIdAsync(int id)
        {
            return await context.CmpLogsEnvios
                .Where(x => !x.Borrado)
                .FirstOrDefaultAsync(x => x.LogId == id);
        }
    }
}
