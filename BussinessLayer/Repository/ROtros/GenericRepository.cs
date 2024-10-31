using BussinessLayer.Interfaces.IAutenticacion;
using BussinessLayer.Interfaces.Repositories;
using DataLayer.Models.Otros;
using DataLayer.PDbContex;
using Microsoft.EntityFrameworkCore;

namespace BussinessLayer.Repository.ROtros
{
    public class GenericRepository<T> : IGenericRepository<T> where T : AuditableEntities
    {
        protected readonly PDbContext _context;
        private readonly IClaimsService _claimsService;

        public GenericRepository(PDbContext dbContext, IClaimsService claimsService)
        {
            _context = dbContext;
            _claimsService = claimsService;
        }

        public async Task<T> GetById(int id)
        {
            var entity = await _context.Set<T>().FindAsync(id);
            return entity?.Borrado == true ? null : entity;
        }

        public async Task<IList<T>> GetAll()
        {
            return await _context.Set<T>().Where(e => !e.Borrado).ToListAsync();
        }

        public async Task<T> Add(T entity)
        {
            try
            {
                entity.FECHA_ADICION = DateTime.Now;
                entity.USUARIO_ADICCIONUSUARIO_ADICCION = _claimsService.GetClaimValueByType("Usuario") ?? "UsuarioDesconocido";

                _context.Set<T>().Add(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error al agregar la entidad", ex);
            }
        }

        public async Task Update(T entity)
        {
            try
            {
                entity.FECHA_MODIFICACION = DateTime.Now;
                entity.USUARIO_MODIFICACION = _claimsService.GetClaimValueByType("Usuario") ?? "UsuarioDesconocido";

                _context.Entry(entity).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error al actualizar la entidad", ex);
            }
        }

        public async Task Delete(int id)
        {
            var entity = await GetById(id);
            if (entity != null)
            {
                try
                {
                    entity.Borrado = true;
                    entity.FECHA_MODIFICACION = DateTime.Now;
                    entity.USUARIO_MODIFICACION = _claimsService.GetClaimValueByType("NombreUsuario") ?? "UsuarioDesconocido";

                    _context.Entry(entity).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException("Error al eliminar la entidad", ex);
                }
            }
        }
    }
}