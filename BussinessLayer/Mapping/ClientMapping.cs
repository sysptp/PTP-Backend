using AutoMapper;
using BussinessLayer.DTOs.Cliente;
using DataLayer.Models.Clients;

namespace BussinessLayer.Mapping
{
    public class ClientMapping : Profile
    {
        public ClientMapping()
        {
            CreateMap<CreateClientDto, Client>()
                .ForMember(x => x.IsDeleted, opt => opt.Ignore())
                .ForMember(x => x.ModifiedBy, opt => opt.Ignore())
                .ForMember(x => x.DateModified, opt => opt.Ignore())
                .ForMember(x => x.ClientContacts, opt => opt.Ignore())
                .ReverseMap();

        }
    }
}
