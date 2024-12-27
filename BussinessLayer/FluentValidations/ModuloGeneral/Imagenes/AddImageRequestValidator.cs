using BussinessLayer.DTOs.ModuloGeneral.Imagenes;
using BussinessLayer.Interfaces.Repository.ModuloGeneral.Empresa;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.FluentValidations.ModuloGeneral.Imagenes
{
    public class AddImageRequestValidator : AbstractValidator<AddImageProductDTO>
    {
        private readonly IGnEmpresaRepository _empresaRepository;
        public AddImageRequestValidator(
            IGnEmpresaRepository gnEmpresaRepository)
        {

            _empresaRepository = gnEmpresaRepository;

            // Validar que IdEmpresa no sea nulo y sea mayor que 0
            RuleFor(x => x.IdEmpresa)
                .NotNull().WithMessage("El Id de la empresa no puede ser nulo.")
                .GreaterThan(0).WithMessage("El Id de la empresa debe ser mayor que 0.")
                .MustAsync(async (idEmpresa, cancellation) => await CompanyExits(idEmpresa))
                .WithMessage("La empresa especificada no existe.");

            RuleFor(x => x.Descripcion)
               .NotEmpty()
               .WithMessage("La descripción no puede estar vacía.")
               .Matches("^[a-zA-Z0-9 ]*$")
               .WithMessage("La descripción no debe contener caracteres especiales.");

            RuleFor(x => x.Url)
                .NotEmpty()
                .WithMessage("La URL no puede estar vacía.")
                .Matches("^[a-zA-Z0-9:/._-]*$")
                .WithMessage("La URL contiene caracteres no válidos.");

            RuleFor(x => x.EsPrincipal)
                .NotNull()
                .WithMessage("El campo EsPrincipal no puede ser nulo.");
        }

        public async Task<bool> CompanyExits(long companyId)
        {
            var company = await _empresaRepository.GetById(companyId);
            return company != null;
        }
    }
}
