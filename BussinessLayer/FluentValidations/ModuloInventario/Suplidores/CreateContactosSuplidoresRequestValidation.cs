using BussinessLayer.DTOs.ModuloInventario.Suplidores;
using BussinessLayer.Interfaces.Repository.ModuloGeneral.Empresa;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.FluentValidations.ModuloInventario.Suplidores
{
    public class CreateContactosSuplidoresRequestValidation : AbstractValidator<CreateContactosSuplidoresDto>
    {
        private readonly IGnEmpresaRepository _empresaRepository;
        private readonly ISuplidoresService _suplidoresService;

        public CreateContactosSuplidoresRequestValidation(
            IGnEmpresaRepository gnEmpresaRepository,
            ISuplidoresService suplidoresService)
        {

            _empresaRepository = gnEmpresaRepository;
            _suplidoresService = suplidoresService;

            RuleFor(x => x.IdEmpresa)
                .NotNull().WithMessage("El Id de la empresa no puede ser nulo.")
                .GreaterThan(0).WithMessage("El Id de la empresa debe ser mayor que 0.")
                .MustAsync(async (idEmpresa, cancellation) => await CompanyExits(idEmpresa))
                .WithMessage("La empresa especificada no existe.");

            RuleFor(x => x.IdSuplidor)
                .NotNull().WithMessage("El Id del suplidor no puede ser nulo.")
                .GreaterThan(0).WithMessage("El Id del suplidor debe ser mayor que 0.")
                .MustAsync(async (idSuplidor, cancellation) => await SupplierExits(idSuplidor))
                .WithMessage("El suplidor especificado no existe.");

            RuleFor(x => x.Nombre)
                .NotEmpty().WithMessage("El Nombre es requerido.")
                .MaximumLength(30).WithMessage("El Nombre no puede tener más de 30 caracteres.")
                .Matches(@"^[a-zA-Z0-9\s]+$").WithMessage("El Nombre solo puede contener letras, números y espacios.");

            RuleFor(x => x.Telefono1)
                .NotEmpty().WithMessage("El Telefono1 es requerido.")
                .MaximumLength(30).WithMessage("El Telefono1 no puede tener más de 30 caracteres.")
                .Matches(@"^[0-9\s\+\-]+$").WithMessage("El Telefono1 solo puede contener números, espacios, '+' y '-'.");

            RuleFor(x => x.Telefono2)
                .MaximumLength(30).WithMessage("El Telefono2 no puede tener más de 30 caracteres.")
                .Matches(@"^[0-9\s\+\-]*$").WithMessage("El Telefono2 solo puede contener números, espacios, '+' y '-'.");

            RuleFor(x => x.Extension)
                .MaximumLength(30).WithMessage("La Extension no puede tener más de 30 caracteres.")
                .Matches(@"^[a-zA-Z0-9\s]*$").WithMessage("La Extension solo puede contener letras, números y espacios.");
        }

        public async Task<bool> CompanyExits(long companyId)
        {
            var company = await _empresaRepository.GetById(companyId);
            return company != null;
        }
        public async Task<bool> SupplierExits(int supId)
        {
            var product = await _suplidoresService.GetById(supId);
            return product != null;
        }
    }
}
