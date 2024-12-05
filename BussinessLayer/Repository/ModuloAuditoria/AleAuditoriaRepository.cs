using BussinessLayer.Interfaces.Repository.Auditoria;
using BussinessLayer.Repository.ROtros;
using DataLayer.Models.ModuloAuditoria;
using DataLayer.PDbContex;

namespace BussinessLayer.Repository.ModuloAuditoria
{
    public class AleAuditoriaRepository : GenericRepository<AleAuditoria>, IAleAuditoriaRepository
    {
        private readonly PDbContext _dbContext;
        private readonly ITokenService _tokenService;
        public AleAuditoriaRepository(PDbContext dbContext, ITokenService tokenService) : base(dbContext, tokenService)
        {
            _dbContext = dbContext;
            _tokenService = tokenService;
        }

        public async Task<AleAuditoria> AddAuditoria(AleAuditoria entity)
        {
            try
            {
                entity.FechaAdicion = DateTime.Now;

                _context.Set<AleAuditoria>().Add(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error al agregar la entidad", ex);
            }
        }
    }
}
