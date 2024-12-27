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
              .ForMember(dest => dest.Titulo, opt => opt.MapFrom(src => src.Titulo))
              .ForMember(dest => dest.Descripcion, opt => opt.MapFrom(src => src.Descripcion))
              .ForMember(dest => dest.IdUsuarioAsignado, opt => opt.MapFrom(src => src.IdUsuarioAsignado))
              .ForMember(dest => dest.idTicketPadre, opt => opt.MapFrom(src => src.idTicketPadre))
              .ForMember(dest => dest.ReferenciasTicketExterno, opt => opt.MapFrom(src => src.ReferenciasTicketExterno))
              .ForMember(dest => dest.Solucion, opt => opt.MapFrom(src => src.Solucion))
              .ForMember(dest => dest.FechaVencimiento, opt => opt.MapFrom(src => src.FechaVencimiento))
              .ForMember(dest => dest.FechaCierre, opt => opt.MapFrom(src => src.FechaCierre))
              .ForMember(dest => dest.IdTipoTicket, opt => opt.MapFrom(src => src.IdTipoTicket))
              .ForMember(dest => dest.IdDepartamentos, opt => opt.MapFrom(src => src.IdDepartamentos))
              .ForMember(dest => dest.IdCategoria, opt => opt.MapFrom(src => src.IdCategoria))
              .ForMember(dest => dest.IdSubCategoria, opt => opt.MapFrom(src => src.IdSubCategoria))
              .ForMember(dest => dest.IdErrorCategoria, opt => opt.MapFrom(src => src.IdErrorCategoria))
              .ForMember(dest => dest.IdEstado, opt => opt.MapFrom(src => src.IdEstado))
              .ForMember(dest => dest.IdTipoSolucion, opt => opt.MapFrom(src => src.IdTipoSolucion))
              .ForMember(dest => dest.IdPrioridad, opt => opt.MapFrom(src => src.IdPrioridad))
              .ForMember(dest => dest.IdEmpresa, opt => opt.MapFrom(src => src.IdEmpresa))
              .ReverseMap();

            CreateMap<HdkTicketsReponse, HdkTickets>()
              .ForMember(dest => dest.IdTicket, opt => opt.MapFrom(src => src.IdTicket))
              .ForMember(dest => dest.Titulo, opt => opt.MapFrom(src => src.Titulo))
              .ForMember(dest => dest.Descripcion, opt => opt.MapFrom(src => src.Descripcion))
              .ForMember(dest => dest.IdUsuarioAsignado, opt => opt.MapFrom(src => src.IdUsuarioAsignado))
              .ForMember(dest => dest.idTicketPadre, opt => opt.MapFrom(src => src.idTicketPadre))
              .ForMember(dest => dest.ReferenciasTicketExterno, opt => opt.MapFrom(src => src.ReferenciasTicketExterno))
              .ForMember(dest => dest.Solucion, opt => opt.MapFrom(src => src.Solucion))
              .ForMember(dest => dest.FechaVencimiento, opt => opt.MapFrom(src => src.FechaVencimiento))
              .ForMember(dest => dest.FechaCierre, opt => opt.MapFrom(src => src.FechaCierre))
              .ForMember(dest => dest.IdTipoTicket, opt => opt.MapFrom(src => src.IdTipoTicket))
              .ForMember(dest => dest.IdDepartamentos, opt => opt.MapFrom(src => src.IdDepartamentos))
              .ForMember(dest => dest.IdCategoria, opt => opt.MapFrom(src => src.IdCategoria))
              .ForMember(dest => dest.IdSubCategoria, opt => opt.MapFrom(src => src.IdSubCategoria))
              .ForMember(dest => dest.IdErrorCategoria, opt => opt.MapFrom(src => src.IdErrorCategoria))
              .ForMember(dest => dest.IdEstado, opt => opt.MapFrom(src => src.IdEstado))
              .ForMember(dest => dest.IdTipoSolucion, opt => opt.MapFrom(src => src.IdTipoSolucion))
              .ForMember(dest => dest.IdPrioridad, opt => opt.MapFrom(src => src.IdPrioridad))
              .ForMember(dest => dest.FechaAdicion, opt => opt.MapFrom(src => src.FechaAdicion))
              .ForMember(dest => dest.FechaModificacion, opt => opt.MapFrom(src => src.FechaModificacion))
              .ForMember(dest => dest.UsuarioAdicion, opt => opt.MapFrom(src => src.UsuarioAdicion))
              .ForMember(dest => dest.UsuarioModificacion, opt => opt.MapFrom(src => src.UsuarioModificacion))
              .ForMember(dest => dest.Borrado, opt => opt.MapFrom(src => src.Borrado))
              .ForMember(dest => dest.IdEmpresa, opt => opt.MapFrom(src => src.IdEmpresa)).ReverseMap();
        }
    }
}

