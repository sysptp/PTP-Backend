
using BussinessLayer.Interfaces.IAutenticacion;
using System.Text.RegularExpressions;

namespace BussinessLayer.Services.SAutenticacion
{
    public class RepositorySection : IRepositorySection
    {
        
        public bool TextCaractersValidation(string text)
        {
            // Expresión regular para validar que no contenga caracteres no permitidos
            string pattern = @"^[^%$#*.^~!\s]*$";
            return Regex.IsMatch(text, pattern);
        }
    }
}