using BussinessLayer.Interfaces.Repositories;
using Dapper;
using DataLayer.Models.Otros;
using DataLayer.PDbContex;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections.Concurrent;
using System.Data;
using System.Linq.Expressions;
using System.Reflection;


namespace BussinessLayer.Repository.ROtros
{
    public class GenericRepository<T> : IGenericRepository<T> where T : AuditableEntities
    {
        private readonly ITokenService _tokenService;
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
        public virtual async Task Update(T entity, int id)
        {
            try
            {
                // Verifica que la entidad existe
                var oldEntity = await GetById(id);
                if (oldEntity == null)
                {
                    throw new InvalidOperationException("La entidad no existe o ha sido eliminada.");
                }

                // Asigna los valores de auditoría
                entity.FechaModificacion = DateTime.Now;
                entity.UsuarioModificacion = _tokenService.GetClaimValue("sub") ?? "UsuarioDesconocido";
                entity.FechaAdicion = oldEntity.FechaAdicion;
                entity.UsuarioAdicion = oldEntity.UsuarioAdicion;

                // Obtén el nombre de la tabla asociada al tipo T
                var tableName = _context.Model.FindEntityType(typeof(T)).GetTableName();

                // Obtén el nombre de la clave primaria
                var primaryKey = _context.Model.FindEntityType(typeof(T))
                                               .FindPrimaryKey()
                                               .Properties
                                               .Select(p => p.Name)
                                               .FirstOrDefault();

                if (string.IsNullOrEmpty(primaryKey))
                {
                    throw new InvalidOperationException("No se pudo determinar la clave primaria de la tabla.");
                }

                // Construye dinámicamente las columnas a actualizar
                var properties = typeof(T).GetProperties()
                                          .Where(p => p.Name != primaryKey && p.Name != "FechaAdicion" && p.Name != "UsuarioAdicion" &&
                (p.PropertyType.IsPrimitive ||
                 p.PropertyType == typeof(string) ||
                 p.PropertyType == typeof(DateTime) ||
                 (Nullable.GetUnderlyingType(p.PropertyType)?.IsPrimitive ?? false) ||
                 Nullable.GetUnderlyingType(p.PropertyType) == typeof(DateTime)))
                                          .Select(p => $"{p.Name} = @{p.Name}");
                var updateColumns = string.Join(", ", properties);

                // Construye la consulta SQL
                var sql = $@"
            UPDATE {tableName}
            SET {updateColumns}
            WHERE {primaryKey} = @PrimaryKey";

                // Prepara los parámetros con Dapper
                var parameters = new DynamicParameters(entity);
                parameters.Add("PrimaryKey", id);

                // Ejecuta la consulta
                using (var connection = new SqlConnection(_connectionString))
                {
                    if (connection.State == ConnectionState.Closed)
                        await connection.OpenAsync();

                    var rowsAffected = await connection.ExecuteAsync(sql, parameters);

                    if (rowsAffected == 0)
                        throw new InvalidOperationException("No se encontró la entidad para actualizar.");
                }
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

                var tableName = _context.Model.FindEntityType(typeof(T)).GetTableName();
                var primaryKey = _context.Model.FindEntityType(typeof(T))
                                               .FindPrimaryKey()
                                               .Properties
                                               .Select(p => p.Name)
                                               .FirstOrDefault();

                if (string.IsNullOrEmpty(primaryKey))
                {
                    throw new InvalidOperationException("No se pudo determinar la clave primaria de la tabla.");
                }

                var properties = typeof(T).GetProperties()
                    .Where(p => p.Name != primaryKey &&
                                (p.PropertyType.IsPrimitive ||
                                 p.PropertyType == typeof(string) ||
                                 p.PropertyType == typeof(DateTime) ||
                                 p.PropertyType == typeof(TimeSpan) ||
                                 (Nullable.GetUnderlyingType(p.PropertyType)?.IsPrimitive ?? false) ||
                                 Nullable.GetUnderlyingType(p.PropertyType) == typeof(DateTime) ||
                                 Nullable.GetUnderlyingType(p.PropertyType) == typeof(TimeSpan)))
                    .Select(p => p.Name);

                var columns = string.Join(", ", properties);
                var values = string.Join(", ", properties.Select(p => $"@{p}"));

                var sql = $@"
        INSERT INTO {tableName} ({columns})
        OUTPUT INSERTED.{primaryKey}
        VALUES ({values})";

                using (var connection = new SqlConnection(_connectionString))
                {
                    var id = await connection.ExecuteScalarAsync<object>(sql, entity);

                    var primaryKeyProperty = typeof(T).GetProperty(primaryKey);
                    if (primaryKeyProperty != null)
                    {
                        primaryKeyProperty.SetValue(entity, Convert.ChangeType(id, primaryKeyProperty.PropertyType));
                    }
                }

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

                var dbContext = _context;

                var tableName = dbContext.Model.FindEntityType(typeof(T))?.GetTableName();
                var primaryKey = dbContext.Model.FindEntityType(typeof(T))
                    ?.FindPrimaryKey()
                    ?.Properties
                    ?.Select(p => p.Name)
                    ?.FirstOrDefault();

                if (string.IsNullOrEmpty(primaryKey))
                {
                    throw new InvalidOperationException("No se pudo determinar la clave primaria de la tabla.");
                }

                var columns = typeof(T).GetProperties()
                    .Where(p => p.Name != primaryKey &&
                                (p.PropertyType.IsPrimitive ||
                                 p.PropertyType == typeof(string) ||
                                 p.PropertyType == typeof(DateTime) ||
                                 p.PropertyType == typeof(TimeSpan) ||
                                 (Nullable.GetUnderlyingType(p.PropertyType)?.IsPrimitive ?? false) ||
                                 Nullable.GetUnderlyingType(p.PropertyType) == typeof(DateTime) ||
                                 Nullable.GetUnderlyingType(p.PropertyType) == typeof(TimeSpan)))
                    .Select(p => p.Name);

                var values = string.Join(", ", columns.Select(c => $"@{c}"));
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

        public virtual async Task Update(T entity, Object id)
        {
            try
            {
                // Verifica que la entidad existe
                var oldEntity = await GetById(id);
                if (oldEntity == null)
                {
                    throw new InvalidOperationException("La entidad no existe o ha sido eliminada.");
                }

                // Asigna los valores de auditoría
                entity.FechaModificacion = DateTime.Now;
                entity.UsuarioModificacion = _tokenService.GetClaimValue("sub") ?? "UsuarioDesconocido";
                entity.FechaAdicion = oldEntity.FechaAdicion;
                entity.UsuarioAdicion = oldEntity.UsuarioAdicion;

                // Obtén el nombre de la tabla asociada al tipo T
                var tableName = _context.Model.FindEntityType(typeof(T)).GetTableName();

                // Obtén el nombre de la clave primaria
                var primaryKey = _context.Model.FindEntityType(typeof(T))
                                               .FindPrimaryKey()
                                               .Properties
                                               .Select(p => p.Name)
                                               .FirstOrDefault();

                if (string.IsNullOrEmpty(primaryKey))
                {
                    throw new InvalidOperationException("No se pudo determinar la clave primaria de la tabla.");
                }

                // Construye dinámicamente las columnas a actualizar
                var properties = typeof(T).GetProperties()
                    .Where(p => p.Name != primaryKey &&
                                (p.PropertyType.IsPrimitive ||
                                 p.PropertyType == typeof(string) ||
                                 p.PropertyType == typeof(DateTime) ||
                                 p.PropertyType == typeof(TimeSpan) ||
                                 (Nullable.GetUnderlyingType(p.PropertyType)?.IsPrimitive ?? false) ||
                                 Nullable.GetUnderlyingType(p.PropertyType) == typeof(DateTime) ||
                                 Nullable.GetUnderlyingType(p.PropertyType) == typeof(TimeSpan)))
                    .Select(p => p.Name);

                var setColumns = string.Join(", ", properties.Select(p => $"{p} = @{p}"));

                var sql = $@"
            UPDATE {tableName}
            SET {setColumns}
            WHERE {primaryKey} = @PrimaryKey";

                var parameters = new DynamicParameters();
                foreach (var property in properties)
                {
                    var value = typeof(T).GetProperty(property)?.GetValue(entity);
                    parameters.Add(property, value);
                }

                parameters.Add("PrimaryKey", id);

                using (var connection = new SqlConnection(_connectionString))
                {
                    if (connection.State == ConnectionState.Closed)
                        await connection.OpenAsync();

                    var rowsAffected = await connection.ExecuteAsync(sql, parameters);

                    if (rowsAffected == 0)
                        throw new InvalidOperationException("No se encontró la entidad para actualizar.");
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message, ex);
            }
        }

        public virtual async Task Delete(Object id)
        {
            try
            {
                // Verifica que la entidad existe
                var entity = await GetById(id);
                if (entity == null)
                {
                    throw new InvalidOperationException("La entidad no existe o ha sido eliminada.");
                }

                // Asigna los valores de auditoría
                entity.Borrado = true;
                entity.FechaModificacion = DateTime.Now;
                entity.UsuarioModificacion = _tokenService.GetClaimValue("sub") ?? "UsuarioDesconocido";

                // Obtén el nombre de la tabla asociada al tipo T
                var tableName = _context.Model.FindEntityType(typeof(T)).GetTableName();

                // Obtén el nombre de la clave primaria
                var primaryKey = _context.Model.FindEntityType(typeof(T))
                                               .FindPrimaryKey()
                                               .Properties
                                               .Select(p => p.Name)
                                               .FirstOrDefault();

                if (string.IsNullOrEmpty(primaryKey))
                {
                    throw new InvalidOperationException("No se pudo determinar la clave primaria de la tabla.");
                }

                // Construye la consulta SQL para Soft Delete
                var sql = $@"
            UPDATE {tableName}
            SET Borrado = @Borrado,
                FechaModificacion = @FechaModificacion,
                UsuarioModificacion = @UsuarioModificacion
            WHERE {primaryKey} = @PrimaryKey";

                // Prepara los parámetros
                var parameters = new DynamicParameters(new
                {
                    Borrado = true,
                    FechaModificacion = entity.FechaModificacion,
                    UsuarioModificacion = entity.UsuarioModificacion,
                    PrimaryKey = id
                });

                // Ejecuta la consulta
                using (var connection = new SqlConnection(_connectionString))
                {
                    if (connection.State == ConnectionState.Closed)
                        await connection.OpenAsync();

                    var rowsAffected = await connection.ExecuteAsync(sql, parameters);

                    if (rowsAffected == 0)
                        throw new InvalidOperationException("No se encontró la entidad para eliminar.");
                }
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
    }
}