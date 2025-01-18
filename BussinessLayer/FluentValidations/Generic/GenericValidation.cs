using DataLayer.PDbContex;

namespace BussinessLayer.FluentValidations.Generic
{
    public class GenericValidation(PDbContext pDbContext) : IGenericValidation
    {
        public bool ValidarTipoPlantilla(int id)
        {
            return pDbContext.CmpTipoPlantillas.Any(x =>x.Id == id);
        }

        public bool ExistingBussines(long empresaId)
        {
            return pDbContext.GnEmpresa.Any(x => x.CODIGO_EMP == empresaId);
        }
        public bool ExistingServer(int servidorId)
        {
            return pDbContext.CmpServidoresSmtps.Any(X => X.ServidorId == servidorId);
        }

        public bool ExistePlantilla(int id)
        {
            return pDbContext.CmpPlantillas.Any(x => x.Id == id);
        }
    }
}
