using BussinessLayer.DTOs.ModuloCitas.CtaAppointments;
using BussinessLayer.Interface.Repository.Modulo_Citas;
using BussinessLayer.Interfaces.Repository.ModuloCitas;
using BussinessLayer.Interfaces.Repository.ModuloGeneral.Empresa;
using BussinessLayer.Interfaces.Repository.ModuloGeneral.Seguridad;
using FluentValidation;
using System.Globalization;

namespace BussinessLayer.Validations.ModuloCitas.CtaAppointments
{
    public class CtaAppointmentsRequestValidation : AbstractValidator<CtaAppointmentsRequest>
    {
        private readonly IGnEmpresaRepository _companyRepository;
        private readonly ICtaAppointmentReasonRepository _reasonRepository;
        private readonly ICtaMeetingPlaceRepository _placeRepository;
        private readonly ICtaStateRepository _stateRepository;
        private readonly ICtaAppointmentAreaRepository _areaRepository;
        private readonly IUsuarioRepository _userRepository;

        public CtaAppointmentsRequestValidation(
            IGnEmpresaRepository companyRepository,
            ICtaAppointmentReasonRepository reasonRepository,
            ICtaAppointmentAreaRepository areaRepository,
            ICtaMeetingPlaceRepository placeRepository,
            ICtaStateRepository stateRepository,
            IUsuarioRepository userRepository)
        {
            _companyRepository = companyRepository;
            _reasonRepository = reasonRepository;
            _placeRepository = placeRepository;
            _stateRepository = stateRepository;
            _areaRepository = areaRepository;
            _userRepository = userRepository;


            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("La descripción es obligatoria.")
                .MaximumLength(500).WithMessage("La descripción no debe exceder los 500 caracteres.");

            RuleFor(x => x.IdReasonAppointment)
                .GreaterThan(0).WithMessage("El motivo de la cita (IdReasonAppointment) es obligatorio.")
                .MustAsync(async (id, cancellation) => await _reasonRepository.GetById(id) != null)
                .WithMessage("El motivo de la cita (IdReasonAppointment) no existe en la base de datos.");

            RuleFor(x => x.IdPlaceAppointment)
                .GreaterThan(0).WithMessage("El lugar de la cita (IdPlaceAppointment) es obligatorio.")
                .MustAsync(async (id, cancellation) => await _placeRepository.GetById(id) != null)
                .WithMessage("El lugar de la cita (IdPlaceAppointment) no existe en la base de datos.");

            RuleFor(x => x.IdState)
                .GreaterThan(0).WithMessage("El estado de la cita (IdState) es obligatorio.")
                .MustAsync(async (id, cancellation) => await _stateRepository.GetById(id) != null)
                .WithMessage("El estado de la cita (IdState) no existe en la base de datos.");

            RuleFor(x => x.CompanyId)
                .GreaterThan(0).WithMessage("El identificador de la empresa (CompanyId) es obligatorio.")
                .MustAsync(async (id, cancellation) => await _companyRepository.GetById(id) != null)
                .WithMessage("El identificador de la empresa (CompanyId) no existe en la base de datos.");

            RuleFor(x => x.UserId)
                .GreaterThan(0).WithMessage("El usuario asignado (UserId) es obligatorio.")
                .MustAsync(async (id, cancellation) => await _userRepository.GetById(id) != null)
                .WithMessage("El usuario asignado (UserId) no existe en la base de datos.");

            RuleFor(x => x.AreaId)
                .MustAsync(async (id, cancellation) =>
                {
                    return id == null || id == 0 || await _areaRepository.GetById(id) != null;
                })
                .WithMessage("El identificador del área (AreaId) no existe en la base de datos.");

            RuleForEach(x => x.CtaAppointmentContacts)
                .ChildRules(contact =>
                {
                    contact.RuleFor(c => c.ContactId)
                        .GreaterThan(0).WithMessage("El identificador de contacto (ContactId) es obligatorio.");
                    contact.RuleFor(c => c.ContactTypeId)
                        .GreaterThan(0).WithMessage("El tipo de contacto (ContactTypeId) es obligatorio.");
                });

            RuleForEach(x => x.CtaAppointmentUsers)
                .ChildRules(user =>
                {
                    user.RuleFor(u => u.UserId)
                        .GreaterThan(0).WithMessage("El identificador de usuario invitado (UserId) es obligatorio.")
                        .MustAsync(async (id, cancellation) => await _userRepository.GetById(id) != null)
                        .WithMessage("El usuario invitado (UserId) no existe en la base de datos.");
                });

            RuleFor(x => x.AppointmentDate)
                .GreaterThan(DateTime.Now.Date)
                .WithMessage("La fecha de la cita (AppointmentDate) debe ser en el futuro.");

            RuleFor(x => x.EndAppointmentDate)
                .GreaterThanOrEqualTo(x => x.AppointmentDate)
                .WithMessage("La fecha de finalización (EndAppointmentDate) debe ser igual o posterior a la fecha de la cita.");

            RuleFor(x => x.AppointmentTime)
      .NotEmpty().WithMessage("El tiempo de inicio (AppointmentTime) es obligatorio.");

            RuleFor(x => x.EndAppointmentTime)
                .NotEmpty().WithMessage("El tiempo de finalización (EndAppointmentTime) es obligatorio.")
                .Must((request, endTime) => endTime > request.AppointmentTime)
                .WithMessage("El tiempo de finalización (EndTime) debe ser mayor al tiempo de inicio (AppointmentTime).");

            RuleFor(x => x.NotificationTime)
                .GreaterThan(TimeSpan.Zero)
                .WithMessage("El tiempo de notificación (NotificationTime) debe ser mayor a cero.");


        }

    }
}
