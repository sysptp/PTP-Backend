using AutoMapper;
using BussinessLayer.DTOs.ModuloCitas.CtaGuest;
using BussinessLayer.Interfaces.Repositories;
using BussinessLayer.Interfaces.Services.ModuloCitas;
using DataLayer.Models.ModuloCitas;

namespace BussinessLayer.Services.ModuloCitas
{
    public class CtaGuestService : GenericService<CtaGuestRequest, CtaGuestResponse, CtaGuest>, ICtaGuestService
    {
        public CtaGuestService(IGenericRepository<CtaGuest> repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}
