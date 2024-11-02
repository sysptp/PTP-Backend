using BussinessLayer.Interfaces.Repositories;
using DataLayer.Models.Otros;
using DataLayer.PDbContex;
using Microsoft.EntityFrameworkCore;

namespace BussinessLayer.Repository.ROtros
{
    public class GenericRepository<T> : IGenericRepository<T> where T : AuditableEntities
    {
        private readonly ITokenService _tokenService;
        protected readonly PDbContext _context;

        public GenericRepository(PDbContext dbContext, ITokenService tokenService)
        {
            _context = dbContext;
            _tokenService = tokenService;
        }

        public async Task<T> GetById(int id)
        {
            var entity = await _context.Set<T>().FindAsync(id);
            if (entity == null) 
            {
                return null;
            }
            return entity.Borrado == true ? null : entity;
        }

        public async Task<IList<T>> GetAll()
        {
            return await _context.Set<T>().Where(e => !e.Borrado).ToListAsync();
        }

        public async Task<T> Add(T entity)
        {
            try
            {
                entity.FechaAdicion = DateTime.Now;
                //entity.UsuarioAdicion = _tokenService.GetClaimValue("unique_name") ?? "UsuarioDesconocido";
                entity.UsuarioAdicion = "System";

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
                entity.FechaModificacion = DateTime.Now;
                entity.UsuarioModificacion = _tokenService.GetClaimValue("unique_name") ?? "UsuarioDesconocido";

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
                    entity.FechaModificacion = DateTime.Now;
                    entity.UsuarioModificacion = _tokenService.GetClaimValue("unique_name") ?? "UsuarioDesconocido";

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