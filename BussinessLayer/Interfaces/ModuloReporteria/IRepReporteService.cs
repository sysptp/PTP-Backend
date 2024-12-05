using BussinessLayer.DTOs.ModuloReporteria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.Interfaces.ModuloReporteria
{
    public interface IRepReporteService
    {
        Task<int> Add(CreateRepReporteDto model);
        Task Update(EditRepReporteDto model);
        Task Delete(int id);
        Task<ViewRepReporteDto?> GetById(int id);
        Task<List<ViewRepReporteDto>> GetAll();
    }
}
