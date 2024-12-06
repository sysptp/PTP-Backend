using BussinessLayer.DTOs.ModuloGeneral.Geografia.DRegion;
using BussinessLayer.Interfaces.Repository.ModuloGeneral.Geografia;
using FluentValidation;

namespace BussinessLayer.FluentValidations.ModuloGeneral.Geografia
{
    public class RegionRequestValidator : AbstractValidator<RegionRequest>
    {
        private readonly IPaisRepository _paisRepository;

        public RegionRequestValidator(IPaisRepository paisRepository)
        {
            _paisRepository = paisRepository;

            RuleFor(x => x.Name).NotEmpty().WithMessage("El nombre de la región no puede ser vacío")
                .NotNull().WithMessage("El nombre de la región no puede ser nulo");

            RuleFor(x => x.CountryId)
             .MustAsync(async (countryId, cancellation) => await CountryExists(countryId))
             .WithMessage("El ID del municipio no es válido.");
        }

        public async Task<bool> CountryExists(int countryId)
        {
            var company = await _paisRepository.GetById(countryId);
            return company != null;
        }

    }
}
