using System.Collections.Generic;
using System.Threading.Tasks;
using BussinessLayer.Interface.IOtros;
using DataLayer.Models.Facturas;

namespace BussinessLayer.Interfaces.ModuloFacturacion
{
    public interface IDetalleFacturacionService : IBaseService<DetalleFacturacion>
    {
        Task<IEnumerable<DetalleFacturacion>> GetDetalleByFacturacionId(int facturacionId);
    }
}