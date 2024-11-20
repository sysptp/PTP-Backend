using BussinessLayer.DTOs.CentroReporteriaDTOs;
using BussinessLayer.ViewModels;
using DataLayer.Models.Reporteria;
using System.Data.Common;

namespace BussinessLayer.Interfaces.ModuloReporteria
{
    // CREADO POR MANUEL 3/10/2024 - INTERFAZ PARA EL MANEJO DE LA REPORTERIA
    public interface IReporteriaService
    {
        Task<List<Dictionary<string, object>>> ExecuteQuery(string sql, List<DbParameter> parameters);

        Task<List<ReporteriaViewModel>> GetAll(int idEmpresa);

        Task<ReporteriaViewModel> GetById(int id, int idEmpresa);

        Task<CentroReporteria> GetCentroById(int id);

        Task<int> Add(CentroReporteria menu);

        Task Update(CentroReporteria menu);

        Task Delete(CentroReporteria menu);

        Task<int> GetLastAdd(int idEmpresa);

        // Variables
        Task<List<VariablesReporteria>> GetAllVariables();

        Task<List<JsonVariables>> GetVariableById(int idReporteria, int idEmpresa);

        Task<VariablesReporteria> VarById(int id);

        Task<int> AddVariable(VariablesReporteria variable);

        Task UpdateVariable(VariablesReporteria variable);

        Task DeleteVariable(VariablesReporteria variable);

    }
}
