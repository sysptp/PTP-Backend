using BussinessLayer.Atributes;
using BussinessLayer.Interfaces.ModuloGeneral.Language;

namespace BussinessLayer.Services.ModuloGeneral.Language.Translation
{
    public class JsonTranslationService : IJsonTranslationService
    {
        private readonly ITranslationFieldService _fieldService;

        public JsonTranslationService(ITranslationFieldService fieldService)
        {
            _fieldService = fieldService;
        }
        public async Task<IEnumerable<object>> TranslateEntities<T>(IEnumerable<T> entities) where T : class
        {
            var tableName = GetTableName<T>();
            var translatedFields = await _fieldService.GetTranslatedFields(tableName);

            return entities.Select(entity => TranslateEntity(entity, translatedFields));
        }

        private object TranslateEntity<T>(T entity, Dictionary<string, string> translatedFields) where T : class
        {
            var result = new Dictionary<string, object>();
            foreach (var property in typeof(T).GetProperties())
            {
                var value = property.GetValue(entity);
                var translatedName = translatedFields.ContainsKey(property.Name)
                    ? translatedFields[property.Name]
                    : property.Name;

                result[translatedName] = value;
            }

            return result;
        }

        public static string GetTableName<T>()
        {
            var attribute = typeof(T).GetCustomAttributes(typeof(TableNameAttribute), true)
                                     .FirstOrDefault() as TableNameAttribute;

            if (attribute == null)
                throw new InvalidOperationException($"The DTO {typeof(T).Name} does not have a TableName attribute.");

            return attribute.TableName;
        }

    }

}
