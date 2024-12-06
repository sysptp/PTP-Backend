
using AutoMapper;
using BussinessLayer.DTOs.ModuloInventario.Almacenes;
using DataLayer.Models.ModuloInventario.Almacen;

namespace BussinessLayer.Mapping.ModuloInventario.Almacen
{
    public class InvInventarioSucursalProfile : Profile
    {
        public InvInventarioSucursalProfile()
        {

            CreateMap<InvInventarioSucursalRequest, InvInventarioSucursal>().ReverseMap();

            CreateMap<InvInventarioSucursalReponse, InvInventarioSucursal>().ReverseMap();

        }
    }
}

