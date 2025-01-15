
using DataLayer.Models.ModuloCampaña;

public interface ICmpServidoresSmtpRepository
{
    Task AddAsync(CmpServidoresSmtp servidor);
    Task DeleteAsync(long id);
    Task<List<CmpServidoresSmtp>> GetAllAsync();
    Task<CmpServidoresSmtp?> GetByIdAsync(long id);
    Task UpdateAsync(CmpServidoresSmtp servidor);
}