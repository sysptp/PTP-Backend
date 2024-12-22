using Microsoft.AspNetCore.Http;

namespace BussinessLayer.Interfaces.Services.ICargaMasiva
{
    public interface ICargaMasivaService
    {
        string GenerateEmptyCsv(List<(string Name, bool IsIdentity, bool IsNullable)> columnDetails, string delimitador);
        Task<List<(string Name, bool IsIdentity, bool IsNullable)>> GetColumnDetailsAsync(string tableName);
        Task<List<string>> GetTableNamesAsync();
        Task<(bool Success, string ErrorMessage)> ProcessCsvFileAsync(string tableName, IFormFile file, string delimitador);
    }
}