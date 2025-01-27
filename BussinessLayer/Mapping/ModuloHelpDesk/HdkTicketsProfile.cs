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
              .ForPath(dest => dest.GnEmpresa.NOMBRE_EMP, opt => opt.MapFrom(src => src.NombreEmpresa))
              .ForPath(dest => dest.HdkTypeTicket.Descripcion, opt => opt.MapFrom(src => src.TipoTicket))
              .ForPath(dest => dest.HdkDepartaments.Descripcion, opt => opt.MapFrom(src => src.Departamento))
              .ForPath(dest => dest.HdkCategoryTicket.Descripcion, opt => opt.MapFrom(src => src.Categoria))
              .ForPath(dest => dest.HdkSubCategory.Descripcion, opt => opt.MapFrom(src => src.SubCategoria))
              .ForPath(dest => dest.HdkErrorSubCategory.Descripcion, opt => opt.MapFrom(src => src.SubCategoria))
              .ForPath(dest => dest.HdkStatusTicket.Descripcion, opt => opt.MapFrom(src => src.Estado))
              .ForPath(dest => dest.HdkSolutionTicket.Descripcion, opt => opt.MapFrom(src => src.Solucion))
              .ForPath(dest => dest.HdkPrioridadTicket.Descripcion, opt => opt.MapFrom(src => src.Prioridad))
              .ReverseMap();
        }
    }
}

