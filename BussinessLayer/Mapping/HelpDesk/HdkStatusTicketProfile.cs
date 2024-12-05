using BussinessLayer.DTOs.HelpDesk;
using AutoMapper;
using DataLayer.Models.ModuloHelpDesk;

namespace BussinessLayer.Mapping.HeplDesk
{
    public class HdkStatusTicketProfile:Profile
    { 
        public HdkStatusTicketProfile()
        {

            CreateMap<HdkStatusTicketRequest, HdkStatusTicket>()
              .ForMember(dest => dest.Descripcion, opt => opt.MapFrom(src => src.Descripcion))
              .ForMember(dest => dest.EsCierre, opt => opt.MapFrom(src => src.EsCierre))
              .ForMember(dest => dest.IdEmpresa, opt => opt.MapFrom(src => src.IdEmpresa))
              .ReverseMap();

            CreateMap<HdkStatusTicketReponse, HdkStatusTicket>()
            .ForMember(dest => dest.IdEstado, opt => opt.MapFrom(src => src.IdEstado))
            .ForMember(dest => dest.Descripcion, opt => opt.MapFrom(src => src.Descripcion))
            .ForMember(dest => dest.EsCierre, opt => opt.MapFrom(src => src.EsCierre))
            .ForMember(dest => dest.FechaAdicion, opt => opt.MapFrom(src => src.FechaAdicion))
            .ForMember(dest => dest.FechaModificacion, opt => opt.MapFrom(src => src.FechaModificacion))
            .ForMember(dest => dest.UsuarioAdicion, opt => opt.MapFrom(src => src.UsuarioAdicion))
            .ForMember(dest => dest.UsuarioModificacion, opt => opt.MapFrom(src => src.UsuarioModificacion))
            .ForMember(dest => dest.Borrado, opt => opt.MapFrom(src => src.Borrado))
            .ForMember(dest => dest.IdEmpresa, opt => opt.MapFrom(src => src.IdEmpresa)).ReverseMap();
        }
    }
}
