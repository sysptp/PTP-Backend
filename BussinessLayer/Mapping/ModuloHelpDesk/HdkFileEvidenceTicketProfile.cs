using AutoMapper;
using DataLayer.Models.ModuloHelpDesk;
using BussinessLayer.DTOs.ModuloHelpDesk;

namespace BussinessLayer.Mapping.ModuloHelpDesk
{
    public class HdkFileEvidenceTicketProfile : Profile
    {
        public HdkFileEvidenceTicketProfile()
        {
            CreateMap<HdkFileEvidenceTicketRequest, HdkFileEvidenceTicket>()
          .ReverseMap();

            CreateMap<HdkFileEvidenceTicketReponse, HdkFileEvidenceTicket>()
            .ForPath(dest => dest.GnEmpresa.NOMBRE_EMP, opt => opt.MapFrom(src => src.NombreEmpresa))
            .ReverseMap();
        }
    }
}
