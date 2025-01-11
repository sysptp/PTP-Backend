using AutoMapper;
using DataLayer.Models.ModuloHelpDesk;
using BussinessLayer.DTOs.ModuloHelpDesk;


namespace BussinessLayer.Mapping.ModuloHelpDesk
{
    public class HdkSolutionTicketProfile : Profile
    {
        public HdkSolutionTicketProfile()
        {

            CreateMap<HdkSolutionTicketRequest, HdkSolutionTicket>()
              .ForMember(dest => dest.Descripcion, opt => opt.MapFrom(src => src.Descripcion))
              .ForMember(dest => dest.IdEmpresa, opt => opt.MapFrom(src => src.IdEmpresa))
              .ReverseMap();

            CreateMap<HdkSolutionTicketReponse, HdkSolutionTicket>()
            .ForMember(dest => dest.IdSolution, opt => opt.MapFrom(src => src.IdSolution))
            .ForMember(dest => dest.Descripcion, opt => opt.MapFrom(src => src.Descripcion))
            .ForMember(dest => dest.FechaAdicion, opt => opt.MapFrom(src => src.FechaAdicion))
            .ForMember(dest => dest.FechaModificacion, opt => opt.MapFrom(src => src.FechaModificacion))
            .ForMember(dest => dest.UsuarioAdicion, opt => opt.MapFrom(src => src.UsuarioAdicion))
            .ForMember(dest => dest.UsuarioModificacion, opt => opt.MapFrom(src => src.UsuarioModificacion))
            .ForMember(dest => dest.Borrado, opt => opt.MapFrom(src => src.Borrado))
            .ForMember(dest => dest.IdEmpresa, opt => opt.MapFrom(src => src.IdEmpresa))
            .ForMember(dest => dest.GnEmpresa.NOMBRE_EMP, opt => opt.MapFrom(src => src.NombreEmpresa))
            .ForMember(dest => dest.HdkDepartaments, opt => opt.MapFrom(src => src.HdkDepartamentsReponse))
            .ReverseMap();
        }
    }
}
