using AutoMapper;
using BussinessLayer.DTOs.ModuloInventario.Almacenes;
using DataLayer.Models.ModuloInventario.Almacen;

namespace BussinessLayer.Mapping.ModuloInventario.Almacen
{
    public class InvAlmacenesProfile : Profile
    {
        public InvAlmacenesProfile()
        {

            CreateMap<InvAlmacenesRequest, InvAlmacenes>().ReverseMap();

            CreateMap<InvAlmacenesReponse, InvAlmacenes>().ReverseMap();



        }
    }
}
