using BussinessLayer.DTOs.ModuloInventario.Marcas;
using BussinessLayer.Interfaces.Repository.Empresa;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.FluentValidations.ModuloInventario.Marcas
{
    public class EditBrandRequestValidation : AbstractValidator<EditBrandDto>
    {
        private readonly IGnEmpresaRepository _empresaRepository;

        public EditBrandRequestValidation(IGnEmpresaRepository gnEmpresaRepository)
        {

            _empresaRepository = gnEmpresaRepository;

            // Validar que Id no sea nulo y sea mayor que 0
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("El Id debe ser mayor que 0.");

            // Validar que Nombre no sea nulo, no esté vacío y no contenga caracteres especiales peligrosos
            RuleFor(x => x.Nombre)
                .NotNull().WithMessage("El Nombre no puede ser nulo.")
                .NotEmpty().WithMessage("El Nombre no puede estar vacío.")
                .Matches("^[a-zA-Z0-9 ]*$").WithMessage("El Nombre contiene caracteres no permitidos.")
                .MaximumLength(50).WithMessage("El Nombre no debe exceder los 50 caracteres.");

        }

        public async Task<bool> CompanyExits(long companyId)
        {
            var company = await _empresaRepository.GetById(companyId);
            return company != null;
        }
    }
}
