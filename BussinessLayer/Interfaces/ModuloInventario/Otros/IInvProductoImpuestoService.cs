using BussinessLayer.DTOs.ModuloInventario.Otros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.Interfaces.ModuloInventario.Otros
{
    public interface IInvProductoImpuestoService
    {
        Task<int> Add(CreateInvProductoImpuestoDto model);
        Task Update(EditInvProductoImpuestoDto model);
        Task Delete(int id);
        Task<ViewInvProductoImpuestoDto> GetById(int id);
        Task<List<ViewInvProductoImpuestoDto>> GetAll();
    }
}
