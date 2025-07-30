using AutoMapper;
using BussinessLayer.DTOs.ModuloCitas.CtaState;
using BussinessLayer.Interface.Repository.Modulo_Citas;
using BussinessLayer.Interfaces.Repository.ModuloCitas;
using BussinessLayer.Interfaces.Services.ModuloCitas;
using BussinessLayer.Services;

namespace DataLayer.Models.Modulo_Citas
{
    public class CtaStateService : GenericService<CtaStateRequest, CtaStateResponse, CtaState>, ICtaStateService
    {
        private readonly IMapper _mapper;
        private readonly ICtaStateRepository _ctaStateRepository;
        private readonly ICtaAppointmentAreaRepository _areaRepository;

        public CtaStateService(IMapper mapper, ICtaStateRepository ctaStateRepository, ICtaAppointmentAreaRepository areaRepository) : base(ctaStateRepository, mapper)
        {
            _mapper = mapper;
            _ctaStateRepository = ctaStateRepository;
            _areaRepository = areaRepository;
        }

        public override async Task<List<CtaStateResponse>> GetAllDto()
        {
            var appointmentStateList = await base.GetAllDto();
            
            foreach(var state in appointmentStateList)
            {
               var area = await _areaRepository.GetById(state.AreaId);
                state.AreaDescription = area?.Description;
            }

            return appointmentStateList;

        }
    }
}
