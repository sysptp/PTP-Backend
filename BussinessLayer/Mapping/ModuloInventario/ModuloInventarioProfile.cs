using AutoMapper;
using BussinessLayer.DTOs.ModuloInventario.Precios;
using BussinessLayer.DTOs.ModuloInventario.Productos;
using BussinessLayer.DTOs.ModuloInventario.Versiones;
using DataLayer.Models.ModuloInventario.Precios;
using DataLayer.Models.ModuloInventario.Productos;
using DataLayer.Models.ModuloInventario.Version;

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

        CreateMap<CreateVersionesDto, Versiones>().ReverseMap();
    }
}

