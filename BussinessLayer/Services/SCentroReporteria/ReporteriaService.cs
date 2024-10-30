using BussinessLayer.DTOs.CentroReporteriaDTOs;
using BussinessLayer.ViewModels;
using DataLayer.Models.Reporteria;
using DataLayer.PDbContex;
using System.Data.Common;
using DataLayer.Data;
using BussinessLayer.Interfaces.ICentroReporteria;
using Microsoft.EntityFrameworkCore;
using Dapper;

namespace BussinessLayer.Services.SCentroReporteria
{
    public class ReporteriaService : IReporteriaService
    {
        // CREADO POR MANUEL 3/10/2024
        private readonly PDbContext _context;

        public ReporteriaService(PDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<List<Dictionary<string, object>>> ExecuteQuery(string sql, List<DbParameter> parameters)
        {
            var result = new List<Dictionary<string, object>>();

            // Creamos y abrimos la conexión a la base de datos
            Connection conn = new();

            using (var connection = await conn.Conn())
            {
                await connection.OpenAsync();

                IEnumerable<dynamic> rows;

                if (parameters.Count > 0)
                {
                    var parametersDictionary = new Dictionary<string, object>();
                    foreach (var parameter in parameters)
                    {
                        parametersDictionary[parameter.ParameterName] = parameter.Value;
                    }

                    rows = await connection.QueryAsync(sql, parametersDictionary);
                }
                else
                {
                    rows = await connection.QueryAsync(sql);
                }

                if (rows == null || !rows.Any())
                {
                    return new List<Dictionary<string, object>>();
                }
                else
                {
                    // Iteramos sobre cada fila del resultado
                    foreach (var row in rows)
                    {
                        var rowDict = new Dictionary<string, object>();

                        // Convertimos cada fila a ExpandoObject
                        var expandoRow = row as IDictionary<string, object>;

                        if (expandoRow != null)
                        {
                            foreach (var property in expandoRow)
                            {
                                rowDict[property.Key] = property.Value;
                            }
                        }
                        else
                        {
                            // Si la fila no es un IDictionary, se usa reflexión
                            var properties = row.GetType().GetProperties();
                            foreach (var property in properties)
                            {
                                rowDict[property.Name] = property.GetValue(row);
                            }
                        }

                        result.Add(rowDict);
                    }
                }

                // No necesitas cerrar la conexión explícitamente, ya que se usa 'using'
            }

            return result;
        }


        #region centro reporteria

        public async Task<int> GetLastAdd(int idEmpresa)
        {
            // Obtener el último registro de CentroReporteria para la empresa dada
            var lastCentroReporteria = await _context.CentroReporterias
                .Where(c => c.IdEmpresa == idEmpresa)
                .OrderByDescending(c => c.Id)
                .FirstOrDefaultAsync();

            if (lastCentroReporteria != null)
            {
                return lastCentroReporteria.Id; // Retorna el Id del último registro
            }

            return 0; // Retorna 0 si no hay registros para la empresa
        }

        public async Task<int> Add(CentroReporteria reporteria)
        {
            _context.CentroReporterias.Add(reporteria);
            await _context.SaveChangesAsync();
            return reporteria.Id;
        }

        public async Task Delete(CentroReporteria reporteria)
        {
            reporteria.Estado = "I";
            _context.Entry(reporteria).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<List<ReporteriaViewModel>> GetAll(int idEmpresa)
        {
            var data = await _context.CentroReporterias.Where(c => c.IdEmpresa == idEmpresa && c.Estado == "A")
                .OrderByDescending(c => c.NumQuery)
                .ToListAsync();

            // Mapear los datos a ReporteriaViewModel
            var result = data.Select(c => new ReporteriaViewModel
            {
                Id = c.Id,
                NombreReporte = c.NombreReporte,
                Estado = c.Estado.ToString(), // Aseguramos que Estado sea un string
                DescripcionReporte = c.DescripcionReporte,
                FechaAdicion = c.FechaAdicion,
                AdicionadoPor = c.AdicionadoPor,
                NumQuery = c.NumQuery
            }).ToList();

            return result;
        }

        public async Task<ReporteriaViewModel> GetById(int id, int idEmpresa)
        {
            var result = await _context.CentroReporterias.Where(x => x.NumQuery == id 
            && x.IdEmpresa == idEmpresa 
            && x.Estado == "A").FirstOrDefaultAsync();

            var data = new ReporteriaViewModel
            {
                Id = result.Id,
                NumQuery = result.NumQuery,
                AdicionadoPor = result.AdicionadoPor,
                DescripcionReporte= result.DescripcionReporte,
                Estado = result.Estado.ToString(),
                FechaAdicion= result.FechaAdicion,
                NombreReporte= result.NombreReporte,
                EsPesado = (bool)result.EsPesado,
                EsSubquery = (bool)result.EsSubquery,
                QueryCommand = result.QueryCommand,
            };

            return data;
        }

        public async Task<CentroReporteria> GetCentroById(int id)
        {
            var result = await _context.CentroReporterias
                .Where(x => x.Id == id && x.Estado == "A").FirstOrDefaultAsync();

            return result;
        }

        public async Task Update(CentroReporteria reporteria)
        {
            _context.Entry(reporteria).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        #endregion

        #region variables
        public async Task DeleteVariable(VariablesReporteria variable)
        {
            variable.Estado = "I";
            _context.Entry(variable).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<int> AddVariable(VariablesReporteria variable)
        {
            _context.VariablesReporterias.Add(variable);
            await _context.SaveChangesAsync();
            return variable.Id;
        }

        public async Task<List<VariablesReporteria>> GetAllVariables()
        {
            var data = await _context.VariablesReporterias.ToListAsync();
            return data.Where(x => x.Estado == "A").ToList();
        }

        public async Task<List<JsonVariables>> GetVariableById(int idReporteria, int idEmpresa)
        {
            var result = await _context.VariablesReporterias
                .Where(x => x.IdCentroReporteria == idReporteria 
                && x.IdEmpresa == idEmpresa
                && x.Estado == "A")
                .ToListAsync();

            var data = result.Select(c => new JsonVariables
            {
                Id = c.Id.ToString(),
                EsObligatorio = (bool)c.EsObligatorio,
                NombreVariable = c.NombreVariable,
                TipoVariable = c.TipoVariable,
                ValorPorDefecto = c.ValorPorDefecto,
                Variable = c.Variable
            }).ToList();

            return data;
        }

        public async Task UpdateVariable(VariablesReporteria variable)
        {
            _context.Entry(variable).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<VariablesReporteria> VarById(int id)
        {
            var result = await _context.VariablesReporterias
                .Where(x => x.Id == id && x.Estado == "A").FirstOrDefaultAsync();
            return result;
        }

        #endregion

    }
}
