using BussinessLayer.Interface.IOtros;

public interface ICuentaPorPagarService : IBaseService<CuentasPorPagar>
{
    Task Create(CuentasPorPagar dcp);
    Task AddWithInicial(CuentasPorPagar entity);
}

