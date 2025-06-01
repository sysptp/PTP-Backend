using BussinessLayer.Interfaces.Repositories;
using DataLayer.Models.Modulo_Citas;

namespace BussinessLayer.Interface.Repository.Modulo_Citas
{
    public interface ICtaConfiguracionRepository : IGenericRepository<CtaConfiguration>
    {
        CtaConfiguration? GetByCompanyId(long companyId);
    }
}
