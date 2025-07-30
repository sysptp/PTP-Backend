using System.Collections.Concurrent;
using System.Data;
using System.Linq.Expressions;
using System.Reflection;
using BussinessLayer.DTOs.Common;
using BussinessLayer.Interfaces.Repositories;
using Dapper;
using DataLayer.Models.Otros;
using DataLayer.PDbContex;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


namespace BussinessLayer.Repository.ROtros
{
    public class GenericRepository<T> : IGenericRepository<T> where T : AuditableEntities
    {
        protected readonly ITokenService _tokenService;
        protected readonly PDbContext _context;
        private readonly string _connectionString;

        public GenericRepository(PDbContext dbContext, ITokenService tokenService)
        {
            _context = dbContext;
            _tokenService = tokenService;

            _connectionString = GetConnectionString();
        }

        private static string GetConnectionString()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            return configuration.GetConnectionString("POS_CONN");
        }

        public virtual async Task<T> GetById(object id)
        {
            var entity = await _context.Set<T>().FindAsync(id);
            if (entity == null)
            {
                return null;
            }
            return entity.Borrado == true ? null : entity;
        }

        public virtual async Task<IList<T>> GetAll()
        {
            try
            {
                return await _context.Set<T>().Where(e => !e.Borrado).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public virtual async Task<(IList<T> Data, int TotalCount)> GetAllPaginatedAsync(
           PaginationRequest pagination,
           Expression<Func<T, bool>>? filter = null,
           Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
           string includeProperties = "")
        {
            IQueryable<T> query = _context.Set<T>();

            // Aplicar filtro de no borrados
            query = query.Where(e => EF.Property<bool>(e, "Borrado") == false);

            // Aplicar filtro adicional si existe
            if (filter != null)
            {
                query = query.Where(filter);
            }

            // Incluir propiedades relacionadas
            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty.Trim());
            }

            // Contar total antes de paginación
            var totalCount = await query.CountAsync();

            // Aplicar ordenamiento
            if (orderBy != null)
            {
                query = orderBy(query);
            }
            else
            {
                // Ordenamiento por defecto usando reflexión
                query = ApplyDefaultOrdering(query);
            }

            // Aplicar paginación si está habilitada
            if (pagination.HasPagination)
            {
                query = query.Skip(pagination.Skip).Take(pagination.PageSize);
            }

            var data = await query.ToListAsync();

            return (data, totalCount);
        }

        // ⭐ VERSIÓN SIMPLIFICADA para repositorios específicos
        public virtual async Task<(IList<T> Data, int TotalCount)> GetAllWithIncludePaginatedAsync(
            List<string> includeProperties,
            PaginationRequest pagination,
            Expression<Func<T, bool>>? filter = null)
        {
            IQueryable<T> query = _context.Set<T>();

            // Aplicar filtro de no borrados
            query = query.Where(e => EF.Property<bool>(e, "Borrado") == false);

            // Aplicar filtro adicional
            if (filter != null)
            {
                query = query.Where(filter);
            }

            // Incluir propiedades relacionadas
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            // Contar total
            var totalCount = await query.CountAsync();

            // Aplicar ordenamiento por defecto
            query = ApplyDefaultOrdering(query);

            // Aplicar paginación
            if (pagination.HasPagination)
            {
                query = query.Skip(pagination.Skip).Take(pagination.PageSize);
            }

            var data = await query.ToListAsync();

            return (data, totalCount);
        }


