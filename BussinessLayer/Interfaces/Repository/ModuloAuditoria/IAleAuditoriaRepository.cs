using BussinessLayer.Interfaces.Repositories;
using DataLayer.Models.ModuloAuditoria;


namespace BussinessLayer.Interfaces.Repository.ModuloAuditoria
{
    public interface IAleAuditoriaRepository : IGenericRepository<AleAuditoria>
    {
        Task<AleAuditoria> AddAuditoria(AleAuditoria entity);
    }
}
