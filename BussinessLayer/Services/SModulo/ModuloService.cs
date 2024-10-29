using BussinessLayer.Interfaces.IModulo;
using BussinessLayer.Services.SOtros;
using DataLayer.Models.MenuApp;
using DataLayer.PDbContex;

namespace BussinessLayer.Services.SModulo
{
    public class ModuloService : GenericService<GnModulo>, IModuloService
    {
        public ModuloService(PDbContext dbContext) : base(dbContext)
        {
        }
    }
}
