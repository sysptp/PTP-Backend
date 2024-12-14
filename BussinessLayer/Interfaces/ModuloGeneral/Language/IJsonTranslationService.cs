namespace BussinessLayer.Interfaces.ModuloGeneral.Language
{
    public interface IJsonTranslationService
    {
        Task<IEnumerable<object>> TranslateEntities<T>(IEnumerable<T> entities) where T : class;
    }
}