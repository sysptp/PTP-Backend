using BussinessLayer.Interface.IOtros;

public interface IDetalleCuentasPorCobrar : IBaseService<DetalleCuentasPorCobrar>
{
    Task<IEnumerable<DetalleCuentasPorCobrar>> GetAllByCuentaPorCobrarId(int id);
}

