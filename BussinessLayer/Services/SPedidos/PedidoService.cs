using Microsoft.EntityFrameworkCore;
using DataLayer.PDbContex;
using DataLayer.Models.ModuloInventario.Pedidos;

public class PedidoService : IPedidoService
{
    private readonly PDbContext _context;

    public PedidoService(PDbContext dbContext)
    {
        _context = dbContext;
    }

    public async Task Add(Pedido entity)
    {
        try
        {
            _context.Pedidos.Add(entity);
            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

    }

    public async Task Edit(Pedido entity)
    {
        try
        {
            var newPedido = await _context.Pedidos.FindAsync(entity.Id);
            if (newPedido == null) return;
            newPedido.Detalle.Clear();
            newPedido.Detalle = entity.Detalle;
            newPedido.IdSuplidor = entity.IdSuplidor;
            newPedido.Solicitado = entity.Solicitado;
            newPedido.FechaModificacion = DateTime.Now;

            _context.Entry(newPedido).State = EntityState.Modified;
            await _context.SaveChangesAsync();

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Pedido> GetById(int id, long idEMpresa)
    {
        throw new NotImplementedException();
    }

    public async Task<IList<Pedido>> GetAll(long idEMpresa)
    {
        var list = await _context.Pedidos.Where(x => !x.Borrado.Equals(true) && x.IdEmpresa== idEMpresa).Include(x => x.Detalle).Include(x => x.Suplidor).ToListAsync();
        SetProductNameToPedidos(list);
        return list;
    }

    public async Task Delete(int id, long idEMpresa)
    {
        var pedido = await GetById(id,idEMpresa);
        if (pedido == null) return;
        pedido.Borrado = true;
        await Edit(pedido);
    }

    public async Task<IList<DetallePedido>> GetDetallesByPedido(int pedidoId, long idEMpresa)
    {
        var pedido = await GetById(pedidoId,idEMpresa);
        return pedido == null ? new List<DetallePedido>() : pedido.Detalle;
    }

    private void SetProductNameToPedidos(List<Pedido> pedidos)
    {
        pedidos.ForEach(x => SetProductName(x.Detalle.ToList()));
    }

    private void SetProductName(List<DetallePedido> detalles)
    {
        throw new NotImplementedException();
    }

    //Detalles 
    public async Task<IEnumerable<Pedido>> GetDetallePedidoByPedidoId(int idPedido)
    {
        return await _context.Pedidos.Where(x=> x.Id == idPedido).Include(x => x.Detalle).Include(x=> x.Suplidor).ToListAsync();
      
    }
        
    //Header
    public async Task<Pedido> GetHeaderFromDetalle(int pedidoId) => await _context.Pedidos.FirstOrDefaultAsync(x => x.Id == pedidoId);

}
