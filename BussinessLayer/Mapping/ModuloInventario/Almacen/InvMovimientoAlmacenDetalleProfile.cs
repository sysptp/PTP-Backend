using AutoMapper;
using BussinessLayer.DTOs.ModuloInventario.Almacenes;
using DataLayer.Models.ModuloInventario.Almacen;

namespace BussinessLayer.Mapping.ModuloInventario.Almacen
{
    internal class InvMovimientoAlmacenDetalleProfile : Profile
    {
        public InvMovimientoAlmacenDetalleProfile()
        {

            CreateMap<InvMovimientoAlmacenDetalleRequest, InvMovimientoAlmacenDetalle>().ReverseMap();

            CreateMap<InvMovimientoAlmacenDetalleReponse, InvMovimientoAlmacenDetalle>().ReverseMap();

        }
    }
}
