using AutoMapper;
using DataLayer.Models.ModuloHelpDesk;
using BussinessLayer.DTOs.ModuloHelpDesk;

namespace BussinessLayer.Mapping.ModuloHelpDesk
{
    public class HdkPrioridadTicketProfile : Profile
    {
        public HdkPrioridadTicketProfile()
        {

            CreateMap<HdkPrioridadTicketRequest, HdkPrioridadTicket>()
              .ReverseMap();

            CreateMap<HdkPrioridadTicketReponse, HdkPrioridadTicket>()
            .ForMember(dest => dest.GnEmpresa.NOMBRE_EMP, opt => opt.MapFrom(src => src.NombreEmpresa))
            .ReverseMap();
        }
    }
}
