using FluentValidation;
using global::BussinessLayer.DTOs.Account;
using global::BussinessLayer.Interfaces.Repository.ModuloGeneral.Empresa;
using global::BussinessLayer.Interfaces.Repository.ModuloGeneral.Seguridad;

namespace BussinessLayer.FluentValidations.Account
{

    public class ExternalRegisterRequestValidator : AbstractValidator<ExternalRegisterRequest>
    {
        private readonly IGnEmpresaRepository _empresaRepository;
        private readonly IGnPerfilRepository _gnPerfilRepository;
        private readonly IGnSucursalRepository _sucursalRepository;

        public ExternalRegisterRequestValidator(
            IGnEmpresaRepository empresaRepository,
            IGnPerfilRepository gnPerfilRepository,
            IGnSucursalRepository sucursalRepository)
        {
            _empresaRepository = empresaRepository;
            _gnPerfilRepository = gnPerfilRepository;
            _sucursalRepository = sucursalRepository;

            RuleFor(x => x.Provider)
             .IsInEnum()
             .WithMessage("Proveedor no válido");

            RuleFor(x => x.Token)
                .NotEmpty().WithMessage("El token es requerido.");

            RuleFor(x => x.CompanyId)
                .MustAsync(async (companyId, cancellation) => await CompanyExists(companyId))
                .WithMessage("El ID de la compañía no es válido.");

            RuleFor(x => x.RoleId)
                .MustAsync(async (roleId, cancellation) => await RoleExists(roleId))
                .WithMessage("El ID del rol no es válido.");

            RuleFor(x => x.SucursalId)
                .MustAsync(async (sucursalId, cancellation) => await SucursalExists(sucursalId))
                .WithMessage("El ID de la sucursal no es válido.");
        }

        private async Task<bool> CompanyExists(long companyId)
        {
            var company = await _empresaRepository.GetById(companyId);
            return company != null;
        }

        private async Task<bool> RoleExists(int id)
        {
            var role = await _gnPerfilRepository.GetById(id);
            return role != null;
        }

        private async Task<bool> SucursalExists(long id)
        {
            var sucursal = await _sucursalRepository.GetById(id);
            return sucursal != null;
        }
    }

}
