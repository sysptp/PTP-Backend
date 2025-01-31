using AutoMapper;
using BussinessLayer.DTOs.ModuloGeneral.Utils;
using DataLayer.Models.ModuloGeneral;

namespace BussinessLayer.Mapping.ModuloGeneral.Utils
{
    public class UtilProfile : Profile
    {
        public UtilProfile()
        {
            CreateMap<GnRepeatUnit, GnRepeatUnitRequest>()
               .ReverseMap();

            CreateMap<GnRepeatUnit, GnRepeatUnitResponse>()
              .ReverseMap();
        }
    }
}
