using AutoMapper;
using BussinessLayer.DTOs.ModuloHelpDesk;
using DataLayer.Models.ModuloHelpDesk;

namespace BussinessLayer.Mapping.ModuloHelpDesk
{
    public class HdkDepartamentsProfile : Profile
    {
        public HdkDepartamentsProfile()
        {
            CreateMap<HdkDepartamentsRequest, HdkDepartaments>()
           .ReverseMap();

            CreateMap<HdkDepartaments,HdkDepartamentsReponse>()
            .ForPath(dest => dest.NombreEmpresa, opt => opt.MapFrom(src => src.GnEmpresa.NOMBRE_EMP)).ReverseMap();
        }
    }
}
