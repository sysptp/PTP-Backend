using AutoMapper;
using BussinessLayer.DTOs.ModuloCitas;
using BussinessLayer.Interfaces.Repository.ModuloCitas;
using BussinessLayer.Interfaces.Services.ModuloCitas;
using DataLayer.Models.ModuloCitas;

namespace BussinessLayer.Services.ModuloCitas
{
    public class CtaAppointmentSequenceService : GenericService<CtaAppointmentSequenceRequest, CtaAppointmentSequenceResponse, CtaAppointmentSequence>,
        ICtaAppointmentSequenceService
    {
        private readonly ICtaAppointmentSequenceRepository _repository;

        public CtaAppointmentSequenceService(ICtaAppointmentSequenceRepository repository, IMapper mapper) : base(repository, mapper)
        {
            _repository = repository;
        }

        public override Task<CtaAppointmentSequenceResponse> Add(CtaAppointmentSequenceRequest vm)
        {
            vm.SequenceNumber = vm.MinValue;
            vm.LastUsed = DateTime.Now;
            return base.Add(vm);
        }

        public async Task<string> GetFormattedSequenceAsync(long companyId, int? areaId)
        {
            var sequences = await _repository.GetAll();
            var sequence = new CtaAppointmentSequence();
            
            if (areaId != null && areaId > 0) 
            {
                 sequence = sequences.FirstOrDefault(s => s.CompanyId == companyId && s.IsActive && areaId == s.AreaId);
            }
            else
            {
                sequence = sequences.FirstOrDefault(s => s.CompanyId == companyId && s.IsActive);
            }

            if (sequence == null)
                throw new InvalidOperationException("No existe una secuencia activa para esta compañía.");

            var formattedSequence = $"{sequence.Prefix ?? ""}{sequence.SequenceIdentifier ?? ""}{sequence.SequenceNumber}{sequence.Suffix ?? ""}";
            return formattedSequence;
        }

        public async Task UpdateSequenceAsync(long companyId,int? areaId)
        {
            var sequences = await _repository.GetAll();

           var sequence = GetSequenceByCompanyIdOrCompanyIdAndAreaId(companyId, areaId, sequences);

            if (sequence == null)
                throw new InvalidOperationException("No existe una secuencia activa para esta compañía.");

            if (sequence.MaxValue > 0 && sequence.SequenceNumber >= sequence.MaxValue)
                throw new InvalidOperationException("Se ha alcanzado el valor máximo permitido para esta secuencia.");

            sequence.SequenceNumber += sequence.IncrementBy;
            sequence.LastUsed = DateTime.UtcNow;

            await _repository.Update(sequence, sequence.Id);
        }

        private CtaAppointmentSequence GetSequenceByCompanyIdOrCompanyIdAndAreaId(long companyId, int? areaId, IList<CtaAppointmentSequence> sequences)
        {
            var sequence = new CtaAppointmentSequence();

            if (areaId != null && areaId > 0)
            {
                sequence = sequences.FirstOrDefault(s => s.CompanyId == companyId && s.IsActive && areaId == s.AreaId);
            }
            else
            {
                sequence = sequences.FirstOrDefault(s => s.CompanyId == companyId && s.IsActive);
            }

            return sequence;
        }
    }
}
