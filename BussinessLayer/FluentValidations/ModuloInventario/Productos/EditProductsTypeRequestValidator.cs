using BussinessLayer.DTOs.ModuloInventario.Productos;
using BussinessLayer.Interfaces.Repository.ModuloGeneral.Empresa;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.FluentValidations.ModuloInventario.Productos
{
    public class EditProductsTypeRequestValidator : AbstractValidator<EditProductTypeDto>
    {

        private readonly IGnEmpresaRepository _empresaRepository;

        public EditProductsTypeRequestValidator(IGnEmpresaRepository gnEmpresaRepository)
        {

            _empresaRepository = gnEmpresaRepository;

            // Validation for NombreTipoProducto
            RuleFor(x => x.NombreTipoProducto)
                .NotEmpty().WithMessage("El nombre del tipo de producto no puede estar vacío.")
                .NotNull().WithMessage("El nombre del tipo de producto es obligatorio.")
                .Matches("^[a-zA-Z0-9 ]*$").WithMessage("El nombre del tipo de producto no debe contener caracteres especiales.");

            // Validar que Id sea mayor que 0
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("El Id debe ser mayor que 0.");
        }

        public async Task<bool> CompanyExits(long companyId)
        {
            var company = await _empresaRepository.GetById(companyId);
            return company != null;
        }
    }
}
