using FluentValidation;
using BussinessLayer.DTOs.ModuloGeneral.Sucursal;
using BussinessLayer.Interfaces.Repository.Empresa;
using BussinessLayer.Interfaces.Repository.Geografia;
using BussinessLayer.Interface.IAccount;

namespace BussinessLayer.FluentValidations.ModuloGeneral.Empresas
{
    public class GnSucursalRequestValidator : AbstractValidator<GnSucursalRequest>
    {
        private readonly IGnEmpresaRepository _empresaRepository;
        private readonly IPaisRepository _paisRepository;
        private readonly IProvinciaRepository _provinciaRepository;
        private readonly IMunicipioRepository _municipioRepository;
        private readonly IRegionRepository _regionRepository;
        private readonly IAccountService _accountService;
        private readonly IGnSucursalRepository _grnSucursalRepository;

        public GnSucursalRequestValidator(
            IGnEmpresaRepository empresaRepository,
            IPaisRepository paisRepository, IProvinciaRepository provinciaRepository,
            IMunicipioRepository municipioRepository,
            IRegionRepository regionRepository, IAccountService accountService, IGnSucursalRepository grnSucursalRepository)
        {
            _empresaRepository = empresaRepository;
            _paisRepository = paisRepository;
            _provinciaRepository = provinciaRepository;
            _municipioRepository = municipioRepository;
            _regionRepository = regionRepository;
            _accountService = accountService;
            _grnSucursalRepository = grnSucursalRepository;

            RuleFor(x => x.SucursalName)
                .NotEmpty().WithMessage("El nombre de la sucursal es requerido.")
                .Length(1, 100).WithMessage("El nombre de la sucursal no debe exceder los 100 caracteres.");

            RuleFor(x => x.Phone)
                .Length(0, 15).WithMessage("El teléfono no debe exceder los 15 caracteres.")
                .When(x => !string.IsNullOrEmpty(x.Phone));

            RuleFor(x => x.Address)
                .Length(0, 250).WithMessage("La dirección no debe exceder los 250 caracteres.")
                .When(x => !string.IsNullOrEmpty(x.Address));

            RuleFor(x => x.CompanyId)
              .MustAsync(async (companyId, cancellation) => await CompanyExits(companyId))
              .WithMessage("El ID de la compañía no es válido.");
            
            RuleFor(x => x.ResponsibleUserId)
              .MustAsync(async (responsibleUserId, cancellation) => await UserExists(responsibleUserId))
              .WithMessage("El ID de el usuario Responsable de la Sucursal no es válido.");

            RuleFor(x => x.CountryId)
              .MustAsync(async (countryId, cancellation) => await CountryExists(countryId))
              .WithMessage("El ID del país no es válido.");

            RuleFor(x => x.RegionId)
              .MustAsync(async (regionId, cancellation) => await RegionExists(regionId))
              .WithMessage("El ID de la región no es válido.");

            RuleFor(x => x.ProvinceId)
              .MustAsync(async (provinciaId, cancellation) => await ProvinciaExists(provinciaId))
              .WithMessage("El ID de la provincia no es válido.");

            RuleFor(x => x.MunicipalityId)
              .MustAsync(async (municipalityId, cancellation) => await MunicipioExists(municipalityId))
              .WithMessage("El ID del municipio no es válido.");

            RuleFor(x => x.IsPrincipal)
                .NotNull()
                .WithMessage("Debe especificar si la sucursal es principal.")
                .MustAsync(async (sucursal, isPrincipal, cancellation) =>
                {
                    if (!isPrincipal) return true; 

                    return await VerifyIsOnlyOneSucursal(sucursal.CompanyId);
                })
                .WithMessage("Ya existe una sucursal principal para esta compañía.");

        }

        public async Task<bool> CompanyExits(long companyId)
        {
            var company = await _empresaRepository.GetById(companyId);
            return company != null;
        }

        public async Task<bool> VerifyIsOnlyOneSucursal(long companyId)
        {
            var sucursales = await _grnSucursalRepository.GetAll();
            var sucursalPrincipal = sucursales.Where(x => x.Principal == true && x.CodigoEmp == companyId);
            return sucursalPrincipal == null;
        }
        public async Task<bool> CountryExists(int countryId)
        {
            var country = await _paisRepository.GetById(countryId);
            return country != null;
        }
        public async Task<bool> RegionExists(int regionId)
        {
            var region = await _regionRepository.GetById(regionId);
            return region != null;
        }
        public async Task<bool> MunicipioExists(int municipioId)
        {
            var municipio = await _municipioRepository.GetById(municipioId);
            return municipio != null;
        }
        public async Task<bool> ProvinciaExists(int provinciaId)
        {
            var provincia = await _provinciaRepository.GetById(provinciaId);
            return provincia != null;
        }
        public async Task<bool> UserExists(int userId)
        {
            var exists = await _accountService.VerifyUserById(userId);
            return exists;
        }
    }
}
