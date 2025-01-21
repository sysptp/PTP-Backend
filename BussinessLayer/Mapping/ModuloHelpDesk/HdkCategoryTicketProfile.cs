using AutoMapper;
using BussinessLayer.DTOs.ModuloHelpDesk;
using DataLayer.Models.ModuloHelpDesk;

namespace BussinessLayer.Mapping.ModuloHelpDesk
{
    public class HdkCategoryTicketProfile : Profile
    {
        public HdkCategoryTicketProfile()
        {
            CreateMap<HdkCategoryTicketRequest, HdkCategoryTicket>().ReverseMap();

            CreateMap<HdkCategoryTicket,HdkCategoryTicketReponse>()
            .ForPath(dest => dest.NombreEmpresa, opt => opt.MapFrom(src => src.GnEmpresa.NOMBRE_EMP)).ReverseMap();


        }
    }
}
