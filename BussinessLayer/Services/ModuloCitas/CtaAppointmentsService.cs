using AutoMapper;
using BussinessLayer.DTOs.ModuloCitas.CtaAppointments;
using BussinessLayer.Interface.Repository.ModuloCitas;
using BussinessLayer.Interfaces.Repository.ModuloCitas;
using BussinessLayer.Interfaces.Services.ModuloCitas;
using BussinessLayer.Services;
using DataLayer.Models.ModuloCitas;

namespace DataLayer.Models.Modulo_Citas
{
    public class CtaAppointmentsService : GenericService<CtaAppointmentsRequest, CtaAppointmentsResponse, CtaAppointments>, ICtaAppointmentsService
    {
        private readonly ICtaAppointmentSequenceService _sequenceService;
        private readonly IMapper _mapper;
        private readonly ICtaAppointmentContactsRepository _contactsRepository;
        private readonly ICtaAppointmentUsersRepository _usersRepository;
        private readonly ICtaAppointmentsRepository _repository;

        public CtaAppointmentsService(ICtaAppointmentSequenceService sequenceService, IMapper mapper,
            ICtaAppointmentsRepository repository, ICtaAppointmentContactsRepository contactsRepository,
            ICtaAppointmentUsersRepository usersRepository) : base(repository, mapper)
        {
            _sequenceService = sequenceService;
            _mapper = mapper;
            _contactsRepository = contactsRepository;
            _usersRepository = usersRepository;
            _repository = repository;
        }

        public override async Task<CtaAppointmentsResponse> Add(CtaAppointmentsRequest vm)
        {
            var nextSequence = await _sequenceService.GetFormattedSequenceAsync(vm.CompanyId, vm.AreaId);
            vm.AppointmentCode = nextSequence;

            await _sequenceService.UpdateSequenceAsync(vm.CompanyId, vm.AreaId);

            var appointmentEntity = await base.Add(vm);

            var appointmentId = appointmentEntity.AppointmentId;

            if (vm.CtaAppointmentUsers != null && vm.CtaAppointmentUsers.Any())
            {
                var usersToInsert = vm.CtaAppointmentUsers.Select(user => new CtaAppointmentUsers
                {
                    UserId = user.UserId,
                    AppointmentId = appointmentId,
                    CompanyId = appointmentEntity.CompanyId
                }).ToList();

                await _usersRepository.AddRangeAsync(usersToInsert);
            }

            if (vm.CtaAppointmentContacts != null && vm.CtaAppointmentContacts.Any())
            {
                var contactsToInsert = vm.CtaAppointmentContacts.Select(contact => new CtaAppointmentContacts
                {
                    ContactId = contact.ContactId,
                    AppointmentId = appointmentId,
                    CompanyId = appointmentEntity.CompanyId
                }).ToList();

                await _contactsRepository.AddRangeAsync(contactsToInsert);
            }

            return _mapper.Map<CtaAppointmentsResponse>(appointmentEntity);
        }

        //public override async Task<List<CtaAppointmentsResponse>> GetAllDto()
        //{
        //    var appointments = await _repository.GetAllWithIncludeAsync(new List<string>
        //    { "CtaAppointmentReason", "CtaMeetingPlace","CtaState", "CtaAppointmentContacts", "CtaAppointmentUsers", "CtaAppointmentManagement"});

        //    var appointmentDtoList = _mapper.Map<List<CtaAppointmentsResponse>>(appointments);

        //    return appointmentDtoList;
        //}

    }
}
