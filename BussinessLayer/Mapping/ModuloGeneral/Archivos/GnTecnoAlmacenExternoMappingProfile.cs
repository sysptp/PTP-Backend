using AutoMapper;
using BussinessLayer.DTOs.ModuloGeneral.Archivos;
using DataLayer.Models.ModuloGeneral.Archivos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.Mapping.ModuloGeneral.Archivos
{
    public class GnTecnoAlmacenExternoMappingProfile : Profile
    {
        public GnTecnoAlmacenExternoMappingProfile()
        {
            CreateMap<GnTecnoAlmacenExterno, CreateGnTecnoAlmacenExternoDto>().ReverseMap();
            CreateMap<GnTecnoAlmacenExterno, EditGnTecnoAlmacenExternoDto>().ReverseMap();
            CreateMap<GnTecnoAlmacenExterno, ViewGnTecnoAlmacenExternoDto>().ReverseMap();
        }
    }
}
