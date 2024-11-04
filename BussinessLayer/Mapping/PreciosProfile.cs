using AutoMapper;
using BussinessLayer.DTOs.Precios;
using DataLayer.Models.Productos;

namespace BussinessLayer.Mapping
{
    public class PreciosProfile : Profile
    {
        public PreciosProfile()
        {

            CreateMap<CreatePreciosDto, Precio>().ReverseMap();
            CreateMap<Precio, ViewPreciosDto>().ReverseMap();
        }
    }
}
