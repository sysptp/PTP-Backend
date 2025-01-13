using BussinessLayer.FluentValidations.ModuloCampaña.CmpSmtpConfiguraciones;

namespace BussinessLayer.Interfaces.ModuloCampaña
{
    public interface ICmpServidoresSmtpRepository
    {
        Task<CmpServidoresSmtp> GetByIdAsync(int id);
        Task<List<CmpServidoresSmtp>> GetAllAsync();
        Task CreateAsync(CmpServidoresSmtp servidor);
        Task UpdateAsync(CmpServidoresSmtp servidor);
        Task DeleteAsync(int id);
    }
}
