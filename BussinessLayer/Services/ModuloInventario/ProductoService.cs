using Microsoft.EntityFrameworkCore;
using DataLayer.PDbContex;
using AutoMapper;
using DataLayer.Models.ModuloInventario;
using BussinessLayer.Interfaces.ModuloInventario;
using BussinessLayer.DTOs.ModuloInventario;

namespace BussinessLayer.Services.ModuloInventario
{
    public class ProductoService : IProductoService
    {
        private readonly IMapper _mapper;
        private readonly PDbContext _context;
        private readonly ITokenService _tokenService;

        public ProductoService(PDbContext dbContext,
            IMapper mapper, ITokenService tokenService)
        {
            _context = dbContext;
            _mapper = mapper;
            _tokenService = tokenService;
        }   

        // Servicio para crear un producto
        public async Task<CreateProductsDto> CreateProduct(CreateProductsDto producto)
        {
            var newProduct = _mapper.Map<Producto>(producto);

            newProduct.FechaCreacion = DateTime.Now;
            newProduct.Borrado = false;
            newProduct.UsuarioCreacion = _tokenService.GetClaimValue("sub") ?? "UsuarioDesconocido";
            newProduct.Borrado = false;
            newProduct.Activo = true;

            _context.Productos.Add(newProduct);
            await _context.SaveChangesAsync();

            return producto;
        }

        // Servicio para listar todos los productos
        public async Task<List<ViewProductsDto>> GetProducts()
        {
            var list = await _context.Productos
                .Include(x => x.Version)
                .Include(x => x.Version.Marca)
                .Include(x => x.InvProductoImagenes)
                .Include(x => x.InvProductoImpuestos)
                .Include(x => x.InvProductoSuplidores)
                .ToListAsync();

            return _mapper.Map<List<ViewProductsDto>>(list);
        }

        // Servicio para listar todos los productos por empresa
        public async Task<List<ViewProductsDto>> GetProductByIdCompany(long idCompany)
        {
            var list = await _context.Productos
                .Include(x => x.Version)
                .Include(x => x.Version.Marca)
                .Include(x => x.TipoProducto)
                .Include(x => x.InvProductoImagenes)
                .Include(x => x.InvProductoImpuestos)
                .Include(x => x.InvProductoSuplidores)
                .Where(x => x.IdEmpresa == idCompany).ToListAsync();

            return _mapper.Map<List<ViewProductsDto>>(list);   
        }

        // Servicio para obtener productos por id
        public async Task<ViewProductsDto> GetProductById(int idProduct)
        {
            var product = await _context.Productos
                .Include(x => x.Version)
                .Include(x => x.Version.Marca)
                .Include(x => x.InvProductoImagenes)
                .Include(x => x.InvProductoImpuestos)
                .Include(x => x.InvProductoSuplidores)
                .FirstOrDefaultAsync(x => x.Id == idProduct);

            return _mapper.Map<ViewProductsDto>(product);
        }

        // Servicio obtener producto por código dentro de una empresa
        public async Task<ViewProductsDto> GetProductByCodeInCompany(string codeProduct, long idEmpresa)
        {
            var product = await _context.Productos
                .Include(x => x.Version)
                .Include(x => x.Version.Marca)
                .Include(x => x.InvProductoImagenes)
                .Include(x => x.InvProductoImpuestos)
                .Include(x => x.InvProductoSuplidores)
                .FirstOrDefaultAsync(x => x.Codigo == codeProduct && x.IdEmpresa == idEmpresa);

            return _mapper.Map<ViewProductsDto>(product);
        }

        // Servicio para eliminar producto por id unico
        public async Task DeleteProductById(int id)
        {
            var producto = await GetProductById(id);

            if (producto != null)
            {
                producto.Borrado = true;
                _context.Update(producto);
                await _context.SaveChangesAsync();
            }
        }

        // Servicio para eliminar producto por codigo dentro de una empresa
        public async Task DeleteProductByCodigo(string codigo, long idEmpresa)
        {
            var producto = await _context.Productos
                .FirstOrDefaultAsync(x => x.IdEmpresa == idEmpresa && x.Codigo == codigo);

            if (producto != null)
            {
                producto.Borrado = true;
                _context.Update(producto);
                await _context.SaveChangesAsync();
            }
        }

        // Verificar si ya existe un código de producto
        public async Task<bool> CheckCodeExist(string productCode)
        {
            return await _context.Productos
                .AnyAsync(x => x.Codigo.Equals(productCode));
        }

        // Servicio para editar un producto
        public async Task EditProduct(ViewProductsDto producto)
        {
            var existingProduct = await _context.Productos
                .FirstOrDefaultAsync(x => x.Id == producto.Id);

            if (existingProduct != null)
            {
                _mapper.Map(producto, existingProduct);
                _context.Productos.Update(existingProduct);
                await _context.SaveChangesAsync();
            }
        }

        // Obtener productos facturados
        public async Task<List<ViewProductsDto>> GetAllFacturacion(long idEmpresa)
        {
            var productos = await _context.Productos
                .Where(x => x.Borrado != true && x.IdEmpresa == idEmpresa 
                && x.HabilitaVenta == true 
                && (x.CantidadInventario >= 1 || x.EsProducto == true))
                    .ToListAsync();

            return _mapper.Map<List<ViewProductsDto>>(productos);
        }

        // Obtener producto por el código de barra
        public async Task<ViewProductsDto> GetProductoByBarCode(long idEmpresa, string codigoBarra)
        {
            var data = await _context.Productos
                .SingleOrDefaultAsync(x => x.CodigoBarra == codigoBarra 
                && x.IdEmpresa == idEmpresa);

            return _mapper.Map<ViewProductsDto>(data);
        }

        // Servicio para obtener productos por el código de barra de la factura
        public async Task<ViewProductsDto> GetProductoByBarCodeFactura(long idEmpresa, string codigoBarra)
        {
            var data = await _context.Productos
                .SingleOrDefaultAsync(x => x.CodigoBarra == codigoBarra 
                && x.IdEmpresa == idEmpresa 
                && x.HabilitaVenta == true 
                && (x.CantidadInventario >= 1 || x.EsProducto == true));

            return _mapper.Map<ViewProductsDto>(data);
        }

        // Servicio para obtener todos los productos agotados
        public async Task<List<ViewProductsDto>> GetAllAgotados(long idEmpresa)
        {
            var productos = await _context.Productos
                .Where(x => x.Borrado != true && x.IdEmpresa == idEmpresa 
                    && x.CantidadInventario <= 0 
                    && x.EsProducto == true)
                    .ToListAsync();

            return _mapper.Map<List<ViewProductsDto>>(productos);
        }
        

        // servicio para obtener productos por suplidores    
    }
}