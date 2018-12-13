using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AspNetCoreStarter.Tests
{
    public static class DoValidation
    {
        /// <summary>
        /// Method used to validate an object and return an error messages associated with it
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static List<ValidationResult> On(object model)
        {
            var results = new List<ValidationResult>();

            // Validate the model based on it's properties' validation attributes
            var context = new ValidationContext(model);
            Validator.TryValidateObject(model, context, results, true);

            // Validate the model based on it's custom validation implementation
            if (model is IValidatableObject)
                results.AddRange((model as IValidatableObject).Validate(context));

            return results;
        }


        /// <summary>
        /// Method used to validate an object and return an error messages associated with it
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static List<ValidationResult> On<TModel>(TModel model)
        {
            var results = new List<ValidationResult>();

            // Validate the model based on it's properties' validation attributes
            var context = new ValidationContext(model);
            Validator.TryValidateObject(model, context, results, true);

            // Validate the model based on it's custom validation implementation
            if (model is IValidatableObject)
                results.AddRange((model as IValidatableObject).Validate(context));

            return results;
        }
    }
}
