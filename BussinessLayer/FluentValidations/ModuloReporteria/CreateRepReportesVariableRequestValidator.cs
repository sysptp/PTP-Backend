using BussinessLayer.DTOs.ModuloReporteria;
using BussinessLayer.Interfaces.Repository.ModuloGeneral.Empresa;
using BussinessLayer.Interfaces.Services.ModuloReporteria;
using BussinessLayer.Services.ModuloReportes;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BussinessLayer.FluentValidations.ModuloReporteria
{
    public class CreateRepReportesVariableRequestValidator : AbstractValidator<CreateRepReportesVariableDto>
    {
        private readonly IGnEmpresaRepository _empresaRepository;
        private readonly IRepReporteService _repReporteService;
        public CreateRepReportesVariableRequestValidator(IGnEmpresaRepository gnEmpresaRepository,
            IRepReporteService repReporteService)
        {
            _empresaRepository = gnEmpresaRepository;
            _repReporteService = repReporteService;


            // Validar que IdEmpresa no sea nulo y sea mayor que 0
            RuleFor(x => x.IdEmpresa)
                .NotNull().WithMessage("El Id de la empresa no puede ser nulo.")
                .GreaterThan(0).WithMessage("El Id de la empresa debe ser mayor que 0.")
                .MustAsync(async (idEmpresa, cancellation) => await CompanyExits(idEmpresa))
                .WithMessage("La empresa especificada no existe.");

            // IdCentroReporteria: Must be greater than 0.
            RuleFor(x => x.IdCentroReporteria)
                .NotNull().WithMessage("El Id del reporte no puede ser nulo.")
                .GreaterThan(0).WithMessage("El Id del reporte debe ser mayor que 0.")
                .MustAsync(async (id, cancellation) => await ReporteExits(id))
                .WithMessage("El reporte especificado no existe.");

            // NombreVariable: No puede ser nulo, vacío ni contener caracteres especiales.
            RuleFor(x => x.NombreVariable)
                .NotEmpty().WithMessage("NombreVariable es obligatorio.")
                .Must(SinCaracteresEspeciales).WithMessage("NombreVariable contiene caracteres inválidos.");

            // TipoVariable: No puede ser nulo o vacío.
            RuleFor(x => x.TipoVariable)
                .NotEmpty().WithMessage("TipoVariable es obligatorio.");

            // EsObligatorio: Se valida implícitamente porque es un booleano no nulo.

            // ValorPorDefecto: Es opcional, pero si está presente, no debe contener caracteres especiales.
            RuleFor(x => x.ValorPorDefecto)
                .Must(SinCaracteresEspeciales)
                .When(x => !string.IsNullOrWhiteSpace(x.ValorPorDefecto))
                .WithMessage("ValorPorDefecto contiene caracteres inválidos.");

            // Variable: No puede ser nulo, vacío ni contener caracteres especiales.
            RuleFor(x => x.Variable)
                .NotEmpty().WithMessage("Variable es obligatorio.")
                .Must(SinCaracteresEspeciales).WithMessage("Variable contiene caracteres inválidos.");
        }

        public async Task<bool> CompanyExits(long companyId)
        {
            var company = await _empresaRepository.GetById(companyId);
            return company != null;
        }

        public async Task<bool> ReporteExits(int id)
        {
            var company = await _repReporteService.GetById(id);
            return company != null;
        }

        // Método auxiliar para validar que una cadena no contiene caracteres especiales.
        private bool SinCaracteresEspeciales(string input)
        {
            // Permitir letras, números, espacios y puntuación básica.
            var regex = new Regex(@"^[a-zA-Z0-9\s.,_-]*$");
            return regex.IsMatch(input);
        }
    }
}
