using BussinessLayer.Interface.IFacturacion;
using BussinessLayer.Interface.IOtros;
using BussinessLayer.Interfaces.ICuentas;
using BussinessLayer.ViewModels;
using DataLayer.Models.Cuentas;
using DataLayer.Models.Facturas;
using DataLayer.Models.ModuloInventario;
using DataLayer.Models.Otros;
using DataLayer.PDbContex;
using Microsoft.EntityFrameworkCore;
using TipoPago = DataLayer.Enums.TipoPago;

namespace BussinessLayer.Services.SFacturacion
{
    public class FacturacionService : IFacturacionService
    {
        private readonly PDbContext _context;
        private readonly IDetalleFacturacionService _detalleFacturacionService;
        private readonly ICuentasPorCobrar _cuentasPorCobrarService;
        private readonly IDgiiNcfService _dgii;

        public FacturacionService(IDetalleFacturacionService detalleFacturacionService, 
            ICuentasPorCobrar cuentasPorCobrar, 
            IDgiiNcfService dgiiNcfService, PDbContext pDbContext)
        {
            _context = pDbContext;
            _cuentasPorCobrarService = cuentasPorCobrar;
            _dgii = dgiiNcfService; 
            _detalleFacturacionService = detalleFacturacionService;
        }


        public async Task Create(FacturacionViewModel vm)
        {
            if (vm.Facturacion != null && vm.DetalleFacturacions.Count > 0)
            {
                //Facturar
                string ncf = await _dgii.GetSeqNcfByTipoNcf(vm.Facturacion.SecuenciaId);

                vm.Facturacion.Ncf = string.IsNullOrEmpty(ncf)? " -":ncf;
                DgiiNcfSecuencia ncfEditar = await _dgii.FindNcfBySequencial(ncf);
                vm.Facturacion.SecuenciaId = ncfEditar.Id;
                await this.Add(vm.Facturacion);

                //Cambiar estado de NCF a True(Enviado a la Dgii)
                
                ncfEditar.Estado = true;
                await _dgii.Edit(ncfEditar);

                var maxId = await _context.Facturacion.MaxAsync(x => x.Id);

                if ((TipoPago)vm.Facturacion.TipoPagoId == TipoPago.Credito)
                {
                    var cuentasPorCobrar = new CuentasPorCobrar
                    {
                        FacturacionId = vm.Facturacion.Id,
                        ClienteId = vm.Facturacion.ClienteId,
                        IsPaid = false,
                        FechaLimite = DateTime.Now,
                        FechaUltimoPago = DateTime.Now, // Revisar si es correcto dejarlo en la fecha actual
                        MontoInicial = 0,
                        MontoPendiente = vm.Facturacion.MontoTotal, // Asumiendo que MontoPendiente es igual al MontoTotal menos el MontoInicial
                        MontoTotal = vm.Facturacion.MontoTotal,
                        Borrado = false,
                        IdEmpresa = vm.Facturacion.IdEmpresa,
                        DetalleCuentasPorCobrar = new List<DetalleCuentasPorCobrar>
                        {
                            new DetalleCuentasPorCobrar
                            {
                                Monto = 0, // Monto inicial
                                FechaPago = DateTime.Now,
                                IdUsuario = vm.Facturacion.CodigoUsuario, // Identity
                                IsCalceled = false,
                                IdEmpresa = vm.Facturacion.IdEmpresa
                            }
                        }
                    };


                    await _cuentasPorCobrarService.Add(cuentasPorCobrar);      
                }

                foreach (var d in vm.DetalleFacturacions)
                {
                    d.FacturacionId = maxId;
                    d.IdEmpresa= vm.Facturacion.IdEmpresa;

                    await _detalleFacturacionService.Add(d);

                    //Reduccion Inventario y aumento en Servicios
                    Producto p = await _context.Productos.FindAsync(d.ProductoId);
                    if(p.EsProducto == true)
                    { p.CantidadInventario += d.Cantidad; }
                    else { p.CantidadInventario -= d.Cantidad; }
                    
                    _context.Entry(p).State = EntityState.Modified;
                    //Fin Reduccion

                    await _context.SaveChangesAsync();

                }
            }
        }

        public async Task Add(Facturacion entity)
        {
            if (entity != null)
            {
                try
                {
                    entity.Borrado = false;
                    entity.Comentario = "N/A";
                    entity.Justificacion = "N/A";
                    //entity.Id = 0;
                    _context.Facturacion.Add(entity);
                    await _context.SaveChangesAsync();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }

        public async Task<IList<Facturacion>> GetAll()
        {
            return await _context.Facturacion.Where(x => x.Borrado != true).Include(x => x.Cliente).ToListAsync();
        }

        public async Task Delete(Facturacion model)
        {
            _context.Facturacion.Remove(model);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Facturacion model)
        {
            _context.Entry(model).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<Facturacion> GetById(int id)
        {
            var result = await _context.Facturacion.FindAsync(id);

            return result;
        }

    }
}
