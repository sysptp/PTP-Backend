using BussinessLayer.Interfaces.Repositories;
using Dapper;
using DataLayer.Models.Empresa;
using DataLayer.Models.Otros;
using DataLayer.PDbContex;
using Microsoft.EntityFrameworkCore;
using System.Data;


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

        public async Task<T> GetById(object id)
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
                entity.UsuarioAdicion = _tokenService.GetClaimValue("sub") ?? "UsuarioDesconocido";

                _context.Set<T>().Add(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error al agregar la entidad", ex);
            }
        }

        public async Task Update(T entity, object id)
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
                entity.FechaAdicion = oldEntity.FechaAdicion;
                entity.UsuarioAdicion = oldEntity.UsuarioAdicion;

                _context.Entry(oldEntity).CurrentValues.SetValues(entity);

                _context.Entry(oldEntity).State = EntityState.Modified;

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error al actualizar la entidad", ex);
            }
        }

        public async Task Update(T entity, int id)
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
                entity.FechaAdicion = oldEntity.FechaAdicion;
                entity.UsuarioAdicion = oldEntity.UsuarioAdicion;

                _context.Entry(oldEntity).CurrentValues.SetValues(entity);

                _context.Entry(oldEntity).State = EntityState.Modified;

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
                    entity.UsuarioModificacion = _tokenService.GetClaimValue("sub") ?? "UsuarioDesconocido";

                    _context.Entry(entity).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException("Error al eliminar la entidad", ex);
                }
            }
        }

        public async Task Delete(object id)
        {
            var entity = await GetById(id);
            if (entity != null)
            {
                try
                {
                    entity.Borrado = true;
                    entity.FechaModificacion = DateTime.Now;
                    entity.UsuarioModificacion = _tokenService.GetClaimValue("sub") ?? "UsuarioDesconocido";

                    _context.Entry(entity).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException("Error al eliminar la entidad", ex);
                }
            }
        }


        public async Task<IEnumerable<TResult>> ExecuteStoredProcedureAsync<TResult>(string storedProcedure, object parameters = null)
        {
            using (var connection = _context.Database.GetDbConnection())
            {
                if (connection.State == ConnectionState.Closed)
                    await connection.OpenAsync();

                var result = await connection.QueryAsync<TResult>(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }

        public virtual async Task<List<T>> GetAllWithIncludeAsync(List<string> properties)
        {
            var query = _context.Set<T>().AsQueryable();
            foreach (string property in properties)
            {
                query = query.Include(property);
            }
            return await query.ToListAsync();
        }

    }
}