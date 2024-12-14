<<<<<<<< HEAD:BussinessLayer/FluentValidations/ModuloGeneral/Configuracion/Seguridad/GnPermisoRequestValidator.cs
﻿using BussinessLayer.DTOs.ModuloGeneral.Configuracion.Seguridad.Permiso;
using BussinessLayer.Interfaces.Repository.Configuracion.Menu;
using BussinessLayer.Interfaces.Repository.Empresa;
using BussinessLayer.Repository.REmpresa;
using BussinessLayer.Repository.RSeguridad;
using FluentValidation;

namespace BussinessLayer.FluentValidations.ModuloGeneral.Configuracion.Seguridad
========
﻿using BussinessLayer.DTOs.ModuloGeneral.Seguridad.Permiso;
using BussinessLayer.Interfaces.Repository.ModuloGeneral.Empresa;
using BussinessLayer.Interfaces.Repository.ModuloGeneral.Menu;
using BussinessLayer.Interfaces.Repository.ModuloGeneral.Seguridad;
using FluentValidation;

namespace BussinessLayer.FluentValidations.ModuloGeneral.Seguridad
>>>>>>>> REFACTOR:BussinessLayer/FluentValidations/ModuloGeneral/Seguridad/GnPermisoRequestValidator.cs
{
    public class GnPermisoRequestValidator : AbstractValidator<GnPermisoRequest>
    {
        private readonly IGnPerfilRepository _perfilRepository;
        private readonly IGnEmpresaRepository _empresaRepository;
        private readonly IGnMenuRepository _menuRepository;

        public GnPermisoRequestValidator(IGnPerfilRepository grnPerfilRepository, IGnEmpresaRepository gnEmpresaRepository, IGnMenuRepository menuRepository)
        {
            _perfilRepository = grnPerfilRepository;
            _empresaRepository = gnEmpresaRepository;
            _menuRepository = menuRepository;

            RuleFor(p => p.Create)
            .NotNull().WithMessage("El permiso de Crear no de ser nulo")
            .Must(value => value == true || value == false).WithMessage("El permiso de Crear debe ser verdadero o falso");

            RuleFor(p => p.Query)
                .NotNull().WithMessage("El permiso de Consultar no de ser nulo")
                .Must(value => value == true || value == false).WithMessage("El permiso de Consultar debe ser verdadero o falso");

            RuleFor(p => p.Edit)
                .NotNull().WithMessage("El permiso de Editar no de ser nulo")
                .Must(value => value == true || value == false).WithMessage("El permiso de Editar debe ser verdadero o falso");

            RuleFor(p => p.Delete)
              .NotNull().WithMessage("El permiso de Eliminar no de ser nulo")
              .Must(value => value == true || value == false).WithMessage("El permiso de Eliminar debe ser verdadero o falso");

            RuleFor(x => x.CompanyId)
               .MustAsync(async (companyId, cancellation) => await CompanyExits(companyId))
               .WithMessage("El ID de la compañía no es válido.");

            RuleFor(x => x.RoleId)
              .MustAsync(async (companyId, cancellation) => await RolExists(companyId))
              .WithMessage("El ID del rol no es válido.");

            RuleFor(x => x.MenuId)
              .MustAsync(async (companyId, cancellation) => await MenuExists(companyId))
              .WithMessage("El ID del menu no es válido.");

        }

        public async Task<bool> CompanyExits(long companyId)
        {
            var company = await _empresaRepository.GetById(companyId);
            return company != null;
        }

        public async Task<bool> RolExists(int roleId)
        {
            var rol = await _perfilRepository.GetById(roleId);
            return rol != null;
        }

        public async Task<bool> MenuExists(int menuId)
        {
            var menu = await _menuRepository.GetById(menuId);
            return menu != null;
        }
    }
}
