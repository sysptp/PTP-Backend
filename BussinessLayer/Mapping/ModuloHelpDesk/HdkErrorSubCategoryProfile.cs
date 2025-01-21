using AutoMapper;
using DataLayer.Models.ModuloHelpDesk;
using BussinessLayer.DTOs.ModuloHelpDesk;

namespace BussinessLayer.Mapping.ModuloHelpDesk
{
    public class HdkErrorSubCategoryProfile : Profile
    {
        public HdkErrorSubCategoryProfile()
        {
            CreateMap<HdkErrorSubCategoryRequest, HdkErrorSubCategory>()
           .ReverseMap();

            CreateMap<HdkErrorSubCategoryReponse, HdkErrorSubCategory>()
            .ForPath(dest => dest.GnEmpresa.NOMBRE_EMP, opt => opt.MapFrom(src => src.NombreEmpresa))
            .ForMember(dest => dest.HdkSubCategory, opt => opt.MapFrom(src => src.HdkSubCategoryReponse))
            .ReverseMap();

        }
    }
}
