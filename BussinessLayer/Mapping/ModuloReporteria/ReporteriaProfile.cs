using AutoMapper;
using BussinessLayer.DTOs.ModuloReporteria;
using DataLayer.Models.ModuloReporteria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.Mapping.ModuloReporteria
{
    public class ReporteriaProfile : Profile
    {
        public ReporteriaProfile()
        {
            CreateMap<CreateRepReporteDto, RepReporte>().ReverseMap();
            CreateMap<EditRepReporteDto, RepReporte>().ReverseMap();
            CreateMap<ViewRepReporteDto, RepReporte>().ReverseMap();
        }
    }

}
