using BussinessLayer.Interface.IOtros;

public interface ICuentasPorCobrar : IBaseService<CuentasPorCobrar>
{
    Task<IEnumerable<CuentasPorCobrar>> GetAllPaids();
}

