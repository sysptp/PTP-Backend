using BussinessLayer.Interface.IProductos;
using BussinessLayer.ViewModels;
using DataLayer.Models.Productos;
using DataLayer.PDbContex;
using Microsoft.EntityFrameworkCore;

namespace BussinessLayer.Services.SProductos
{
    public class PrecioService : IPrecioService
    {
        private readonly IProductoService _productoService;
        private readonly PDbContext _context;

        public PrecioService(PDbContext dbContext, IProductoService productoService)
        {
            _productoService = productoService;
            _context = dbContext;
        }

        public async Task<IList<ProductoPrecioInfoViewModel>> GetProductPricesInfo(long idEmpresa)
        {
            var list = await _productoService.GetAll(idEmpresa);
            return list.Select(producto => new ProductoPrecioInfoViewModel
            {
                Nombre = $"{producto.Version.Nombre} {producto.Version.Nombre}",
                PrecioBase = producto.PrecioBase,
                Precio1 = producto.Precios.Any() && producto.Precios.Count >= 1 ? producto.Precios[0].Valor : 0,
                Precio2 = producto.Precios.Any() && producto.Precios.Count >= 2 ? producto.Precios[1].Valor : 0,
                Precio3 = producto.Precios.Any() && producto.Precios.Count == 3 ? producto.Precios[2].Valor : 0,
                ProductoId = producto.Id
            }).ToList();
        }

        public async Task AssignPrices(ProductoPrecioInfoViewModel productosPrecio, long idEmpresa)
        {
            var precios = await _context.Precios.Where(x => x.ProductoId.Equals(productosPrecio.ProductoId)).ToListAsync();

            if (!precios.Any())
            {
                await CreatePrices(productosPrecio);
                return;
            }
                
            await UpdatePrices(precios, productosPrecio);
            
        }
        private async Task CreatePrices(ProductoPrecioInfoViewModel productosPrecio)
        {

            var list = new List<Precio>();

            if (productosPrecio.Precio1 > 0)
            {
                list.Add(GetPrecio(1, productosPrecio));
            }

            if (productosPrecio.Precio2 > 0)
            {
                list.Add(GetPrecio(2, productosPrecio));
            }

            if (productosPrecio.Precio3 > 0)
            {
                list.Add(GetPrecio(3, productosPrecio));
            }

            if (list.Any())
            {
                _context.Precios.AddRange(list);
                await _context.SaveChangesAsync();

            }
        }

        private async Task UpdatePrices(List<Precio> precios, ProductoPrecioInfoViewModel productosPrecio)
        {
            for (int i = 1; i < 4; i++)
            {
                switch (i)
                {
                    case 1:

                        var precio1 = precios.SingleOrDefault(x => x.NumSeq.Equals(1));

                        if (precio1 == null)
                        {
                            _context.Precios.Add(GetPrecio(1, productosPrecio));
                            break;
                        }

                        if (!precio1.Valor.Equals(productosPrecio.Precio1) && productosPrecio.Precio1 > 0)
                        {
                            precio1.Valor = productosPrecio.Precio1;

                            //_context.Precios.AddOrUpdate(precio1);

                        }
                        else
                        {
                            _context.Entry(new Precio { Id = precio1.Id }).State = EntityState.Deleted;
                        }
                        break;

                    case 2:
                        var precio2 = precios.SingleOrDefault(x => x.NumSeq.Equals(2));
                        if (precio2 == null)
                        {
                            _context.Precios.Add(GetPrecio(2, productosPrecio));
                            break;
                        }

                        if (!precio2.Valor.Equals(productosPrecio.Precio2) && productosPrecio.Precio2 > 0)
                        {
                            precio2.Valor = productosPrecio.Precio2;
                           // _context.Precios.AddOrUpdate(precio2);

                        }
                        else
                        {
                            _context.Entry(new Precio { Id = precio2.Id }).State = EntityState.Deleted;
                        }
                        break;

                    case 3:
                        var precio3 = precios.SingleOrDefault(x => x.NumSeq.Equals(3));
                        if (precio3 == null)
                        {
                            _context.Precios.Add(GetPrecio(3, productosPrecio));
                            break;
                        }
                        if (!precio3.Valor.Equals(productosPrecio.Precio3) && productosPrecio.Precio3 > 0)

                        {
                            precio3.Valor = productosPrecio.Precio3;

                            //_context.Precios.AddOrUpdate(precio3);
                        }
                        else
                        {
                            _context.Entry(new Precio { Id = precio3.Id }).State = EntityState.Deleted;

                        }
                        break;
                }
            }

            await _context.SaveChangesAsync();            
        }

        private Precio GetPrecio(int seq, ProductoPrecioInfoViewModel productosPrecio)
        {
            return new Precio
            {
                NumSeq = seq,
                Valor = seq.Equals(1) ? productosPrecio.Precio1 :
                        seq.Equals(2) ? productosPrecio.Precio2 :
                        seq.Equals(3) ? productosPrecio.Precio3 :
                        0,
                Activo = seq.Equals(1),
                ProductoId = productosPrecio.ProductoId

            };
        }

        public async Task<ProductoPrecioInfoViewModel> GetPrecioViewModel(int idProducto, long idEmpresa)
        {
            var producto = await _productoService.GetById(idProducto,idEmpresa);
            if (producto == null) return null;
            if (!producto.Precios.Any())
                return new ProductoPrecioInfoViewModel
                {
                    Nombre = $"{producto.Version.Nombre} {producto.Version.Nombre}",
                    PrecioBase = producto.PrecioBase,
                    Precio1 = 0,
                    Precio2 = 0,
                    Precio3 = 0,
                    ProductoId = producto.Id
                };

            var precio1 = producto.Precios.SingleOrDefault(x => x.NumSeq.Equals(1));
            var precio2 = producto.Precios.SingleOrDefault(x => x.NumSeq.Equals(2));
            var precio3 = producto.Precios.SingleOrDefault(x => x.NumSeq.Equals(3));

            return new ProductoPrecioInfoViewModel
            {
                Nombre = $"{producto.Version.Nombre} {producto.Version.Nombre}",
                PrecioBase = producto.PrecioBase,
                Precio1 = precio1?.Valor ?? 0,
                Precio2 = precio2?.Valor ?? 0,
                Precio3 = precio3?.Valor ?? 0,
                ProductoId = producto.Id
            };
        }

        public async Task SetSamePrice(SetProductsPriceViewModel priceViewModel, long idEmpresa)
        {
            if (priceViewModel == null) return;

            var products = await _productoService.GetProductListById(priceViewModel.ProductsIdList,idEmpresa);
            if (products != null && products.Any())
            {
                foreach (var product in products)
                {
                    try
                    {
                        product.CodigoBarra = string.IsNullOrEmpty(product.CodigoBarra) ? "NEWCODE":product.CodigoBarra ;
                        product.PrecioBase = priceViewModel.PriceBase;
                        await _productoService.Edit(product);
                        await _context.SaveChangesAsync();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                        
                }
            }
            
        }

        public async Task<bool> SetPriceByNumber(int priceNumber, long idEmpresa)
        {
            try
            {
                var products = await _productoService.GetProductoWithPrice(priceNumber,idEmpresa);
                foreach (var product in products)
                {
                    var precio = product.Precios.SingleOrDefault(x => x.NumSeq == priceNumber);
                    if (precio != null)
                        product.PrecioBase = precio.Valor;

                    product.CodigoBarra = "TEST";
                    try
                    {

                        await Task.Run(async () => await _productoService.Edit(product));
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);

                        throw;
                    }

                    //await _productoService.Edit(product);
                }

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }
    }
}