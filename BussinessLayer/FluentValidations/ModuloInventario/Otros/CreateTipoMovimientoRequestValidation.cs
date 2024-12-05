using BussinessLayer.DTOs.ModuloInventario.Otros;
using BussinessLayer.Interfaces.Repository.Empresa;
using FluentValidation;

namespace BussinessLayer.FluentValidations.ModuloInventario.Otros
{
    public class CreateTipoMovimientoRequestValidation : AbstractValidator<CreateTipoMovimientoDto>
    {
        private readonly IGnEmpresaRepository _empresaRepository;

        public CreateTipoMovimientoRequestValidation(IGnEmpresaRepository gnEmpresaRepository)
        {
            _empresaRepository = gnEmpresaRepository;

            RuleFor(x => x.IdEmpresa)
               .NotNull().WithMessage("El Id de la empresa no puede ser nulo.")
               .GreaterThan(0).WithMessage("El Id de la empresa debe ser mayor que 0.")
               .MustAsync(async (idEmpresa, cancellation) => await CompanyExits(idEmpresa))
               .WithMessage("La empresa especificada no existe.");

            RuleFor(x => x.Nombre)
                .NotEmpty().WithMessage("El nombre es obligatorio.")
                .MaximumLength(100).WithMessage("El nombre no puede exceder los 100 caracteres.")
                .Matches("^[a-zA-Z0-9 ]+$").WithMessage("El nombre solo puede contener caracteres alfanuméricos y espacios.");

            RuleFor(x => x.IN_OUT)
                .NotNull().WithMessage("Debe especificar si es IN o OUT.");
        }

        public async Task<bool> CompanyExits(long companyId)
        {
            var company = await _empresaRepository.GetById(companyId);
            return company != null;
        }
    }
}
