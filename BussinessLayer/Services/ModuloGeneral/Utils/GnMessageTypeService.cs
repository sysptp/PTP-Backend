using AutoMapper;
using BussinessLayer.DTOs.ModuloGeneral.Utils.GnMessageType;
using BussinessLayer.Interfaces.Repositories;
using BussinessLayer.Interfaces.Services.ModuloGeneral.Utils;
using DataLayer.Models.ModuloGeneral.Utils;

namespace BussinessLayer.Services.ModuloGeneral.Utils
{
    public class GnMessageTypeService : GenericService<GnMessageTypeRequest, GnMessageTypeResponse, GnMessageType>, IGnMessageTypeService
    {
        public GnMessageTypeService(IGenericRepository<GnMessageType> repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}
