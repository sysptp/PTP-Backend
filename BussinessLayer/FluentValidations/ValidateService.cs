using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.FluentValidations
{
    public class ValidateService<T>(IValidator<T> validator) : IValidateService<T> where T : class
    {
        private readonly List<string> errors = new();

        public List<string> Validate(T entity)
        {
            try
            {
                var model = validator.Validate(entity);
                if(!model.IsValid)
                {
                    foreach(var error in model.Errors)
                    {
                        errors.Add(error.ErrorMessage);
                    }
                    return errors;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message, ex);
            }
        }
    }
}
