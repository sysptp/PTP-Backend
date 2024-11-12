using BussinessLayer.DTOs.Configuracion.Geografia.DProvincia;
using BussinessLayer.Interfaces.Repository.Geografia;
using FluentValidation;

namespace BussinessLayer.FluentValidations.ModuloGeneral.Geografia
{
    public class ProvinceRequestValidator : AbstractValidator<ProvinceRequest>
    {
        private readonly IRegionRepository _regionRepository;

        public ProvinceRequestValidator(IRegionRepository regionRepository)
        {
            _regionRepository = regionRepository;

            RuleFor(x => x.Name).NotEmpty().WithMessage("El nombre de la provincia no puede ser vacío")
                .NotNull().WithMessage("El nombre de la provincia no puede ser nulo");

            RuleFor(x => x.RegionId)
                .MustAsync(async (regionId, cancellationToken) => await RegionExists(regionId))
                .WithMessage("El id de la region no es válido");

        }

        public async Task<bool> RegionExists(int regionId)
        {
            var region = await _regionRepository.GetById(regionId);
            return region != null;
        }
    }
}
