using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Fora.Domain.Models
{
    public abstract class Model : IValidatableObject
    {
        /// <summary>
        /// Throws an Error if the Object is not Valid
        /// </summary>
        public virtual void Validate()
        {
            var context = new ValidationContext(this, serviceProvider: null, items: null);
            Validator.ValidateObject(this, context);
        }

        public virtual IEnumerable<ValidationResult> Validate(ValidationContext validationContext) => new ValidationResult[0];
    }
}
