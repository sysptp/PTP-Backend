using BussinessLayer.Interfaces.Repositories;
using DataLayer.Models.ModuloAuditoria;


namespace BussinessLayer.Interfaces.Repository.Auditoria
{
    public interface IAleAuditoriaRepository:IGenericRepository<AleAuditoria>
    {
        Task<AleAuditoria> AddAuditoria(AleAuditoria entity);
    }
}
