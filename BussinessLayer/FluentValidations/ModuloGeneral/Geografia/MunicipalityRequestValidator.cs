using BussinessLayer.DTOs.ModuloGeneral.Configuracion.Geografia.DMunicipio;
using BussinessLayer.Interfaces.Repository.Geografia;
using FluentValidation;

namespace BussinessLayer.FluentValidations.ModuloGeneral.Geografia
{
    public class MunicipalityRequestValidator : AbstractValidator<MunicipioRequest>
    {
        private readonly IProvinciaRepository _provinciaRepository;

        public MunicipalityRequestValidator(IProvinciaRepository provinciaRepository)
        {
            _provinciaRepository = provinciaRepository;

            RuleFor(x => x.Name).NotEmpty().WithMessage("El nombre del Municipio no puede ser vacío")
                .NotNull().WithMessage("El nombre del Municipio no puede ser nulo");

            RuleFor(x => x.ProvinceId)
                .MustAsync(async (provinceId, cancellationToken) => await ProvinceExists(provinceId))
                .WithMessage("El id de la provincia no es válido");
            
        }

        public async Task<bool> ProvinceExists(int regionId)
        {
            var provincia = await _provinciaRepository.GetById(regionId);
            return provincia != null;
        }
    }
}
