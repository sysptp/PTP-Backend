using BussinessLayer.Interfaces.IAutenticacion;
using BussinessLayer.Interfaces.Repository.Empresa;
using BussinessLayer.Repository.ROtros;
using DataLayer.Models.Empresa;
using DataLayer.PDbContex;
using Microsoft.EntityFrameworkCore;

namespace BussinessLayer.Repository.REmpresa
{
    public class GnEmpresaRepository : GenericRepository<GnEmpresa>, IGnEmpresaRepository
    {
        private readonly PDbContext _dbContext;
        private readonly ITokenService _tokenService;
        public GnEmpresaRepository(PDbContext dbContext, ITokenService tokenService) : base(dbContext, tokenService) 
        {
           _dbContext = dbContext;
            _tokenService = tokenService;
        }

        public async Task Update(GnEmpresa entity, long id)
        {
            try
            {
                var oldEntity = await GetById(id);
                if (oldEntity == null)
                {
                    throw new InvalidOperationException("La entidad no existe o ha sido eliminada.");
                }

                entity.FechaModificacion = DateTime.Now;
                entity.UsuarioModificacion = _tokenService.GetClaimValue("sub") ?? "UsuarioDesconocido";

                foreach (var property in typeof(GnEmpresa).GetProperties())
                {
                    var newValue = property.GetValue(entity);
                    if (newValue != null)
                    {
                        property.SetValue(oldEntity, newValue);
                    }
                }

                _context.Entry(oldEntity).State = EntityState.Modified;

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error al actualizar la entidad", ex);
            }
        }
    }
}
