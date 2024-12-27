using AutoMapper;
using BussinessLayer.DTOs.ModuloInventario.Almacenes;
using DataLayer.Models.ModuloInventario.Almacen;

namespace BussinessLayer.Mapping.ModuloInventario.Almacen
{
    internal class InvMovimientoSucursalDetalleProfile : Profile
    {
        public InvMovimientoSucursalDetalleProfile()
        {

            CreateMap<InvMovimientoSucursalDetalleRequest, InvMovimientoSucursalDetalle>().ReverseMap();

            CreateMap<InvMovimientoSucursalDetalleReponse, InvMovimientoSucursalDetalle>().ReverseMap();

        }
    }

}