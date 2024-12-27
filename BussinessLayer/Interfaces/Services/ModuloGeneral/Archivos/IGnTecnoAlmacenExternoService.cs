using BussinessLayer.DTOs.ModuloGeneral.Archivos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.Interfaces.Services.ModuloGeneral.Archivos
{
    public interface IGnTecnoAlmacenExternoService
    {
        Task<int> Add(CreateGnTecnoAlmacenExternoDto dto);
        Task Update(EditGnTecnoAlmacenExternoDto dto);
        Task Delete(int id);
        Task<ViewGnTecnoAlmacenExternoDto?> GetById(int id);
        Task<List<ViewGnTecnoAlmacenExternoDto>> GetAll();
        Task<List<ViewGnTecnoAlmacenExternoDto>> GetByCompany(int idEmpresa);
    }
}
