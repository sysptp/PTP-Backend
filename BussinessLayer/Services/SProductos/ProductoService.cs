using Microsoft.EntityFrameworkCore;
using BussinessLayer.Interface.IProductos;
using BussinessLayer.ViewModels;
using DataLayer.Models.Productos;
using DataLayer.PDbContex;
using BussinessLayer.DTOs.Productos;
using static Dapper.SqlMapper;

namespace BussinessLayer.Services.SProductos
{
    public class ProductoService : IProductoService
    {
        private readonly PDbContext _context;

        public ProductoService(PDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task CreateProduct(CrearProductoDto producto)
        {
            var newProducto = new Producto
            {
                EsLote = producto.EsLote,
                CantidadLote = producto.CantidadLote ?? 0,
                Descripcion = producto.Descripcion,
                Codigo = producto.Codigo,
                CantidadMinima = producto.CantidadMinima,
                CodigoBarra = producto.CodigoBarra,
                IdEnvase = producto.EnvaseId,
                IdVersion = producto.VersionId,
                CantidadInventario = producto.CantidadInventario,
                PrecioBase = producto.PrecioBase,
                PrecioCompra = producto.PrecioCompra,
                IdEmpresa = producto.IdEmpresa,
                EsProducto = producto.EsProducto,
                HabilitaVenta = producto.HabilitaVenta,
                AdmiteDescuento = producto.AdmiteDescuento,
                Imagen = producto.Imagen,
                AplicaImp = producto.AplicaImp,
                ValorImpuesto = producto.ValorImpuesto
            };

            _context.Productos.Add(newProducto);
            await _context.SaveChangesAsync();
        }





        
        ////public ProductoInfoViewModel GetInfoViewModel(Producto producto, long idEMpresa)
        //{
        //    if (producto == null) return null;
        //    return new ProductoInfoViewModel
        //    {
        //        Id = producto.Id,
        //        Version = producto.Version.Nombre,
        //        Marca = producto.Version.Marca.Nombre,
        //        Descuentos = producto.Descuentos,
        //        Activo = producto.Activo,
        //        Imagenes = producto.Imagenes,
        //        Descripcion = producto.Descripcion,
        //        Codigo = producto.Codigo,
        //        CantidadLote = producto.CantidadLote,
        //        FechaCreacion = producto.FechaCreacion,
        //        EsLote = producto.EsLote,
        //        Envase = producto.Envase.Descripcion,
        //        FechaEdicion = producto.FechaModificacion,
        //        Stock = producto.CantidadInventario,
        //        CantidadMinima = producto.CantidadMinima,
        //        PrecioCompra = producto.PrecioCompra,
        //        IdEmpresa=producto.IdEmpresa
        //    };
        //}

        public async Task<List<ProductoInfoViewModel>> GetInfoViewModelList(long idEMpresa) =>
            ProductoToInfoViewModel(await GetAll(idEMpresa));
        public async Task<List<ProductoInfoViewModel>> GetInfoViewModelListAgotado(long idEMpresa) =>
           ProductoToInfoViewModel(await GetAllAgotados(idEMpresa));
        public CrearProductoDto GetCreateViewModel(Producto producto, long idEMpresa)
        {
            if (producto == null) return null;
            return new CrearProductoDto
            {
                Descuentos = producto.Descuentos,
                Descripcion = producto.Descripcion,
                Codigo = producto.Codigo,
                Imagenes = producto.Imagenes,
                VersionId = producto.IdVersion,
                EnvaseId = producto.IdEnvase,
                CantidadLote = producto.CantidadLote,
                EsLote = producto.EsLote,
                MarcaId = producto.Version.IdMarca,
                Id = producto.Id,
                Activo = producto.Activo,
                CantidadInventario = producto.CantidadInventario,
                PrecioBase = producto.PrecioBase,
                CantidadMinima = producto.CantidadMinima,
                CodigoBarra = producto.CodigoBarra,
                PrecioCompra = producto.PrecioCompra,
                HabilitaVenta = producto.HabilitaVenta,
                AdmiteDescuento = producto.AdmiteDescuento,
                AplicaImp  = producto.AplicaImp,
                ValorImpuesto = producto.ValorImpuesto,
                Imagen=producto.Imagen,
                EsProducto = producto.EsProducto
            };
        }

        public async Task<bool> CheckCodeExist(string productCode)
        {

            return await _context.Productos.AnyAsync(x => x.Codigo.Equals(productCode));
            
        }

        public async Task<List<Producto>> GetProductoWithPrice(int priceNumber, long idEMpresa)
        {
            var products = await _context.Productos
                .Where(x => x.Borrado != true && (x.Precios.Any(z => z.NumSeq == priceNumber)))
                .Include(x => x.Version.Marca)
                .Include(x => x.Envase)
                .Include(x => x.Imagenes)
                .Include(x => x.Descuentos)
                .Include(x => x.Precios)
                .OrderBy(x => x.IdVersion)
                .ToListAsync();

            return products;
        }

        public async Task<List<ProductoInfoViewModel>> GetProductoBySuplidor(int idSuplidor, long idEMpresa)
        {
            return ProductoToInfoViewModel(await GetAll(idEMpresa));

        }

        public Producto GetProductoFromViewModel(CrearProductoDto producto, long idEMpresa)
        {       
            return new Producto
            {
                CodigoBarra = producto.CodigoBarra,
                Id = producto.Id ?? 0,
                PrecioBase = producto.PrecioBase,
             // CantidadInventario = producto.CantidadInventario,
                CantidadLote = producto.CantidadLote ?? 0,
                CantidadMinima = producto.CantidadMinima,
                Codigo = producto.Codigo,
                Descripcion = producto.Descripcion,
                IdEnvase = producto.EnvaseId,
                EsLote = producto.EsLote,
                IdVersion = producto.VersionId,
                PrecioCompra = producto.PrecioCompra,
                EsProducto = producto.EsProducto,
                HabilitaVenta = producto.HabilitaVenta,
                AdmiteDescuento = producto.AdmiteDescuento,
                Imagen  = producto.Imagen,
                AplicaImp=producto.AplicaImp,
                ValorImpuesto = producto.ValorImpuesto

            };
        }

        public async Task<List<Producto>> GetProductListById(int[] productsIdList, long idEMpresa)
        {
            return await _context.Productos
                .Where(x => x.Borrado != true && productsIdList.Any(list => list.Equals(x.Id)))
                .Include(x => x.Version.Marca)
                .Include(x => x.Envase)
                .Include(x => x.Imagenes)
                .Include(x => x.Descuentos)
                .Include(x => x.Precios)
                .OrderBy(x => x.IdVersion)
                .ToListAsync();      
        }

        public async Task<ProductPhotosViewModel> GetPhotoViewModel(int productId, long idEMpresa)
        {
            var product = await GetById(productId,  idEMpresa);
            if (product == null) return null;
            return new ProductPhotosViewModel
            {
                ProductoId = productId,
                Imagens = product.Imagenes          
            };
        }

        public async Task<bool> SetProductPicture(int productId, string image, long idEMpresa)
        {
            var product = await GetById(productId, idEMpresa);
            if (product == null) return false;

            product.Imagenes.Add(new Imagen
            {
                Ruta = image,
                FechaCreacion = DateTime.Now,
                Descripcion = "Foto",
                FechaModificacion = DateTime.Now
            });

            try
            {
                await Edit(product);

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
            
        }

        public async Task<bool> ChangeProductPicture(int imageId, string image, long idEMpresa)
        {
            var imagen = await _context.ImagenesProducto.FindAsync(imageId);
            if (imagen == null) return false;

            imagen.Ruta = image;

            try
            {
               // _context.ImagenesProducto.AddOrUpdate(imagen);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }

        //public async Task Add(Producto entity)
        //{
        //    try
        //    {
        //        _context.Productos.Add(entity);
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e);
        //        throw;
        //    }  
        //}

        public async Task Edit(Producto entity)
        {
            try
            {
                var oldVersion = await _context.Productos.FindAsync(entity.Id);
                if (oldVersion == null) return;

                SetProductoChanges(new Tuple<Producto, Producto>(oldVersion, entity));
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }      
        }

        private void SetProductoChanges(Tuple<Producto, Producto> products)
        {
            var productToUpdate = products.Item1;
            var productWithChanges = products.Item2;

            productToUpdate.IdVersion = productWithChanges.IdVersion;
            productToUpdate.IdEnvase = productWithChanges.IdEnvase;
            productToUpdate.Codigo = productWithChanges.Codigo;
            productToUpdate.Descripcion = productWithChanges.Descripcion;
            productToUpdate.Activo = productWithChanges.Activo;
            productToUpdate.FechaModificacion = DateTime.Now;
            productToUpdate.EsLote = productWithChanges.EsLote;
            productToUpdate.CantidadLote = productWithChanges.EsLote ? productWithChanges.CantidadLote : 0;
            productToUpdate.CantidadMinima = productWithChanges.CantidadMinima;
            productToUpdate.PrecioBase = productWithChanges.PrecioBase;
            productToUpdate.PrecioCompra = productWithChanges.PrecioCompra;
            productToUpdate.Imagenes = productWithChanges.Imagenes;
            productToUpdate.CodigoBarra=productWithChanges.CodigoBarra;
            productToUpdate.AdmiteDescuento = productWithChanges.AdmiteDescuento;
            productToUpdate.AplicaImp  = productWithChanges.AplicaImp;
            productToUpdate.EsProducto = productWithChanges.EsProducto;
            productToUpdate.HabilitaVenta = productWithChanges.HabilitaVenta;
            productToUpdate.ValorImpuesto = productWithChanges.ValorImpuesto;
            
        }

        public async Task<Producto> GetById(int id, long idEMpresa)
        {
            return await _context.Productos
                .Include(x => x.Version.Marca)
                .Include(x => x.Envase)
                .Include(x => x.Imagenes)
                .Include(x => x.Descuentos)
                .Include(x => x.Precios)
                .SingleOrDefaultAsync(x => x.Id.Equals(id));      
        }

        public async Task<IList<Producto>> GetAll(long idEMpresa)
        {

            return await _context.Productos
                .Where(x => x.Borrado != true && x.IdEmpresa==idEMpresa )
                .Include(x => x.Version.Marca)
                .Include(x => x.Envase)
                .Include(x => x.Imagenes)
                .Include(x => x.Descuentos)
                .Include(x => x.Precios)
                .OrderBy(x => x.IdVersion)
                .ToListAsync();
            
        }

        public async Task<IList<Producto>> GetAllAgotados(long idEMpresa)
        {
            return await _context.Productos
                .Where(x => x.Borrado != true && x.IdEmpresa == idEMpresa && x.CantidadInventario <=0 && x.EsProducto=="P" )
                .Include(x => x.Version.Marca)
                .Include(x => x.Envase)
                .Include(x => x.Imagenes)
                .Include(x => x.Descuentos)
                .Include(x => x.Precios)
                .OrderBy(x => x.IdVersion)
                .ToListAsync();     
        }

        public async Task<IList<Producto>> GetAllFacturacion(long idEMpresa)
        {
            return await _context.Productos
                .Where(x => x.Borrado != true && x.IdEmpresa == idEMpresa && x.HabilitaVenta==true && (x.CantidadInventario >=1 || x.EsProducto=="S"))
                .Include(x => x.Version.Marca)
                .Include(x => x.Envase)
                .Include(x => x.Imagenes)
                .Include(x => x.Descuentos)
                .Include(x => x.Precios)
                .OrderBy(x => x.IdVersion)
                .ToListAsync();     
        }

        public async Task DeleteProducto(ProductoInfoViewModel producto, long idEMpresa)
        {
            await Delete(producto.Id,  idEMpresa);
        }

        public async Task Delete(int id, long idEMpresa)
        {
            var producto = await _context.Productos.Where(x=>x.IdEmpresa==idEMpresa && x.Id==id).FirstOrDefaultAsync();
            if (producto == null) return;

            producto.Borrado = true;
            await _context.SaveChangesAsync();        
        }

        private List<ProductoInfoViewModel> ProductoToInfoViewModel(IList<Producto> productos)
        {
            if (productos == null) return new List<ProductoInfoViewModel>();
            return productos.Select(x => new ProductoInfoViewModel
            {
                Marca = x.Version.Marca.Nombre,
                Version = x.Version.Nombre,
                EsLote = x.EsLote,
                Codigo = x.Codigo,
                Descripcion = x.Descripcion,
                Activo = x.Activo,
                CantidadLote = x.CantidadLote,
                Envase = x.Envase.Descripcion,
                FechaCreacion = x.FechaCreacion,
                FechaEdicion = x.FechaModificacion,
                CantidadMinima = x.CantidadMinima,
                Id = x.Id,
                Stock = x.CantidadInventario,
                NombreCompleto = $"{x.Version.Marca.Nombre} {x.Version.Nombre}",
                PrecioCompra = x.PrecioCompra,
                IdEmpresa=x.IdEmpresa,
                Imagen = x.Imagen,
                HabilitaVenta = x.HabilitaVenta,
                AdmiteDescuento= x.AdmiteDescuento,
                EsProducto = x.EsProducto
            }).ToList();
        }
        
        public async Task<Producto> GetProductoByCB( long idEmpresa, string cb = "")
        {
            return await _context.Productos.
                Include(x=> x.Descuentos).
                Include(x=> x.Imagenes).
                Include(x=> x.Precios).
                Include(x=> x.Envase).
                SingleOrDefaultAsync(x => x.CodigoBarra == cb && x.IdEmpresa== idEmpresa);
        }

        public async Task<Producto> GetProductoByCBFactura(long idEmpresa, string cb = "")
        {

            return await _context.Productos.
                Include(x => x.Descuentos).
                Include(x => x.Imagenes).
                Include(x => x.Precios).
                Include(x => x.Envase).
                SingleOrDefaultAsync(x => x.CodigoBarra == cb && x.IdEmpresa == idEmpresa && x.HabilitaVenta==true && (x.CantidadInventario >= 1 || x.EsProducto == "S"));
        }

        public async Task<Producto> GetByIdDesc(int id)
        {
            return await _context.Productos
                .Include(x => x.Version.Marca)
                .Include(x => x.Envase)
                .Include(x => x.Imagenes)
                .Include(x => x.Descuentos)
                .Include(x => x.Precios)
                .SingleOrDefaultAsync(x => x.Id.Equals(id));                   
        }
    }
}