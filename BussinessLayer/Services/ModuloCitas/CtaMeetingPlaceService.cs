using AutoMapper;
using BussinessLayer.DTOs.ModuloCitas.CtaMeetingPlace;
using BussinessLayer.Interfaces.Repositories;
using BussinessLayer.Interfaces.Services.ModuloCitas;
using BussinessLayer.Services;

namespace DataLayer.Models.Modulo_Citas
{
    public class CtaMeetingPlaceService : GenericService<CtaMeetingPlaceRequest, CtaMeetingPlaceResponse, CtaMeetingPlace>, ICtaMeetingPlaceService
    {
        public CtaMeetingPlaceService(IGenericRepository<CtaMeetingPlace> repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}
