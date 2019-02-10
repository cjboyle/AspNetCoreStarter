using AspNetCoreStarter.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreStarter.Infrastructure.ValidationAttributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    public class UniqueUsernameAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var manager = validationContext.GetRequiredService<UserManager<ApplicationUser>>();
            if (manager.Users.Any(u => u.NormalizedUserName.Equals(value as string, StringComparison.OrdinalIgnoreCase)))
            {
                return new ValidationResult(ErrorMessage);
            }
            else
            {
                return ValidationResult.Success;
            }
        }
    }
}
