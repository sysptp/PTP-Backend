using DataLayer.PDbContex;
using BussinessLayer.Helpers.CargaMasivaHelpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Http;
using BussinessLayer.Interfaces.Services.ICargaMasiva;

namespace BussinessLayer.Services.SCargaMasiva
{
    public class CargaMasivaService : ICargaMasivaService
    {
        private readonly PDbContext _context;
        private readonly CsvProcessor _csvProcessor;
        private readonly EntityMapper _entityMapper;

        public CargaMasivaService()
        {
            _context = new PDbContext();
            _csvProcessor = new CsvProcessor();
            _entityMapper = new EntityMapper();
        }

        public async Task<(bool Success, string ErrorMessage)> ProcessCsvFileAsync(string tableName, IFormFile file, string delimitador)
        {
            try
            {
                var columnDetails = await GetColumnDetailsAsync(tableName);

                var modelAssembly = typeof(PDbContext).Assembly;
                var entityType = modelAssembly.GetTypes()
                    .FirstOrDefault(t => t.Name.Equals(tableName, StringComparison.OrdinalIgnoreCase) &&
                                         t.Namespace == "DataLayer.Models");

                if (entityType == null)
                {
                    return (false, $"No se encontró una clase correspondiente a la tabla '{tableName}' en el ensamblado de modelos.");
                }

                var dataRows = _csvProcessor.ReadCsv(file, delimitador);
                var entityList = new List<object>();

                int lineNumber = 1;

                foreach (var row in dataRows)
                {
                    var columnsToProcess = columnDetails.Where(c => !c.IsIdentity).ToList();

                    if (row.Length != columnsToProcess.Count)
                    {
                        return (false, $"El número de columnas en la línea {lineNumber} no coincide con la tabla.");
                    }

                    try
                    {
                        var entityInstance = _entityMapper.MapToEntity(row, columnsToProcess, entityType);
                        entityList.Add(entityInstance);
                    }
                    catch (InvalidOperationException ex)
                    {
                        // Identificar la columna en la que ocurrió el error
                        var errorMessage = $"Error en la línea {lineNumber}: {ex.Message}";
                        return (false, errorMessage);
                    }

                    lineNumber++;
                }

                if (entityList.Any())
                {
                    var setMethod = typeof(DbContext).GetMethod("Set", []).MakeGenericMethod(entityType);
                    var dbSet = setMethod.Invoke(_context, null);
                    var addRangeMethod = dbSet.GetType().GetMethod("AddRange");
                    addRangeMethod.Invoke(dbSet, new object[] { entityList });

                    await _context.SaveChangesAsync();
                }

                return (true, $"Se insertaron: {entityList.Count} registros exitosamente");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al procesar el archivo: {ex.Message}");
                return (false, "Ocurrió un error al procesar el archivo.");
            }
        }

        public async Task<List<string>> GetTableNamesAsync()
        {
            var tableNames = new List<string>();
            try
            {
                var query = "SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE'";
                tableNames = await _context.Database.SqlQueryRaw<string>(query).ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener los nombres de las tablas: {ex.Message}");
                throw;
            }
            return tableNames;
        }

        public async Task<List<(string Name, bool IsIdentity, bool IsNullable)>> GetColumnDetailsAsync(string tableName)
        {
            var columnDetails = new List<(string Name, bool IsIdentity, bool IsNullable)>();
            try
            {
                var query = @"
            SELECT 
                COLUMN_NAME AS Name,
                COLUMNPROPERTY(OBJECT_ID(TABLE_SCHEMA + '.' + TABLE_NAME), COLUMN_NAME, 'IsIdentity') AS IsIdentity,
                IS_NULLABLE AS IsNullable
            FROM INFORMATION_SCHEMA.COLUMNS 
            WHERE TABLE_NAME = @tableName";

                var result = await _context.Database.SqlQueryRaw<ColumnDetail>(query, new SqlParameter("@tableName", tableName)).ToListAsync();

                foreach (var item in result)
                {
                    columnDetails.Add((item.Name, item.IsIdentity == 1, item.IsNullable.Equals("YES", StringComparison.OrdinalIgnoreCase)));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener los nombres de las columnas de la tabla: {ex.Message}");
                throw;
            }
            return columnDetails;
        }

        public string GenerateEmptyCsv(List<(string Name, bool IsIdentity, bool IsNullable)> columnDetails, string delimitador)
        {
            var columnNames = columnDetails.Where(c => !c.IsIdentity).Select(c => c.Name).ToList();
            return string.Join(delimitador, columnNames) + "\n";
        }

        private class ColumnDetail
        {
            public string Name { get; set; }
            public int IsIdentity { get; set; }
            public string IsNullable { get; set; }
        }
    }
}
