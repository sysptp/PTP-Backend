using AutoMapper;
using BussinessLayer.DTOs.ModuloGeneral.Modulo;
using BussinessLayer.Interfaces.Repositories;
using BussinessLayer.Interfaces.Services.ModuloGeneral.Modulo;
using DataLayer.Models.ModuloGeneral.Menu;

namespace BussinessLayer.Services.ModuloGeneral.Modulo
{
    public class GnModuloService : GenericService<GnModuloRequest, GnModuloResponse, GnModulo>, IGnModuloService
    {
        public GnModuloService(IGenericRepository<GnModulo> repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}
