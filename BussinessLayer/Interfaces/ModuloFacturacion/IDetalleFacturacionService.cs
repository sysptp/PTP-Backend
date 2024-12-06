using BussinessLayer.Interface.IOtros;

namespace BussinessLayer.Interfaces.ModuloFacturacion
{
    public interface IDetalleFacturacionService : IBaseService<DetalleFacturacion>
    {
        Task<IEnumerable<DetalleFacturacion>> GetDetalleByFacturacionId(int facturacionId);
    }
}