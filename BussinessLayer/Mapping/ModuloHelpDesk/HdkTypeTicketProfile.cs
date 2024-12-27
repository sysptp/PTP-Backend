using AutoMapper;
using DataLayer.Models.ModuloHelpDesk;
using BussinessLayer.DTOs.ModuloHelpDesk;


namespace BussinessLayer.Mapping.ModuloHelpDesk
{
    public class HdkTypeTicketProfile : Profile
    {
        public HdkTypeTicketProfile()
        {
            CreateMap<HdkTypeTicketRequest, HdkTypeTicket>()
             .ForMember(dest => dest.Descripcion, opt => opt.MapFrom(src => src.Descripcion))
             .ForMember(dest => dest.IdEmpresa, opt => opt.MapFrom(src => src.IdEmpresa))
             .ReverseMap();

            CreateMap<HdkTypeTicketReponse, HdkTypeTicket>()
              .ForMember(dest => dest.IdTipoTicket, opt => opt.MapFrom(src => src.IdTipoTicket))
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
