using BussinessLayer.DTOs.ModuloGeneral.Configuracion.Geografia.DMunicipio;
using BussinessLayer.Interfaces.IOtros;
using DataLayer.Models.Geografia;

namespace BussinessLayer.Interfaces.IGeografia
{
    public interface IMunicipioService : IGenericService<MunicipioRequest, MunicipioResponse, Municipio>
    {
        IEnumerable<Municipio> GetMunicipiosByProvinciaId(int provinciaId);

        Task<List<Municipio>> GetIndex();
    }
}