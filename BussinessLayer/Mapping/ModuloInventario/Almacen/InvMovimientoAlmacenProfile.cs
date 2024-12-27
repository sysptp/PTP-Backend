using AutoMapper;
using BussinessLayer.DTOs.ModuloInventario.Almacenes;
using DataLayer.Models.ModuloInventario.Almacen;

namespace BussinessLayer.Mapping.ModuloInventario.Almacen
{
    public class InvMovimientoAlmacenProfile : Profile
    {
        public InvMovimientoAlmacenProfile()
        {

            CreateMap<InvMovimientoAlmacenRequest, InvMovimientoAlmacen>().ReverseMap();

            CreateMap<InvMovimientoAlmacenReponse, InvMovimientoAlmacen>().ReverseMap();

        }
    }
}
