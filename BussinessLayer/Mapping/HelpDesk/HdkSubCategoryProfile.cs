using BussinessLayer.DTOs.HelpDesk;
using DataLayer.Models.HelpDesk;
using AutoMapper;

namespace BussinessLayer.Mapping.HeplDesk
{
    public class HdkSubCategoryProfile: Profile
    {
        public HdkSubCategoryProfile()
        {
            CreateMap<HdkSubCategoryRequest, HdkSubCategory>()
              .ForMember(dest => dest.IdCategory, opt => opt.MapFrom(src => src.IdCategory))
              .ForMember(dest => dest.Descripcion, opt => opt.MapFrom(src => src.Descripcion))
              .ForMember(dest => dest.CantidadHoraSLA, opt => opt.MapFrom(src => src.CantidadHoraSLA))
              .ForMember(dest => dest.EsAsignacionDirecta, opt => opt.MapFrom(src => src.EsAsignacionDirecta))
              .ForMember(dest => dest.IdDepartamento, opt => opt.MapFrom(src => src.IdDepartamento))
              .ForMember(dest => dest.IdUsuarioAsignacion, opt => opt.MapFrom(src => src.IdUsuarioAsignacion))
              .ForMember(dest => dest.IdEmpresa, opt => opt.MapFrom(src => src.IdEmpresa))
              .ReverseMap();

            CreateMap<HdkSubCategoryReponse, HdkSubCategory>()
              .ForMember(dest => dest.IdSubCategory, opt => opt.MapFrom(src => src.IdSubCategory))
              .ForMember(dest => dest.IdCategory, opt => opt.MapFrom(src => src.IdCategory))
              .ForMember(dest => dest.Descripcion, opt => opt.MapFrom(src => src.Descripcion))
              .ForMember(dest => dest.CantidadHoraSLA, opt => opt.MapFrom(src => src.CantidadHoraSLA))
              .ForMember(dest => dest.EsAsignacionDirecta, opt => opt.MapFrom(src => src.EsAsignacionDirecta))
              .ForMember(dest => dest.IdDepartamento, opt => opt.MapFrom(src => src.IdDepartamento))
              .ForMember(dest => dest.IdUsuarioAsignacion, opt => opt.MapFrom(src => src.IdUsuarioAsignacion))
              .ForMember(dest => dest.FechaAdicion, opt => opt.MapFrom(src => src.FechaAdicion))
              .ForMember(dest => dest.FechaModificacion, opt => opt.MapFrom(src => src.FechaModificacion))
              .ForMember(dest => dest.UsuarioAdicion, opt => opt.MapFrom(src => src.UsuarioAdicion))
              .ForMember(dest => dest.UsuarioModificacion, opt => opt.MapFrom(src => src.UsuarioModificacion))
              .ForMember(dest => dest.Borrado, opt => opt.MapFrom(src => src.Borrado))
              .ForMember(dest => dest.IdEmpresa, opt => opt.MapFrom(src => src.IdEmpresa)).ReverseMap();
        }
    }
}
