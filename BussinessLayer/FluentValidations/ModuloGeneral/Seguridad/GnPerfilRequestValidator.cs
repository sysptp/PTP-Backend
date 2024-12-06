using BussinessLayer.DTOs.ModuloGeneral.Seguridad.Perfil;
using BussinessLayer.Interfaces.Repository.ModuloGeneral.Empresa;
using FluentValidation;

namespace BussinessLayer.FluentValidations.ModuloGeneral.Seguridad
{

    public class GnPerfilRequestValidator : AbstractValidator<GnPerfilRequest>
    {
        private readonly IGnEmpresaRepository _empresaRepository;

        public GnPerfilRequestValidator(IGnEmpresaRepository _repository)
        {
            _empresaRepository = _repository;

            RuleFor(p => p.Name).NotEmpty().WithMessage("El nombre del perfil es obligatorio.");

            RuleFor(x => x.CompanyId)
               .MustAsync(async (companyId, cancellation) => await CompanyExits(companyId))
               .WithMessage("El ID de la compañía no es válido.");

        }

        public async Task<bool> CompanyExits(long companyId)
        {
            var company = await _empresaRepository.GetById(companyId);
            return company != null;
        }
    }

}
