using AutoMapper;
using BussinessLayer.DTOs.ModuloInventario.Descuentos;
using BussinessLayer.DTOs.ModuloInventario.Impuestos;
using BussinessLayer.DTOs.ModuloInventario.Marcas;
using BussinessLayer.DTOs.ModuloInventario.Precios;
using BussinessLayer.DTOs.ModuloInventario.Productos;
using BussinessLayer.DTOs.ModuloInventario.Versiones;
using DataLayer.Models.ModuloInventario.Descuento;
using DataLayer.Models.ModuloInventario.Impuesto;
using DataLayer.Models.ModuloInventario.Marcas;
using DataLayer.Models.ModuloInventario.Precios;
using DataLayer.Models.ModuloInventario.Productos;

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
    }
}
