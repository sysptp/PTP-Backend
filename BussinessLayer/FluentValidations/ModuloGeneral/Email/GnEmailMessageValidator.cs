using BussinessLayer.DTOs.ModuloGeneral.Email;
using BussinessLayer.Interfaces.Repository.ModuloGeneral.Empresa;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace BussinessLayer.Validators.ModuloGeneral.Email
{
    public class GnEmailMessageValidator : AbstractValidator<GnEmailMessageDto>
    {
        private readonly IGnEmpresaRepository _companyRepository;

        public GnEmailMessageValidator(IGnEmpresaRepository companyRepository)
        {
            _companyRepository = companyRepository;

            RuleFor(x => x.CompanyId) // o x.EmpresaId
                .GreaterThan(0)
                .WithMessage("CompanyId es requerido.");

            RuleFor(x => x.To)
                .NotNull().WithMessage("Debe especificar al menos un destinatario.")
                .Must(list => list != null && list.Any()).WithMessage("Debe especificar al menos un destinatario.");

            RuleForEach(x => x.To!)
                .NotEmpty().WithMessage("El destinatario no puede estar vacío.")
                .EmailAddress().WithMessage("El destinatario '{PropertyValue}' no tiene un formato de correo válido.");

            RuleForEach(x => x.Cc!)
                .EmailAddress().WithMessage("El CC '{PropertyValue}' no tiene un formato de correo válido.")
                .When(x => x.Cc != null && x.Cc.Any());

            RuleFor(x => x.Subject)
                .NotEmpty().WithMessage("El asunto es requerido.")
                .MaximumLength(200).WithMessage("El asunto no puede exceder 200 caracteres.");

            RuleFor(x => x.Body)
                .NotEmpty().WithMessage("El cuerpo del correo es requerido.");

            When(x => x.Attachments != null && x.Attachments.Any(), () =>
            {
                RuleForEach(x => x.Attachments!)
                    .Must(BeValidFile).WithMessage("Uno o más adjuntos no son válidos o exceden el tamaño permitido (10 MB).");
            });

            RuleFor(x => x.CompanyId) 
                .MustAsync(async (companyId, ct) =>
                {
                    return await _companyRepository.GetById(companyId) != null;
                })
                .When(x => x.CompanyId > 0) 
                .WithMessage("El identificador de la empresa no existe en la base de datos.");
        }

        private bool BeValidFile(IFormFile file)
        {
            if (file == null) return false;
            const long MaxBytes = 10 * 1024 * 1024; 
            return file.Length > 0 && file.Length <= MaxBytes;
        }
    }
}
