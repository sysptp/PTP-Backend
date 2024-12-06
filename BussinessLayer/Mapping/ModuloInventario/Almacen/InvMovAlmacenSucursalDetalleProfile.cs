using BussinessLayer.DTOs.Auditoria;
using DataLayer.Models.Auditoria;
using AutoMapper;
using BussinessLayer.DTOs.ModuloInventario.Almacenes;
using DataLayer.Models.ModuloInventario.Almacen;

namespace BussinessLayer.Mapping.ModuloInventario.Almacen
{
    public class InvMovAlmacenSucursalDetalleProfile : Profile
    {
        public InvMovAlmacenSucursalDetalleProfile()
        {

            CreateMap<InvMovAlmacenSucursalRequest, InvMovAlmacenSucursal>().ReverseMap();

            CreateMap<InvMovAlmacenSucursalReponse, InvMovAlmacenSucursal>().ReverseMap();

        }
    }

}