
using AutoMapper;
using BussinessLayer.DTOs.ModuloInventario.Almacenes;
using DataLayer.Models.ModuloInventario.Almacen;

namespace BussinessLayer.Mapping.ModuloInventario.Almacen
{
    public class InvAlmacenInventarioProfile : Profile
    {
        public InvAlmacenInventarioProfile()
        {

            CreateMap<InvAlmacenInventarioRequest, InvAlmacenInventario>().ReverseMap();

            CreateMap<InvAlmacenInventarioReponse, InvAlmacenInventario>().ReverseMap();

        }
    }
}
