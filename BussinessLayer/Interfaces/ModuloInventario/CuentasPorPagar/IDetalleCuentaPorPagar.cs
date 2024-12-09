using BussinessLayer.Interface.IOtros;

public interface IDetalleCuentaPorPagar : IBaseService<DetalleCuentaPorPagar>
{
    Task<IEnumerable<DetalleCuentaPorPagar>> GetAllByIdCtaPorPagar(int idcta);
}

