using AutoMapper;
using BussinessLayer.DTOs.ModuloGeneral.Configuracion.Modulo;
using BussinessLayer.Interfaces.IModulo;
using BussinessLayer.Interfaces.Repositories;
using DataLayer.Models.MenuApp;

namespace BussinessLayer.Services.SModulo
{
    public class GnModuloService : GenericService<GnModuloRequest, GnModuloResponse, GnModulo>, IGnModuloService
    {
        public GnModuloService(IGenericRepository<GnModulo> repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}
