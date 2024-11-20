using AutoMapper;
using BussinessLayer.DTOs.ModuloInventario.Pedidos;
using DataLayer.PDbContex;
using Microsoft.EntityFrameworkCore;

public class PedidoService : IPedidoService
{
    private readonly PDbContext _context;
    private readonly IMapper _mapper;
    private readonly ITokenService _tokenService;

    public PedidoService(PDbContext dbContext,
        IMapper mapper,
        ITokenService tokenService)
    {
        _context = dbContext;
        _mapper = mapper;
        _tokenService = tokenService;
    }

    // Obtener data por su id
    public async Task<ViewOrderDto> GetById(int id)
    {
        var data = await _context.Pedidos
            .Include(x => x.Suplidor)
            .Include(x => x.Detalle)
            .Where(x => x.Id == id && x.Borrado == false)
            .FirstOrDefaultAsync();
        return _mapper.Map<ViewOrderDto>(data);
    }

    // Obtener data por su empresa
    public async Task<List<ViewOrderDto>> GetByCompany(int id)
    {
        var data = await _context.Pedidos
            .Include(x => x.Suplidor)
            .Include(x => x.Detalle)
            .Where(x => x.IdEmpresa == id && x.Borrado == false)
            .ToListAsync();

        return _mapper.Map<List<ViewOrderDto>>(data);
    }

    // Crear un nuevo 
    public async Task<int?> Create(CreateOrderDto create)
    {
        var newObject = _mapper.Map<Pedido>(create);
        newObject.FechaCreacion = DateTime.Now;
        newObject.Borrado = false;
        newObject.Activo = false;
        newObject.UsuarioCreacion = _tokenService.GetClaimValue("sub") ?? "UsuarioDesconocido";

        _context.Pedidos.Add(newObject);
        await _context.SaveChangesAsync();

        return newObject.Id;
    }

    // Editar existente
    public async Task Edit(EditOrderDto edit)
    {
        var existing = await _context.Pedidos.FirstOrDefaultAsync(x => x.Id == edit.Id);
        var activox = existing?.Activo;
        if (existing != null)
        {
            _mapper.Map(edit, existing);
            existing.UsuarioModificacion = _tokenService.GetClaimValue("sub") ?? "UsuarioDesconocido";
            existing.FechaModificacion = DateTime.Now;
            existing.Activo = activox;
            _context.Pedidos.Update(existing);
            await _context.SaveChangesAsync();
        }
    }

    // Servicio para eliminar por id unico
    public async Task DeleteById(int id)
    {
        var data = await _context.Pedidos.Where(x => x.Id == id).FirstOrDefaultAsync();

        if (data != null)
        {
            data.Borrado = true;
            _context.Pedidos.Update(data);
            await _context.SaveChangesAsync();
        }
    }

    //public async Task Add(Pedido entity)
    //{
    //    try
    //    {
    //        _context.Pedidos.Add(entity);
    //        await _context.SaveChangesAsync();
    //    }
    //    catch (Exception e)
    //    {
    //        Console.WriteLine(e);
    //        throw;
    //    }

    //}

    //public async Task Edit(Pedido entity)
    //{
    //    try
    //    {
    //        var newPedido = await _context.Pedidos.FindAsync(entity.Id);
    //        if (newPedido == null) return;
    //        newPedido.Detalle.Clear();
    //        newPedido.Detalle = entity.Detalle;
    //        newPedido.IdSuplidor = entity.IdSuplidor;
    //        newPedido.Solicitado = entity.Solicitado;
    //        newPedido.FechaModificacion = DateTime.Now;

    //        _context.Entry(newPedido).State = EntityState.Modified;
    //        await _context.SaveChangesAsync();

    //    }
    //    catch (Exception e)
    //    {
    //        Console.WriteLine(e);
    //        throw;
    //    }
    //}

    //public async Task<Pedido> GetById(int id, long idEMpresa)
    //{
    //    throw new NotImplementedException();
    //}

    //public async Task<IList<Pedido>> GetAll(long idEMpresa)
    //{
    //    var list = await _context.Pedidos.Where(x => !x.Borrado.Equals(true) && x.IdEmpresa== idEMpresa).Include(x => x.Detalle).Include(x => x.Suplidor).ToListAsync();
    //    SetProductNameToPedidos(list);
    //    return list;
    //}

    //public async Task Delete(int id, long idEMpresa)
    //{
    //    var pedido = await GetById(id,idEMpresa);
    //    if (pedido == null) return;
    //    pedido.Borrado = true;
    //    await Edit(pedido);
    //}

    //public async Task<IList<DetallePedido>> GetDetallesByPedido(int pedidoId, long idEMpresa)
    //{
    //    var pedido = await GetById(pedidoId,idEMpresa);
    //    return pedido == null ? new List<DetallePedido>() : pedido.Detalle;
    //}

    //private void SetProductNameToPedidos(List<Pedido> pedidos)
    //{
    //    pedidos.ForEach(x => SetProductName(x.Detalle.ToList()));
    //}

    //private void SetProductName(List<DetallePedido> detalles)
    //{
    //    throw new NotImplementedException();
    //}

    ////Detalles 
    //public async Task<IEnumerable<Pedido>> GetDetallePedidoByPedidoId(int idPedido)
    //{
    //    return await _context.Pedidos.Where(x=> x.Id == idPedido).Include(x => x.Detalle).Include(x=> x.Suplidor).ToListAsync();

    //}

    ////Header
    //public async Task<Pedido> GetHeaderFromDetalle(int pedidoId) => await _context.Pedidos.FirstOrDefaultAsync(x => x.Id == pedidoId);

}
