using AutoMapper;
using BussinessLayer.DTOs.ModuloCitas;
using BussinessLayer.Interfaces.Repositories;
using BussinessLayer.Interfaces.Services.ModuloCitas;
using DataLayer.Models.ModuloCitas;

namespace BussinessLayer.Services.ModuloCitas
{
    public class CtaAreaXUserService : GenericService<CtaAreaXUserRequest, CtaAreaXUserResponse, CtaAreaXUser>, ICtaAreaXUserService
    {
        public CtaAreaXUserService(IGenericRepository<CtaAreaXUser> repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}
