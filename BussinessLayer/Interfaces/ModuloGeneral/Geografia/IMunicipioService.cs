using BussinessLayer.DTOs.ModuloGeneral.Geografia.DMunicipio;
using BussinessLayer.Interfaces.IOtros;
using DataLayer.Models.ModuloGeneral.Geografia;

namespace BussinessLayer.Interfaces.ModuloGeneral.Geografia
{
    public interface IMunicipioService : IGenericService<MunicipioRequest, MunicipioResponse, Municipio>
    {
        IEnumerable<Municipio> GetMunicipiosByProvinciaId(int provinciaId);

        Task<List<Municipio>> GetIndex();
    }
}