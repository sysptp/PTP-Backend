using AutoMapper;
using BussinessLayer.DTOs.ModuloInventario;
using BussinessLayer.DTOs.Precios;
using DataLayer.Models.ModuloInventario;

namespace BussinessLayer.Mapping.ModuloInventario
{
    public class ModuloInventarioProfile : Profile
    {
        public ModuloInventarioProfile()
        {

            CreateMap<CreatePreciosDto, Precio>().ReverseMap();
            CreateMap<Precio, ViewPreciosDto>().ReverseMap();

            CreateMap<CreateProductsDto, Producto>().ReverseMap();
            CreateMap<EditProductDto, Producto>().ReverseMap();
            CreateMap<Producto, ViewProductsDto>().ReverseMap();

            CreateMap<CreateVersionesDto, Versiones>().ReverseMap();
        }
    }
}
