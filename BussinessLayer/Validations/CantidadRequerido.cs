using BussinessLayer.DTOs.Productos;
using BussinessLayer.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace BussinessLayer.Validations
{
    public class CantidadRequerido : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var product = (CreateProductsDto) validationContext.ObjectInstance;
            if (product.EsLote)
            {
                return value == null || (int)value == 0 ? new ValidationResult("Cantidad por Loto Obligatoria") : ValidationResult.Success;
            }

            return ValidationResult.Success;
        }
    }
}
