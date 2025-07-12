using BussinessLayer.Interfaces.Repositories;
using DataLayer.Models.Modulo_Citas;

namespace BussinessLayer.Interface.Repository.Modulo_Citas
{
    public interface ICtaStateRepository : IGenericRepository<CtaState>
    {
        Task<CtaState?> GetDefaultStateByCompanyAndAreaAsync(long companyId, int areaId);
        Task<CtaState?> GetClosureStateByCompanyAndAreaAsync(long companyId, int areaId);
    }
}
