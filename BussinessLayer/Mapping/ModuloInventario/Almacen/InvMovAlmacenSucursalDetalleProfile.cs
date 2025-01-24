using AutoMapper;
using BussinessLayer.DTOs.ModuloInventario.Almacenes;
using DataLayer.Models.ModuloInventario.Almacen;

namespace BussinessLayer.Mapping.ModuloInventario.Almacen
{
    public class InvMovAlmacenSucursalDetalleProfile : Profile
    {
        public InvMovAlmacenSucursalDetalleProfile()
        {

            CreateMap<InvMovAlmacenSucursalDetalleRequest, InvMovAlmacenSucursalDetalle>().ReverseMap();

            CreateMap<InvMovAlmacenSucursalDetalleReponse, InvMovAlmacenSucursalDetalle>().ReverseMap();

        }
    }

}