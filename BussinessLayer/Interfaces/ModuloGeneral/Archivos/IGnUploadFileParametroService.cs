using BussinessLayer.DTOs.ModuloGeneral.Archivos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.Interfaces.ModuloGeneral.Archivos
{
    public interface IGnUploadFileParametroService
    {
        Task<int> Add(CreateGnUploadFileParametroDto model);
        Task Update(EditGnUploadFileParametroDto model);
        Task Delete(int id);
        Task<ViewGnUploadFileParametroDto> GetById(int id);
        Task<List<ViewGnUploadFileParametroDto>> GetAll();
    }
}
