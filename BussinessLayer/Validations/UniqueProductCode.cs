using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using DataLayer.PDbContex;

namespace BussinessLayer.Validations
{
    public class UniqueProductCode : ValidationAttribute
    {
        private readonly PDbContext _context;

        public UniqueProductCode()
        {
            _context = new PDbContext();
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null) return new ValidationResult("Codigo Requerido");
            var result = _context.Productos.AnyAsync(x => x.Codigo.Equals(value.ToString())).Result;

            return result ? new ValidationResult("Codigo debe ser unico") : ValidationResult.Success;                 
        }
    }
}
