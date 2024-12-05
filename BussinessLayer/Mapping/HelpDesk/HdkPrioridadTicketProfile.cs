using BussinessLayer.DTOs.HelpDesk;
using AutoMapper;
using DataLayer.Models.ModuloHelpDesk;

namespace BussinessLayer.Mapping.HeplDesk
{
    public class HdkPrioridadTicketProfile: Profile
    {
        public HdkPrioridadTicketProfile() 
        {

            CreateMap<HdkPrioridadTicketRequest, HdkPrioridadTicket>()
              .ForMember(dest => dest.Descripcion, opt => opt.MapFrom(src => src.Descripcion))
              .ForMember(dest => dest.IdEmpresa, opt => opt.MapFrom(src => src.IdEmpresa))
              .ReverseMap();

            CreateMap<HdkPrioridadTicketReponse, HdkPrioridadTicket>()
            .ForMember(dest => dest.IdPrioridad, opt => opt.MapFrom(src => src.IdPrioridad))
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
