using BussinessLayer.DTOs.ModuloGeneral.Archivos;
using BussinessLayer.Interfaces.Repository.ModuloGeneral.Empresa;
using FluentValidation;

namespace BussinessLayer.FluentValidations.ModuloGeneral.Archivo
{
    public class CreateGnTecnoAlmacenExternoDtoValidator : AbstractValidator<CreateGnTecnoAlmacenExternoDto>
    {
        private readonly IGnEmpresaRepository _empresaRepository;

        public CreateGnTecnoAlmacenExternoDtoValidator(
            IGnEmpresaRepository gnEmpresaRepository)
        {

            _empresaRepository = gnEmpresaRepository;
            RuleFor(x => x.IdEmpresa).NotEmpty().WithMessage("El IdEmpresa es obligatorio.");
            RuleFor(x => x.IdEmpresa)
                .NotNull().WithMessage("El Id de la empresa no puede ser nulo.")
                .GreaterThan(0).WithMessage("El Id de la empresa debe ser mayor que 0.")
                .MustAsync(async (idEmpresa, cancellation) => await CompanyExits(idEmpresa))
                .WithMessage("La empresa especificada no existe.");
            RuleFor(x => x.Descripcion).MaximumLength(150).WithMessage("La Descripcion no puede superar los 150 caracteres.");
            RuleFor(x => x.UsuaridioExteno).MaximumLength(50).WithMessage("El Usuario Externo no puede superar los 50 caracteres.");
            RuleFor(x => x.PassWordExt).MaximumLength(200).WithMessage("El Password Externo no puede superar los 200 caracteres.");
        }
        public async Task<bool> CompanyExits(long companyId)
        {
            var company = await _empresaRepository.GetById(companyId);
            return company != null;
        }
    }
}
