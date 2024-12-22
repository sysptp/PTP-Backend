namespace BussinessLayer.Interfaces.Services.ModuloGeneral.Language
{
    public interface ITranslationFieldService
    {
        Task<Dictionary<string, string>> GetTranslatedFields(string tableName);
    }
}