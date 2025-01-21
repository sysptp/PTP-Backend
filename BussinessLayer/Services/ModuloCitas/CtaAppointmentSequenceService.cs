using AutoMapper;
using BussinessLayer.DTOs.ModuloCitas;
using BussinessLayer.Interfaces.Repository.ModuloCitas;
using BussinessLayer.Interfaces.Services.ModuloCitas;
using DataLayer.Models.ModuloCitas;

namespace BussinessLayer.Services.ModuloCitas
{
    public class CtaAppointmentSequenceService : GenericService<CtaAppointmentSequenceRequest, CtaAppointmentSequenceResponse, CtaAppointmentSequence>, ICtaAppointmentSequenceService
    {
        private readonly ICtaAppointmentSequenceRepository _repository;
        private readonly IMapper _mapper;

        public CtaAppointmentSequenceService(ICtaAppointmentSequenceRepository repository, IMapper mapper) : base(repository, mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public override Task<CtaAppointmentSequenceResponse> Add(CtaAppointmentSequenceRequest vm)
        {
            vm.SequenceNumber = vm.MinValue;
            return base.Add(vm);
        }

        public async Task<string> GetFormattedSequenceAsync(long companyId)
        {
            var sequences = await _repository.GetAll();
            
          var sequence = sequences.FirstOrDefault(s => s.CompanyId == companyId && s.IsActive);

            if (sequence == null)
                throw new InvalidOperationException("No existe una secuencia activa para esta compañía.");

            var formattedSequence = $"{sequence.Prefix ?? ""}{sequence.SequenceIdentifier ?? ""}{sequence.SequenceNumber}{sequence.Suffix ?? ""}";
            return formattedSequence;
        }

        public async Task UpdateSequenceAsync(long companyId)
        {
            var sequences = await _repository.GetAll();

            var sequence = sequences.FirstOrDefault(s => s.CompanyId == companyId && s.IsActive);

            if (sequence == null)
                throw new InvalidOperationException("No existe una secuencia activa para esta compañía.");

            if (sequence.MaxValue > 0 && sequence.SequenceNumber >= sequence.MaxValue)
                throw new InvalidOperationException("Se ha alcanzado el valor máximo permitido para esta secuencia.");

            sequence.SequenceNumber += sequence.IncrementBy;
            sequence.LastUsed = DateTime.UtcNow;

            await _repository.Update(sequence, sequence.Id);
        }
    }
}
