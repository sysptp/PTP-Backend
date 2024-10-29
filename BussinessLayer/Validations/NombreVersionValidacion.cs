using System.ComponentModel.DataAnnotations;
using DataLayer.PDbContex;
using Microsoft.EntityFrameworkCore;

namespace BussinessLayer.Validations
{
    public class NombreVersionValidacion : ValidationAttribute
    {
        private readonly PDbContext _context;

        public NombreVersionValidacion()
        {
            _context = new PDbContext();
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null) return new ValidationResult("Nombre de Version Requerido");
            var result = _context.Versiones.AnyAsync(x => x.Nombre.Equals(value.ToString())).Result;

            return result ? new ValidationResult("Nombre de Version debe ser unico") : ValidationResult.Success;         
        }
    }
}
