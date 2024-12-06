
using AutoMapper;
using BussinessLayer.DTOs.ModuloInventario.Almacenes;
using DataLayer.Models.ModuloInventario.Almacen;

namespace BussinessLayer.Mapping.ModuloInventario.Almacen
{
    public class InvMovAlmacenSucursalProfile : Profile
    {
        public InvMovAlmacenSucursalProfile()
        {

            CreateMap<InvMovAlmacenSucursalRequest, InvMovAlmacenSucursal>().ReverseMap();

            CreateMap<InvMovAlmacenSucursalReponse, InvMovAlmacenSucursal>().ReverseMap();

        }
    }
}
