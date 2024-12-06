using AutoMapper;
using BussinessLayer.DTOs.ModuloInventario.Almacenes;
using DataLayer.Models.ModuloInventario.Almacen;

namespace BussinessLayer.Mapping.ModuloInventario.Almacen
{
    public class InvMovInventarioSucursalProfile : Profile
    {
        public InvMovInventarioSucursalProfile()
        {

            CreateMap<InvMovInventarioSucursalRequest, InvMovInventarioSucursal>().ReverseMap();

            CreateMap<InvMovInventarioSucursalReponse, InvMovInventarioSucursal>().ReverseMap();

        }
    }
}