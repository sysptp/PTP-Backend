namespace BussinessLayer.FluentValidations.Generic
{
    public interface IGenericValidation
    {
        bool ExistingBussines(long empresaId);
        bool ValidarTipoPlantilla(int id);

        bool ExistePlantilla(int id);
        bool ExistingServer(int servidorId);
    }
}
