using System.ComponentModel.DataAnnotations;
using DataLayer.PDbContex;
using Microsoft.EntityFrameworkCore;

namespace BussinessLayer.Validations
{
    public class NombreMarcaValido : ValidationAttribute
    {
        private readonly PDbContext _context;

        public NombreMarcaValido()
        {
            _context = new PDbContext();
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null) return new ValidationResult("Nombre de Marca Requerido");
            var result = _context.Marcas.AnyAsync(x => x.Nombre.Equals(value.ToString()) && x.IdEmpresa==long.Parse(value.ToString())).Result;

            return result ? new ValidationResult("Nombre de Marca debe ser unico") : ValidationResult.Success;
        }
    }
}
