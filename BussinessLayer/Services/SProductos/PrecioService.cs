using AutoMapper;
using BussinessLayer.DTOs.Precios;
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
        private readonly IMapper _mapper;

        public PrecioService(
            PDbContext dbContext,
            IProductoService productoService,
            IMapper mapper)
        {
            _productoService = productoService;
            _context = dbContext;
            _mapper = mapper;
        }

        // Obtener precios por ID de producto
        public async Task<List<ViewPreciosDto>> GetPricesByIdProduct(int idProduct)
        {
            var data = await _context.Precios
                .Where(x => x.ProductoId == idProduct)
                .Include(x => x.Producto)
                .ToListAsync();

            return _mapper.Map<List<ViewPreciosDto>>(data);
        }

        // Crear un nuevo precio
        public async Task CreatePrices(CreatePreciosDto productosPrecio)
        {
            var newPrice = _mapper.Map<Precio>(productosPrecio);

            _context.Precios.Add(newPrice);
            await _context.SaveChangesAsync();
        }

        // Editar precio existente
        public async Task EditPrice(ViewPreciosDto price)
        {
            var existingPrice = await _context.Precios.FirstOrDefaultAsync(x => x.Id == price.Id);

            if (existingPrice != null)
            {
                _mapper.Map(price, existingPrice);  // Map only if the price exists
                await _context.SaveChangesAsync();   // Save changes without redundant Update() call
            }
        }

        // Establecer el mismo valor de precio para una lista de precios
        public async Task SetSamePrice(List<ViewPreciosDto> prices, decimal newPrice)
        {
            foreach (var priceDto in prices)
            {
                priceDto.Valor = newPrice;
                await EditPrice(priceDto);
            }
        }
    }
}