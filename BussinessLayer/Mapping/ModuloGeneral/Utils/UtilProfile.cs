using AutoMapper;
using BussinessLayer.DTOs.ModuloGeneral.Utils;
using BussinessLayer.DTOs.ModuloGeneral.Utils.GnMessageType;
using DataLayer.Models.ModuloGeneral;
using DataLayer.Models.ModuloGeneral.Utils;

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

            CreateMap<GnMessageType, GnMessageTypeRequest>()
              .ReverseMap();

            CreateMap<GnMessageType, GnMessageTypeResponse>()
              .ReverseMap();
        }
    }
}
