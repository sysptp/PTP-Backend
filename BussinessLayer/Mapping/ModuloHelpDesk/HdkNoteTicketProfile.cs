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
           .ReverseMap();

            CreateMap<HdkNoteTicketReponse, HdkNoteTicket>()
            .ForMember(dest => dest.GnEmpresa.NOMBRE_EMP, opt => opt.MapFrom(src => src.NombreEmpresa))
            .ReverseMap();

        }
    }
}
