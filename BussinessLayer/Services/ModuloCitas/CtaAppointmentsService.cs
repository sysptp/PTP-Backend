using AutoMapper;
using BussinessLayer.DTOs.ModuloCitas.CtaAppointments;
using BussinessLayer.Interface.Repository.ModuloCitas;
using BussinessLayer.Interfaces.Services.ModuloCitas;
using BussinessLayer.Services;

namespace DataLayer.Models.Modulo_Citas
{
    public class CtaAppointmentsService : GenericService<CtaAppointmentsRequest, CtaAppointmentsResponse, CtaAppointments>, ICtaAppointmentsService
    {
        private readonly ICtaAppointmentSequenceService _sequenceService;

        public CtaAppointmentsService(ICtaAppointmentsRepository repository, IMapper mapper,
            ICtaAppointmentSequenceService sequenceService) : base(repository, mapper)
        {
            _sequenceService = sequenceService;
        }

        public override async Task<CtaAppointmentsResponse> Add(CtaAppointmentsRequest vm)
        {
            var nextSequence = await _sequenceService.GetFormattedSequenceAsync(vm.CompanyId,vm.AreaId);

            vm.AppointmentCode = nextSequence;

            await _sequenceService.UpdateSequenceAsync(vm.CompanyId, vm.AreaId);

            return await base.Add(vm);
        }
    }
}
