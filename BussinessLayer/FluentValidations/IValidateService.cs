namespace BussinessLayer.FluentValidations
{
    public interface IValidateService<T> where T : class
    {
        List<string> Validate(T entity);
    }
}
