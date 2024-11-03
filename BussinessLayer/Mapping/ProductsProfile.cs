using AutoMapper;
using BussinessLayer.DTOs.Precios;
using BussinessLayer.DTOs.Productos;
using DataLayer.Models.Productos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.Mapping
{
    public class ProductsProfile : Profile
    {
        public ProductsProfile() {

            CreateMap<CreateProductsDto, Producto>().ReverseMap();
            CreateMap<Producto, ViewProductsDto>();
        }
    }
}
