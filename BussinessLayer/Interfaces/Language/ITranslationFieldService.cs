namespace BussinessLayer.Interfaces.Language
{
    public interface ITranslationFieldService
    {
        Task<Dictionary<string, string>> GetTranslatedFields(string tableName);
    }
}