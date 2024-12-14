using BussinessLayer.DTOs.ModuloInventario.Suplidores;
using BussinessLayer.Interfaces.Repository.ModuloGeneral.Empresa;
using FluentValidation;

namespace BussinessLayer.FluentValidations.ModuloInventario.Suplidores
{
    public class EditSuppliersRequestValidation : AbstractValidator<EditSuppliersDto>
    {

        private readonly IGnEmpresaRepository _empresaRepository;

        public EditSuppliersRequestValidation(IGnEmpresaRepository gnEmpresaRepository)
        {

            _empresaRepository = gnEmpresaRepository;

            // Validar que Id sea mayor que 0
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("El Id debe ser mayor que 0.");

            // Validar que TipoIdentificacion no sea nulo y esté dentro de un rango permitido
            RuleFor(x => x.TipoIdentificacion)
                .NotNull().WithMessage("El TipoIdentificacion no puede ser nulo.")
                .InclusiveBetween(1, 3).WithMessage("El TipoIdentificacion debe estar entre 1 y 3."); // Ajusta el rango según sea necesario

            // Validar que NumeroIdentificacion no sea nulo, no esté vacío y tenga un formato válido
            RuleFor(x => x.NumeroIdentificacion)
                .NotNull().WithMessage("El NumeroIdentificacion no puede ser nulo.")
                .NotEmpty().WithMessage("El NumeroIdentificacion no puede estar vacío.")
                .Matches("^[a-zA-Z0-9-]*$").WithMessage("El NumeroIdentificacion contiene caracteres no permitidos.")
                .MaximumLength(20).WithMessage("El NumeroIdentificacion no debe exceder los 20 caracteres.");

            // Validar que Nombres no sea nulo ni esté vacío
            RuleFor(x => x.Nombres)
                .NotNull().WithMessage("El campo Nombres no puede ser nulo.")
                .NotEmpty().WithMessage("El campo Nombres no puede estar vacío.")
                .MaximumLength(50).WithMessage("El campo Nombres no debe exceder los 50 caracteres.");

            // Validar que Apellidos no sea nulo ni esté vacío
            RuleFor(x => x.Apellidos)
                .NotNull().WithMessage("El campo Apellidos no puede ser nulo.")
                .NotEmpty().WithMessage("El campo Apellidos no puede estar vacío.")
                .MaximumLength(50).WithMessage("El campo Apellidos no debe exceder los 50 caracteres.");

            // Validar que TelefonoPrincipal tenga un formato válido
            RuleFor(x => x.TelefonoPrincipal)
                .NotNull().WithMessage("El campo TelefonoPrincipal no puede ser nulo.")
                .NotEmpty().WithMessage("El campo TelefonoPrincipal no puede estar vacío.")
                .Matches(@"^\+?[0-9]{7,15}$").WithMessage("El TelefonoPrincipal debe contener solo números y puede incluir el prefijo '+'.");

            // Validar que DireccionPrincipal no sea nula y tenga una longitud razonable
            RuleFor(x => x.DireccionPrincipal)
                .NotNull().WithMessage("El campo DireccionPrincipal no puede ser nulo.")
                .NotEmpty().WithMessage("El campo DireccionPrincipal no puede estar vacío.")
                .MaximumLength(100).WithMessage("El campo DireccionPrincipal no debe exceder los 100 caracteres.");

            // Validar que Email tenga un formato de correo válido
            RuleFor(x => x.Email)
                .NotNull().WithMessage("El campo Email no puede ser nulo.")
                .NotEmpty().WithMessage("El campo Email no puede estar vacío.")
                .EmailAddress().WithMessage("El campo Email no tiene un formato válido.");

            // Validar que PaginaWeb tenga un formato válido
            RuleFor(x => x.PaginaWeb)
                .MaximumLength(100).WithMessage("El campo PaginaWeb no debe exceder los 100 caracteres.")
                .Matches(@"^https?:\/\/[^\s$.?#].[^\s]*$").WithMessage("El campo PaginaWeb no tiene un formato válido.")
                .When(x => !string.IsNullOrEmpty(x.PaginaWeb));

            // Validar que Descripcion tenga una longitud razonable
            RuleFor(x => x.Descripcion)
                .MaximumLength(200).WithMessage("El campo Descripcion no debe exceder los 200 caracteres.");
        }

        public async Task<bool> CompanyExits(long companyId)
        {
            var company = await _empresaRepository.GetById(companyId);
            return company != null;
        }
    }
}
