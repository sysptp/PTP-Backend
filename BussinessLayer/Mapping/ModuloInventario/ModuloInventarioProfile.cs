using AutoMapper;
using BussinessLayer.DTOs.ModuloInventario.Descuentos;
using BussinessLayer.DTOs.ModuloInventario.Impuestos;
using BussinessLayer.DTOs.ModuloInventario.Marcas;
using BussinessLayer.DTOs.ModuloInventario.Otros;
using BussinessLayer.DTOs.ModuloInventario.Pedidos;
using BussinessLayer.DTOs.ModuloInventario.Precios;
using BussinessLayer.DTOs.ModuloInventario.Productos;
using BussinessLayer.DTOs.ModuloInventario.Suplidores;
using BussinessLayer.DTOs.ModuloInventario.Versiones;
using DataLayer.Models.ModuloInventario.Descuento;
using DataLayer.Models.ModuloInventario.Impuesto;
using DataLayer.Models.ModuloInventario.Marcas;
using DataLayer.Models.ModuloInventario.Otros;
using DataLayer.Models.ModuloInventario.Pedidos;
using DataLayer.Models.ModuloInventario.Precios;
using DataLayer.Models.ModuloInventario.Productos;
using DataLayer.Models.Otros;

public class ModuloInventarioProfile : Profile
{
    public ModuloInventarioProfile()
    {

        CreateMap<CreatePreciosDto, Precio>().ReverseMap();
        CreateMap<Precio, ViewPreciosDto>().ReverseMap();
        CreateMap<EditPricesDto, Precio>().ReverseMap();

        CreateMap<CreateProductsDto, Producto>().ReverseMap();
        CreateMap<EditProductDto, Producto>().ReverseMap();
        CreateMap<Producto, ViewProductsDto>().ReverseMap();

        CreateMap<CreateTipoProductoDto, InvTipoProducto>().ReverseMap();
        CreateMap<ViewProductTypeDto, InvTipoProducto>().ReverseMap();
        CreateMap<EditProductTypeDto, InvTipoProducto>().ReverseMap();

        CreateMap<CreateBrandDto, InvMarcas>().ReverseMap();
        CreateMap<ViewBrandDto, InvMarcas>().ReverseMap();
        CreateMap<EditBrandDto, InvMarcas>().ReverseMap();

        CreateMap<CreateVersionsDto, Versiones>().ReverseMap();
        CreateMap<EditVersionsDto, Versiones>().ReverseMap();
        CreateMap<ViewVersionsDto, Versiones>().ReverseMap();

        CreateMap<CreateTaxDto, InvImpuestos>().ReverseMap();
        CreateMap<EditTaxDto, InvImpuestos>().ReverseMap();
        CreateMap<ViewTaxDto, InvImpuestos>().ReverseMap();

        CreateMap<CreateDiscountDto, Descuentos>().ReverseMap();
        CreateMap<EditDiscountDto, Descuentos>().ReverseMap();
        CreateMap<ViewDiscountDto, Descuentos>().ReverseMap();

        CreateMap<CreateOrderDto, Pedido>().ReverseMap();
        CreateMap<EditOrderDto, Pedido>().ReverseMap();
        CreateMap<ViewOrderDto, Pedido>().ReverseMap();

        CreateMap<CreateSuppliersDto, Suplidores>().ReverseMap();
        CreateMap<EditSuppliersDto, Suplidores>().ReverseMap();
        CreateMap<ViewSuppliersDto, Suplidores>().ReverseMap();

        CreateMap<CreateInvProductoSuplidorDTO, InvProductoSuplidor>().ReverseMap();
        CreateMap<EditInvProductoSuplidorDTO, InvProductoSuplidor>().ReverseMap();
        CreateMap<ViewInvProductoSuplidorDTO, InvProductoSuplidor>().ReverseMap();

        CreateMap<CreateContactosSuplidoresDto, ContactosSuplidores>().ReverseMap();
        CreateMap<EditContactosSuplidoresDto, ContactosSuplidores>().ReverseMap();
        CreateMap<ViewContactosSuplidoresDto, ContactosSuplidores>().ReverseMap();

        CreateMap<CreateTipoMovimientoDto, TipoMovimiento>().ReverseMap();
        CreateMap<EditTipoMovimientoDto, TipoMovimiento>().ReverseMap();
        CreateMap<ViewTipoMovimientoDto, TipoMovimiento>().ReverseMap();

        CreateMap<CreateDetallePedidoDto, DetallePedido>().ReverseMap();
        CreateMap<EditDetallePedidoDto, DetallePedido>().ReverseMap();
        CreateMap<ViewDetallePedidoDto, DetallePedido>().ReverseMap();

        CreateMap<CreateInvProductoImpuestoDto, InvProductoImpuesto>().ReverseMap();
        CreateMap<EditInvProductoImpuestoDto, InvProductoImpuesto>().ReverseMap();
        CreateMap<ViewInvProductoImpuestoDto, InvProductoImpuesto>().ReverseMap();
    }
}
