using BussinessLayer.DTOs.ModuloGeneral.Geografia.DMunicipio;
using BussinessLayer.Interfaces.Services.IOtros;
using DataLayer.Models.ModuloGeneral.Geografia;

namespace BussinessLayer.Interfaces.Services.ModuloGeneral.Geografia
{
    public interface IMunicipioService : IGenericService<MunicipioRequest, MunicipioResponse, Municipio>
    {
        IEnumerable<Municipio> GetMunicipiosByProvinciaId(int provinciaId);

        Task<List<Municipio>> GetIndex();
    }
}