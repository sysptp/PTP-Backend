using BussinessLayer.Interfaces.Services.IOtros;

namespace BussinessLayer.Interfaces.Services.ModuloFacturacion
{
    public interface IDetalleFacturacionService : IBaseService<DetalleFacturacion>
    {
        Task<IEnumerable<DetalleFacturacion>> GetDetalleByFacturacionId(int facturacionId);
    }
}