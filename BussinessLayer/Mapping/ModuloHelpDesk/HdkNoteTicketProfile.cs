using AutoMapper;
using DataLayer.Models.ModuloHelpDesk;
using BussinessLayer.DTOs.ModuloHelpDesk;

namespace BussinessLayer.Mapping.ModuloHelpDesk
{
    public class HdkNoteTicketProfile : Profile
    {
        public HdkNoteTicketProfile()
        {

            CreateMap<HdkNoteTicketRequest, HdkNoteTicket>()
          .ForMember(dest => dest.Notas, opt => opt.MapFrom(src => src.Notas))
          .ForMember(dest => dest.IdTicket, opt => opt.MapFrom(src => src.IdTicket))
          .ForMember(dest => dest.IdEmpresa, opt => opt.MapFrom(src => src.IdEmpresa))
          .ReverseMap();

            CreateMap<HdkNoteTicketReponse, HdkNoteTicket>()
            .ForMember(dest => dest.IdNota, opt => opt.MapFrom(src => src.IdNota))
            .ForMember(dest => dest.Notas, opt => opt.MapFrom(src => src.Notas))
            .ForMember(dest => dest.IdTicket, opt => opt.MapFrom(src => src.IdTicket))
            .ForMember(dest => dest.FechaAdicion, opt => opt.MapFrom(src => src.FechaAdicion))
            .ForMember(dest => dest.FechaModificacion, opt => opt.MapFrom(src => src.FechaModificacion))
            .ForMember(dest => dest.UsuarioAdicion, opt => opt.MapFrom(src => src.UsuarioAdicion))
            .ForMember(dest => dest.UsuarioModificacion, opt => opt.MapFrom(src => src.UsuarioModificacion))
            .ForMember(dest => dest.Borrado, opt => opt.MapFrom(src => src.Borrado))
            .ForMember(dest => dest.IdEmpresa, opt => opt.MapFrom(src => src.IdEmpresa)).ReverseMap();

        }
    }
}
