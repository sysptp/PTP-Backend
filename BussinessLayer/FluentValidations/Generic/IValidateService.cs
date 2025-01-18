namespace BussinessLayer.FluentValidations.Generic
{
    public interface IValidateService<T> where T : class
    {
        List<string> Validate(T entity);
    }
}
