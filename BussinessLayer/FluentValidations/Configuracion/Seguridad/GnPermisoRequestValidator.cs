using BussinessLayer.DTOs.Configuracion.Seguridad.Permiso;
using BussinessLayer.Interfaces.Repository.Empresa;
using BussinessLayer.Repository.RSeguridad;
using FluentValidation;

namespace BussinessLayer.FluentValidations.Configuracion.Seguridad
{
    public class GnPermisoRequestValidator : AbstractValidator<GnPermisoRequest>
    {
        private readonly IGnPerfilRepository _grnPerfilRepository;
        private readonly IGnEmpresaRepository _gnEmpresaRepository;
        private readonly IGnPerfilRepository _gnPerfilRepository;

        public GnPermisoRequestValidator(IGnPerfilRepository grnPerfilRepository, IGnEmpresaRepository gnEmpresaRepository, IGnPerfilRepository gnPerfilRepository)
        {
            _grnPerfilRepository = grnPerfilRepository;
            _gnEmpresaRepository = gnEmpresaRepository;
            _gnPerfilRepository = gnPerfilRepository;
        }
    }
}
