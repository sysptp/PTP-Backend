//public class MovimientoAlmacenService : IMovimientoAlmacenService
//{
    //private readonly PDbContext _context;
    //private readonly ICuentaPorPagarService _cppService;
    //private readonly ITipoPagoService _tipoPagoService;
    //private readonly ITipoMovimientoService _tipoMovimientoService;

    //public MovimientoAlmacenService(
    //    PDbContext dbContext, 
    //    ICuentaPorPagarService cuentaPorPagarService,
    //    ITipoPagoService tipoPagoService, 
    //    ITipoMovimientoService tipoMovimientoService)
    //{
    //    _context = dbContext;
    //    _cppService = cuentaPorPagarService;
    //    _tipoMovimientoService = tipoMovimientoService;
    //    _tipoPagoService = tipoPagoService;
    //}


    //public async Task Add(MovimientoAlmacen entity, List<DetalleMovimientoAlmacen> dma, string fechaLimite, decimal montoInicial)
    //{
    //    try
    //    {
    //        _context.MovimientoAlmacenes.Add(entity);
    //        await _context.SaveChangesAsync();
    //        int maxId = GetMaxIdMovimientoAlmacen();
    //        var tp = await _tipoPagoService.GetById(entity.IdMetodoPago, entity.IdEmpresa);
    //        var tpm = await _tipoMovimientoService.GetById(entity.IdTipoMovimiento, entity.IdEmpresa);

    //        if ((TipoPago)int.Parse(tp.IN_OUT.ToString()) == TipoPago.Credito)
    //        {
    //            CuentasPorPagar _cuentasPorPagar = new CuentasPorPagar
    //            {
    //                Descripcion = "N/A",
    //                IdMovimientoAlmacen = maxId,
    //                MontoDeuda = entity.TotalFacturado - montoInicial,
    //                MontoInicial = montoInicial, //Pendiente
    //                Restante = entity.TotalFacturado - montoInicial,
    //                FechaLimitePago = Convert.ToDateTime(fechaLimite),
    //                FechaUltimoPago = DateTime.Now, //Pendiendte => debe venir del detalle
    //                IsPaid = false,
    //                IdUsuario = 0, //Pediente;
    //                IdEmpresa = entity.IdEmpresa
    //            };

    //            await _cppService.AddWithInicial(_cuentasPorPagar);

    //            foreach (var d in dma)
    //            {
    //                d.IdMovimiento = maxId;
    //                d.IdEmpresa = entity.IdEmpresa;
    //                _context.DetalleMovimientoAlmacenes.Add(d);
    //                await _context.SaveChangesAsync();
    //            }

    //            foreach (var d in dma)
    //            {
    //                d.IdMovimiento = maxId;
    //                Producto p = _context.Productos.Find(d.IdProducto);
    //                p.CantidadInventario += d.Cantidad;
    //                d.IdEmpresa = tpm.IdEmpresa;
    //                _context.Entry(p).State = EntityState.Modified;
    //                _context.DetalleMovimientoAlmacenes.Add(d);
    //                await _context.SaveChangesAsync();
    //            }

    //        }
    //        else if ((TipoPago)int.Parse(tp.IN_OUT.ToString()) == TipoPago.Contado)
    //        {

    //            if ((TipoMovimientoEnum)int.Parse(tpm.IN_OUT.ToString()) == TipoMovimientoEnum.ENTRADA)
    //            {
    //                foreach (var d in dma)
    //                {
    //                    d.IdMovimiento = maxId;
    //                    Producto p = _context.Productos.Find(d.IdProducto);
    //                    p.CantidadInventario += d.Cantidad;
    //                    d.IdEmpresa = tpm.IdEmpresa;
    //                    _context.Entry(p).State = EntityState.Modified;
    //                    _context.DetalleMovimientoAlmacenes.Add(d);
    //                    await _context.SaveChangesAsync();
    //                }
    //            }
    //            else if ((TipoMovimientoEnum)int.Parse(tpm.IN_OUT.ToString()) == TipoMovimientoEnum.SALIDA ||
    //                (TipoMovimientoEnum)int.Parse(tpm.IN_OUT.ToString()) == TipoMovimientoEnum.DEVOLUCION)
    //            {
    //                foreach (var d in dma)
    //                {
    //                    d.IdMovimiento = maxId;
    //                    Producto p = _context.Productos.Find(d.IdProducto);
    //                    p.CantidadInventario -= d.Cantidad;
    //                    d.IdEmpresa = tpm.IdEmpresa;
    //                    _context.Entry(p).State = EntityState.Modified;
    //                    _context.DetalleMovimientoAlmacenes.Add(d);
    //                    await _context.SaveChangesAsync();
    //                }
    //            }
    //        }
    //    }
    //    catch (Exception e)
    //    {
    //        Console.WriteLine(e);
    //        throw;
    //    }
    //}

    //public async Task Edit(MovimientoAlmacen entity)
    //{
    //    try
    //    {
    //        _context.Entry(entity).State = EntityState.Modified;
    //        await _context.SaveChangesAsync();
    //    }
    //    catch (Exception e)
    //    {
    //        Console.WriteLine(e);
    //        throw;
    //    }
    //}

    //public async Task<MovimientoAlmacen> GetById(int id, long idEMpresa)
    //{
    //    try
    //    {
    //        return await _context.MovimientoAlmacenes.FindAsync(id);
    //    }
    //    catch (Exception e)
    //    {
    //        Console.WriteLine(e);
    //        throw;
    //    }
    //}

    //public async Task<List<MovimientoAlmacen>> GetAll(long idEMpresa)
    //{
    //    return await _context.MovimientoAlmacenes.Where(x => x.Borrado != true && x.IdEmpresa == idEMpresa).ToListAsync();
    //}

    //public async Task Delete(int id, long idEMpresa)
    //{
    //    var entity = await _context.MovimientoAlmacenes.Where(x => x.IdEmpresa == idEMpresa && x.Id == id).SingleAsync();
    //    if (entity != null)
    //    {
    //        entity.Borrado = true;
    //        await _context.SaveChangesAsync();
    //    }
    //}

    //public int GetMaxIdMovimientoAlmacen()
    //{
    //    int maxID = _context.MovimientoAlmacenes.Max(x => x.Id);
    //    return maxID;
    //}

//}

