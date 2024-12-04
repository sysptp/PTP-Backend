using AutoMapper;
using BussinessLayer.DTOs.ModuloGeneral.Imagenes;
using DataLayer.Models.ModuloGeneral.Imagen;

namespace BussinessLayer.Mapping.ModuloGeneral.Imagenes
{
    public class ImagenesProfile : Profile
    {
        public ImagenesProfile() {
            CreateMap<AddImageProductDTO, Imagen>().ReverseMap();

        }
    }
}
