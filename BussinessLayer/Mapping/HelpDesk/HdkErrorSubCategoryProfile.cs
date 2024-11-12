using BussinessLayer.DTOs.HelpDesk;
using DataLayer.Models.HelpDesk;
using AutoMapper;

namespace BussinessLayer.Mapping.HeplDesk
{
    public class HdkErrorSubCategoryProfile : Profile
    {
        public HdkErrorSubCategoryProfile()
        {
            CreateMap<HdkErrorSubCategoryRequest, HdkErrorSubCategory>()
           .ForMember(dest => dest.IdSubCategory, opt => opt.MapFrom(src => src.IdSubCategory))
           .ForMember(dest => dest.Descripcion, opt => opt.MapFrom(src => src.Descripcion))
           .ForMember(dest => dest.IdEmpresa, opt => opt.MapFrom(src => src.IdEmpresa))
           .ReverseMap();

            CreateMap<HdkErrorSubCategoryReponse, HdkErrorSubCategory>()
            .ForMember(dest => dest.IdErroSubCategory, opt => opt.MapFrom(src => src.IdErroSubCategory))
            .ForMember(dest => dest.IdSubCategory, opt => opt.MapFrom(src => src.IdSubCategory))
            .ForMember(dest => dest.Descripcion, opt => opt.MapFrom(src => src.Descripcion))     
            .ForMember(dest => dest.FechaAdicion, opt => opt.MapFrom(src => src.FechaAdicion))
            .ForMember(dest => dest.FechaModificacion, opt => opt.MapFrom(src => src.FechaModificacion))
            .ForMember(dest => dest.UsuarioAdicion, opt => opt.MapFrom(src => src.UsuarioAdicion))
            .ForMember(dest => dest.UsuarioModificacion, opt => opt.MapFrom(src => src.UsuarioModificacion))
            .ForMember(dest => dest.Borrado, opt => opt.MapFrom(src => src.Borrado))
            .ForMember(dest => dest.IdEmpresa, opt => opt.MapFrom(src => src.IdEmpresa)).ReverseMap();

        }
    }
}
