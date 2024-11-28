using BussinessLayer.DTOs.ModuloInventario.Versiones;
using BussinessLayer.Interfaces.Repository.Empresa;
using FluentValidation;

namespace BussinessLayer.FluentValidations.ModuloInventario.Versiones
{
    public class CreateVersionRequestValidation  : AbstractValidator<CreateVersionsDto>
    {
        private readonly IGnEmpresaRepository _empresaRepository;
        private readonly IMarcaService _marcaService;

        public CreateVersionRequestValidation(IGnEmpresaRepository gnEmpresaRepository,
            IMarcaService marcaService)
        {

            _empresaRepository = gnEmpresaRepository;
            _marcaService = marcaService;

            // Validar que Nombre no sea nulo, no esté vacío y no contenga caracteres especiales peligrosos
            RuleFor(x => x.Nombre)
                .NotNull().WithMessage("El Nombre no puede ser nulo.")
                .NotEmpty().WithMessage("El Nombre no puede estar vacío.")
                .Matches("^[a-zA-Z0-9 ]*$").WithMessage("El Nombre contiene caracteres no permitidos.")
                .MaximumLength(50).WithMessage("El Nombre no debe exceder los 50 caracteres.");

            // Validar que IdMarca no sea nulo y sea mayor que 0
            RuleFor(x => x.IdMarca)
                .NotNull().WithMessage("El Id de la marca no puede ser nulo.")
                .GreaterThan(0).WithMessage("El Id de la marca debe ser mayor que 0.")
                .MustAsync(async (idMarca, cancellation) => await BrandExits(idMarca))
                .WithMessage("La marca especificada no existe.");

            // Validar que IdEmpresa no sea nulo y sea mayor que 0
            RuleFor(x => x.IdEmpresa)
                .NotNull().WithMessage("El Id de la empresa no puede ser nulo.")
                .GreaterThan(0).WithMessage("El Id de la empresa debe ser mayor que 0.")
                .MustAsync(async (idEmpresa, cancellation) => await CompanyExits(idEmpresa))
                .WithMessage("La empresa especificada no existe.");

        }

        public async Task<bool> CompanyExits(long companyId)
        {
            var company = await _empresaRepository.GetById(companyId);
            return company != null;
        }

        public async Task<bool> BrandExits(int brandId)
        {
            var data = await _marcaService.GetBrandById(brandId);
            return data != null;
        }
    }
    
}
