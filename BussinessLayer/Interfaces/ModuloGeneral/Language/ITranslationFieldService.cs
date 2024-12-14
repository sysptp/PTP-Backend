namespace BussinessLayer.Interfaces.ModuloGeneral.Language
{
    public interface ITranslationFieldService
    {
        Task<Dictionary<string, string>> GetTranslatedFields(string tableName);
    }
}