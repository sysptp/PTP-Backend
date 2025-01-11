using AutoMapper;
using DataLayer.Models.ModuloHelpDesk;
using BussinessLayer.DTOs.ModuloHelpDesk;

namespace BussinessLayer.Mapping.ModuloHelpDesk
{
    public class HdkTicketsProfile : Profile
    {
        public HdkTicketsProfile()
        {

            CreateMap<HdkTicketsRequest, HdkTickets>()
              .ReverseMap();

            CreateMap<HdkTicketsReponse, HdkTickets>()
              .ForMember(dest => dest.GnEmpresa.NOMBRE_EMP, opt => opt.MapFrom(src => src.NombreEmpresa))
              .ForMember(dest => dest.HdkTypeTicket.Descripcion, opt => opt.MapFrom(src => src.TipoTicket))
              .ForMember(dest => dest.HdkDepartaments.Descripcion, opt => opt.MapFrom(src => src.Departamento))
              .ForMember(dest => dest.HdkCategoryTicket.Descripcion, opt => opt.MapFrom(src => src.Categoria))
              .ForMember(dest => dest.HdkSubCategory.Descripcion, opt => opt.MapFrom(src => src.SubCategoria))
              .ForMember(dest => dest.HdkErrorSubCategory.Descripcion, opt => opt.MapFrom(src => src.SubCategoria))
              .ForMember(dest => dest.HdkStatusTicket.Descripcion, opt => opt.MapFrom(src => src.Estado))
              .ForMember(dest => dest.HdkSolutionTicket.Descripcion, opt => opt.MapFrom(src => src.Solucion))
              .ForMember(dest => dest.HdkPrioridadTicket.Descripcion, opt => opt.MapFrom(src => src.Prioridad))
              .ReverseMap();
        }
    }
}

