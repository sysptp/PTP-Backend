using FluentValidation;
using BussinessLayer.DTOs.ModuloInventario.Almacenes;
using BussinessLayer.Interfaces.Repository.ModuloGeneral.Geografia;
using BussinessLayer.Interfaces.Repository.ModuloGeneral.Seguridad;
using BussinessLayer.Interfaces.Repository.ModuloGeneral.Empresa;

namespace BussinessLayer.FluentValidations.ModuloInventario.Almacen
{
    public class InvAlmacenesRequestValidator : AbstractValidator<InvAlmacenesRequest>
    {
        private readonly IGnEmpresaRepository _empresaRepository;
        private readonly IMunicipioRepository _municipioRepository;
        private readonly IUsuarioRepository _usuarioRepository;

        public InvAlmacenesRequestValidator(
            IGnEmpresaRepository empresaRepository,
            IMunicipioRepository municipioRepository,
            IUsuarioRepository usuarioRepository)
        {
            _empresaRepository = empresaRepository;
            _municipioRepository = municipioRepository;
            _usuarioRepository = usuarioRepository;

            RuleFor(x => x.IdEmpresa)
                .GreaterThan(0).WithMessage("El Id de la empresa (IdEmpresa) es obligatorio.")
                .MustAsync(async (id, cancellation) => await _empresaRepository.GetById(id) != null)
                .WithMessage("El Id de la empresa (IdEmpresa) no existe en la base de datos.");

            RuleFor(x => x.MunicipioId)
                .GreaterThan(0).WithMessage("El Id del municipio (MunicipioId) es obligatorio.")
                .MustAsync(async (id, cancellation) => await _municipioRepository.GetById(id) != null)
                .WithMessage("El Id del municipio (MunicipioId) no existe en la base de datos.");

            RuleFor(x => x.IdUsuario)
                .GreaterThan(0).WithMessage("El Id del usuario (IdUsuario) es obligatorio.")
                .MustAsync(async (id, cancellation) => await _usuarioRepository.GetById(id) != null)
                .WithMessage("El Id del usuario (IdUsuario) no existe en la base de datos.");

            RuleFor(x => x.Nombre)
                .NotEmpty().WithMessage("El nombre es obligatorio.")
                .MaximumLength(60).WithMessage("El nombre no debe exceder los 60 caracteres.");

            RuleFor(x => x.Direccion)
                .NotEmpty().WithMessage("La dirección es obligatoria.")
                .MaximumLength(1500).WithMessage("La dirección no debe exceder los 1500 caracteres.");

            RuleFor(x => x.Telefono)
                .NotEmpty().WithMessage("El teléfono es obligatorio.")
                .MaximumLength(20).WithMessage("El teléfono no debe exceder los 20 caracteres.");

            RuleFor(x => x.EsPrincipal)
                .NotNull().WithMessage("Es obligatorio especificar si el almacén es principal o no.");
        }
    }
}
