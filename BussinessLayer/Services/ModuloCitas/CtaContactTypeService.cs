using AutoMapper;
using BussinessLayer.DTOs.ModuloCitas;
using BussinessLayer.Interfaces.Repositories;
using BussinessLayer.Interfaces.Services.ModuloCitas;
using DataLayer.Models.ModuloCitas;

namespace BussinessLayer.Services.ModuloCitas
{
    public class CtaContactTypeService : GenericService<CtaContactTypeRequest, CtaContactTypeResponse, CtaContactType>, ICtaContactTypeService
    {
        public CtaContactTypeService(IGenericRepository<CtaContactType> repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}
