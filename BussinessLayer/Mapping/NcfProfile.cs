using AutoMapper;
using BussinessLayer.DTOs.Ncfs;
using DataLayer.Models.Ncf;

namespace BussinessLayer.Mapping
{
    public class NcfProfile : Profile
    {
        public NcfProfile()
        {
            CreateMap<Ncf, CreateNcfDto>()
                .ReverseMap()
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ForMember(x => x.AvailableSequence, opt => opt.Ignore())
                .ForMember(x => x.Sequence, opt => opt.Ignore());

            CreateMap<Ncf, NcfDto>()
               .ReverseMap()
               .ForMember(x => x.Id, opt => opt.Ignore())
               .ForMember(x => x.UserId, opt => opt.Ignore());
               
               
        }
    }
}
