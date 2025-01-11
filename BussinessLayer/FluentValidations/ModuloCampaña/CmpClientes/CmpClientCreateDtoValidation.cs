using BussinessLayer.DTOs.ModuloCampaña.CmpCliente;
using FluentValidation;

namespace BussinessLayer.FluentValidations.ModuloCampaña.CmpClientes
{
    public class CmpClientCreateDtoValidation : AbstractValidator<CmpClientCreateDto>
    {
        public CmpClientCreateDtoValidation()
        {
            RuleFor(x => x.EmpresaId).NotEmpty().WithMessage("Empresa Id no puede ser nulo o vacio")
                .NotNull().WithMessage("Empresa Id no puede ser nulo o vacio")
                .GreaterThan(0).WithMessage("Empresa ID debe ser mayor a 0");

            RuleFor(x => x.UsuarioCreacion).NotEmpty().WithMessage("Ususario creacion no puede ser nulo")
                .NotNull().WithMessage("Ususario creacion no puede ser nulo")
                .MaximumLength(50).WithMessage("Usuario creacion debe tener maximo 50 caracteres")
                .MinimumLength(5).WithMessage("Usuario creacion debe tener minimo 5 caracteres");
            
            RuleFor(x => x.Nombre).NotEmpty().WithMessage("Nombre no puede ser nulo")
                .NotNull().WithMessage("Nombre no puede ser nulo")
                .MaximumLength(100).WithMessage("Nombre debe tener maximo 100 caracteres")
                .MinimumLength(5).WithMessage("Nombre debe tener minimo 5 caracteres");


        }
    }
}
