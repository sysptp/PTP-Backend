using AutoMapper;
using BussinessLayer.DTOs.ModuloInventario.Precios;
using BussinessLayer.Interfaces.ModuloInventario.Precios;
using BussinessLayer.Interfaces.ModuloInventario.Productos;
using DataLayer.Models.ModuloInventario.Precios;
using DataLayer.PDbContex;
using Microsoft.EntityFrameworkCore;

public class PrecioService : IPrecioService
{
    #region Propiedades
    private readonly PDbContext _context;
    private readonly IMapper _mapper;
    private readonly IProductoService _productoService;
    private readonly ITokenService _tokenService;

    public PrecioService(
        PDbContext dbContext,
        IMapper mapper,
        ITokenService tokenService,
        IProductoService productoService)
    {
        _context = dbContext;
        _mapper = mapper;
        _tokenService = tokenService;
        _productoService = productoService;
    }
    #endregion

    // Obtener precios por ID de producto
    public async Task<ViewPreciosDto> GetPriceById(int id)
    {
        var data = await _context.Precios
            .Where(x => x.Id == id)
            .Include(x => x.Producto)
            .Include(x => x.Moneda)
            .Where(x => x.Borrado == false)
            .FirstOrDefaultAsync();

        return _mapper.Map<ViewPreciosDto>(data);
    }

    // Obtener precios por ID de producto
    public async Task<List<ViewPreciosDto>> GetPricesByIdProduct(int idProduct)
    {
        var data = await _context.Precios
            .Where(x => x.IdProducto == idProduct)
            .Include(x => x.Producto)
            .Include(x => x.Moneda)
            .Where(x => x.Borrado == false)
            .ToListAsync();

        return _mapper.Map<List<ViewPreciosDto>>(data);
    }

    // Crear un nuevo precio
    public async Task<int?> CreatePrices(CreatePreciosDto productosPrecio)
    {
        var newPrice = _mapper.Map<Precio>(productosPrecio);
        newPrice.FechaCreacion = DateTime.Now;
        newPrice.Borrado = false;
        newPrice.UsuarioCreacion = _tokenService.GetClaimValue("sub") ?? "UsuarioDesconocido";
        newPrice.HabilitarVenta = false;

        _context.Precios.Add(newPrice);
        await _context.SaveChangesAsync();

        return newPrice.Id;
    }

    // Editar precio existente
    public async Task EditPrice(EditPricesDto price)
    {
        var existingPrice = await _context.Precios.FirstOrDefaultAsync(x => x.Id == price.Id);

        if (existingPrice != null)
        {
            _mapper.Map(price, existingPrice);
            existingPrice.UsuarioModificacion = _tokenService.GetClaimValue("sub") ?? "UsuarioDesconocido";
            existingPrice.FechaModificacion = DateTime.Now;
            _context.Precios.Update(existingPrice);
            await _context.SaveChangesAsync();
        }
    }

    // Establecer el mismo valor de precio para una lista de precios
    public async Task SetSamePrice(List<EditPricesDto> prices, decimal newPrice)
    {
        foreach (var priceDto in prices)
        {
            priceDto.PrecioValor = newPrice;
            await EditPrice(priceDto);
        }
    }

    // Servicio para eliminar precio por id unico
    public async Task DeletePriceById(int id)
    {
        var precio = await GetPriceById(id);

        if (precio != null)
        {
            precio.Borrado = true;
            precio.HabilitarVenta = false;
            var updated = _mapper.Map<Precio>(precio);
            _context.Update(updated);
            await _context.SaveChangesAsync();
        }
    }
}
