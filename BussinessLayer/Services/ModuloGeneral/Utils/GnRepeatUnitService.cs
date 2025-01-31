using AutoMapper;
using BussinessLayer.DTOs.ModuloGeneral.Utils;
using BussinessLayer.Interface.Repository.ModuloGeneral;
using BussinessLayer.Interfaces.Services.ModuloGeneral;
using DataLayer.Models.ModuloGeneral;

namespace BussinessLayer.Services.ModuloGeneral
{
    public class GnRepeatUnitService : GenericService<GnRepeatUnitRequest, GnRepeatUnitResponse, GnRepeatUnit>, IGnRepeatUnitService
    {
        public GnRepeatUnitService(IGnRepeatUnitRepository repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}