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
    public class RepReportesVariableProfile : Profile
    {
        public RepReportesVariableProfile()
        {
            CreateMap<RepReportesVariable, CreateRepReportesVariableDto>().ReverseMap();
            CreateMap<RepReportesVariable, EditRepReportesVariableDto>().ReverseMap();
            CreateMap<RepReportesVariable, ViewRepReportesVariableDto>().ReverseMap();
        }
    }
}
