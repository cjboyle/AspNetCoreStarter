using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AspNetCoreStarter.Tests
{
    public static class ModelValidator
    {
        /// <summary>
        /// Validates the model and returns the list of results
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static List<ValidationResult> Validate(object model)
        {
            var results = new List<ValidationResult>();

            // Validate the model based on it's validation attributes
            var context = new ValidationContext(model);
            Validator.TryValidateObject(model, context, results, true);

            // Validate the model based on it's custom validation implementation
            if (model is IValidatableObject)
                results.AddRange((model as IValidatableObject).Validate(context));

            return results;
        }
    }
}
