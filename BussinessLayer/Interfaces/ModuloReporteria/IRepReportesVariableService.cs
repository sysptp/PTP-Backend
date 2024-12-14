using BussinessLayer.DTOs.ModuloReporteria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.Interfaces.ModuloReporteria
{
    public interface IRepReportesVariableService
    {
        Task<int> Add(CreateRepReportesVariableDto model);
        Task Update(EditRepReportesVariableDto model);
        Task Delete(int id);
        Task<ViewRepReportesVariableDto> GetById(int id);
        Task<List<ViewRepReportesVariableDto>> GetAll();
    }
}
