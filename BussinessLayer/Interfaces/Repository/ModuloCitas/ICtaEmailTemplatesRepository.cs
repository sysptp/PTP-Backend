
using BussinessLayer.Interfaces.Repositories;
using DataLayer.Models.ModuloCitas;

namespace BussinessLayer.Interfaces.Repository.ModuloCitas
{
    public interface ICtaEmailTemplatesRepository : IGenericRepository<CtaEmailTemplates>
    {
        Task<CtaEmailTemplates?> GetEmailTemplateByFilters(long? companyId, int? templateTypeId, bool appliesToParticipant = false, bool appliesToAssignee = false);
    }
}
