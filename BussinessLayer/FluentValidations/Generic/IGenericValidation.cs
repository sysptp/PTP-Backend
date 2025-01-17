namespace BussinessLayer.FluentValidations.Generic
{
    public interface IGenericValidation
    {
        bool ExistingBussines(long empresaId);
        bool ExistingServer(int servidorId);
    }
}
