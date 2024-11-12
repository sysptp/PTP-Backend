using BussinessLayer.DTOs.HelpDesk;
using DataLayer.Models.HelpDesk;
using AutoMapper;

namespace BussinessLayer.Mapping.HeplDesk
{
    public  class HdkFileEvidenceTicketProfile: Profile
    {
        public HdkFileEvidenceTicketProfile()
        {
            CreateMap<HdkFileEvidenceTicketRequest, HdkFileEvidenceTicket>()
          .ForMember(dest => dest.IdTicket, opt => opt.MapFrom(src => src.IdTicket))
          .ForMember(dest => dest.UrlFile, opt => opt.MapFrom(src => src.UrlFile))
          .ForMember(dest => dest.FileName, opt => opt.MapFrom(src => src.FileName))
          .ForMember(dest => dest.FileExtencion, opt => opt.MapFrom(src => src.FileExtencion))
          .ForMember(dest => dest.IdEmpresa, opt => opt.MapFrom(src => src.IdEmpresa))
          .ReverseMap();

            CreateMap<HdkFileEvidenceTicketReponse, HdkFileEvidenceTicket>()
            .ForMember(dest => dest.IdFileEvidence, opt => opt.MapFrom(src => src.IdFileEvidence))
            .ForMember(dest => dest.IdTicket, opt => opt.MapFrom(src => src.IdTicket))
            .ForMember(dest => dest.UrlFile, opt => opt.MapFrom(src => src.UrlFile))
            .ForMember(dest => dest.FileName, opt => opt.MapFrom(src => src.FileName))
            .ForMember(dest => dest.FileExtencion, opt => opt.MapFrom(src => src.FileExtencion))
            .ForMember(dest => dest.FechaAdicion, opt => opt.MapFrom(src => src.FechaAdicion))
            .ForMember(dest => dest.FechaModificacion, opt => opt.MapFrom(src => src.FechaModificacion))
            .ForMember(dest => dest.UsuarioAdicion, opt => opt.MapFrom(src => src.UsuarioAdicion))
            .ForMember(dest => dest.UsuarioModificacion, opt => opt.MapFrom(src => src.UsuarioModificacion))
            .ForMember(dest => dest.Borrado, opt => opt.MapFrom(src => src.Borrado))
            .ForMember(dest => dest.IdEmpresa, opt => opt.MapFrom(src => src.IdEmpresa)).ReverseMap();
        }
    }
}