        public virtual async Task Update(T entity, Object id)
        {
            try
            {
                // Obtener la entidad existente con tracking
                var existingEntity = await _context.Set<T>().FindAsync(id);
                if (existingEntity == null)
                {
                    throw new InvalidOperationException("La entidad no existe o ha sido eliminada.");
                }

                // Preservar valores de auditoría originales
                var originalFechaAdicion = existingEntity.FechaAdicion;
                var originalUsuarioAdicion = existingEntity.UsuarioAdicion;

                // Actualizar todas las propiedades
                _context.Entry(existingEntity).CurrentValues.SetValues(entity);

                // Restaurar valores que no deben cambiar
                existingEntity.FechaAdicion = originalFechaAdicion;
                existingEntity.UsuarioAdicion = originalUsuarioAdicion;

                // Asignar valores de modificación
                existingEntity.FechaModificacion = DateTime.Now;
                existingEntity.UsuarioModificacion = _tokenService.GetClaimValue("sub") ?? "UsuarioDesconocido";

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message, ex);
            }
        }
        public virtual async Task<T> Add(T entity)
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
                throw new InvalidOperationException(ex.Message, ex);
            }
        }
        public virtual async Task AddRangeAsync(IEnumerable<T> entities)
        {
            try
            {
                foreach (var entity in entities)
                {
                    entity.FechaAdicion = DateTime.Now;
                    entity.UsuarioAdicion = _tokenService.GetClaimValue("sub") ?? "UsuarioDesconocido";
                }

                _context.Set<T>().AddRange(entities);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message, ex);
            }
        }
        public virtual async Task AddRangeCompositeKeyAsync(IEnumerable<T> entities)
        {
            try
            {
                foreach (var entity in entities)
                {
                    entity.FechaAdicion = DateTime.Now;
                    entity.UsuarioAdicion = _tokenService.GetClaimValue("sub") ?? "UsuarioDesconocido";
                }

                var dbContext = _context;
                var entityType = dbContext.Model.FindEntityType(typeof(T));
                var tableName = entityType?.GetTableName();

                if (string.IsNullOrEmpty(tableName))
                {
                    throw new InvalidOperationException("No se pudo determinar el nombre de la tabla.");
                }

                var columns = typeof(T).GetProperties()
                    .Where(p => p.PropertyType.IsPrimitive ||
                               p.PropertyType == typeof(string) ||
                               p.PropertyType == typeof(DateTime) ||
                               p.PropertyType == typeof(TimeSpan) ||
                               (Nullable.GetUnderlyingType(p.PropertyType)?.IsPrimitive ?? false) ||
                               Nullable.GetUnderlyingType(p.PropertyType) == typeof(DateTime) ||
                               Nullable.GetUnderlyingType(p.PropertyType) == typeof(TimeSpan))
                    .Select(p => p.Name);

                var values = string.Join(", ", columns.Select(c => $"@{c}"));

                var primaryKey = entityType.FindPrimaryKey();
                if (primaryKey?.Properties?.Count > 1)
                {
                    var keyColumns = primaryKey.Properties.Select(p => p.Name).ToList();
                    var whereConditions = string.Join(" AND ", keyColumns.Select(k => $"{k} = @{k}"));

                    foreach (var entity in entities)
                    {
                        var checkSql = $"SELECT COUNT(1) FROM {tableName} WHERE {whereConditions}";

                        using (var connection = new SqlConnection(_connectionString))
                        {
                            var exists = await connection.ExecuteScalarAsync<int>(checkSql, entity);
                            if (exists > 0)
                            {
                                continue;
                            }
                        }
                    }
                }

                var sql = $@"
INSERT INTO {tableName} ({string.Join(", ", columns)})
VALUES ({values})";

                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.ExecuteAsync(sql, entities);
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error en AddRangeCompositeKeyAsync: {ex.Message}", ex);
            }
        }

        public virtual async Task Delete(Object id)
        {
            try
            {
                var entity = await _context.Set<T>().FindAsync(id);
                if (entity == null)
                {
                    throw new InvalidOperationException("La entidad no existe o ha sido eliminada.");
                }

                entity.Borrado = true;
                entity.FechaModificacion = DateTime.Now;
                entity.UsuarioModificacion = _tokenService.GetClaimValue("sub") ?? "UsuarioDesconocido";

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message, ex);
            }
        }

        public virtual async Task<IEnumerable<TResult>> ExecuteStoredProcedureAsync<TResult>(string storedProcedure, object parameters = null)
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
            return await query.Where(x => x.Borrado != true).ToListAsync();
        }

        private static readonly ConcurrentDictionary<Type, PropertyInfo> IdPropertyCache = new();

        public virtual async Task<T> GetAllWithIncludeByIdAsync(object id, List<string>? properties = null)
        {
            var query = _context.Set<T>().AsQueryable();

            if (properties != null)
            {
                foreach (string property in properties)
                {
                    query = query.Include(property);
                }
            }

            // Get cached ID property or find and cache it
            var idProperty = IdPropertyCache.GetOrAdd(typeof(T), type =>
            {
                var entityType = _context.Model.FindEntityType(type);
                var primaryKey = entityType?.FindPrimaryKey();

                if (primaryKey != null && primaryKey.Properties.Count == 1)
                {
                    return primaryKey.Properties[0].PropertyInfo;
                }

                // Fallback to reflection if EF Core metadata isn't available
                return type.GetProperties()
                    .FirstOrDefault(p => p.Name.Equals("Id", StringComparison.OrdinalIgnoreCase) ||
                                        p.Name.EndsWith("Id", StringComparison.OrdinalIgnoreCase) &&
                                        p.Name.Contains(type.Name.Replace("Dto", "").Replace("Response", "")));
            });

            if (idProperty == null)
            {
                throw new InvalidOperationException($"No Id property found on type {typeof(T).Name}");
            }

            // Build the where clause
            var parameter = Expression.Parameter(typeof(T), "x");
            var idPropertyAccess = Expression.Property(parameter, idProperty);
            var idConstant = Expression.Constant(Convert.ChangeType(id, idProperty.PropertyType));
            var equality = Expression.Equal(idPropertyAccess, idConstant);
            var lambda = Expression.Lambda<Func<T, bool>>(equality, parameter);

            return await query.Where(lambda).Where(x => x.Borrado != true).FirstOrDefaultAsync();
        }

        // Método auxiliar para aplicar ordenamiento por defecto
        private IQueryable<T> ApplyDefaultOrdering(IQueryable<T> query)
        {
            var entityType = typeof(T);

            // Buscar propiedades Id comunes
            var idProperty = entityType.GetProperties()
                .FirstOrDefault(p =>
                    p.Name.Equals("Id", StringComparison.OrdinalIgnoreCase) ||
                    p.Name.Equals($"{entityType.Name}Id", StringComparison.OrdinalIgnoreCase) ||
                    p.Name.EndsWith("Id", StringComparison.OrdinalIgnoreCase));

            if (idProperty != null)
            {
                // Crear expresión tipada correctamente
                var parameter = Expression.Parameter(entityType, "x");
                var property = Expression.Property(parameter, idProperty.Name);

                // Crear lambda tipado específicamente para el tipo de la propiedad
                var lambdaType = typeof(Func<,>).MakeGenericType(entityType, idProperty.PropertyType);
                var lambda = Expression.Lambda(lambdaType, property, parameter);

                // Usar reflexión para llamar OrderByDescending con los tipos correctos
                var orderByMethod = typeof(Queryable).GetMethods()
                    .First(m => m.Name == "OrderByDescending" && m.GetParameters().Length == 2)
                    .MakeGenericMethod(entityType, idProperty.PropertyType);

                return (IQueryable<T>)orderByMethod.Invoke(null, new object[] { query, lambda });
            }

            // Si no encuentra Id, usar ordenamiento por fecha de creación si existe
            var createdProperty = entityType.GetProperties()
                .FirstOrDefault(p =>
                    p.Name.Equals("FechaAdicion", StringComparison.OrdinalIgnoreCase) ||
                    p.Name.Equals("CreatedDate", StringComparison.OrdinalIgnoreCase) ||
                    p.Name.Equals("DateCreated", StringComparison.OrdinalIgnoreCase));

            if (createdProperty != null && createdProperty.PropertyType == typeof(DateTime))
            {
                var parameter = Expression.Parameter(entityType, "x");
                var property = Expression.Property(parameter, createdProperty.Name);
                var lambda = Expression.Lambda<Func<T, DateTime>>(property, parameter);

                return query.OrderByDescending(lambda);
            }

            // Fallback: sin ordenamiento específico
            return query;
        }
    }
}