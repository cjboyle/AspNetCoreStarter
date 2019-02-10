using AspNetCoreStarter.Domain;
using AspNetCoreStarter.Infrastructure;
using AspNetCoreStarter.Infrastructure.ValidationAttributes;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AspNetCoreStarter.Features.Identity
{
    public static class Register
    {
        public class Query : IRequest<bool>
        {

        }

        public class QueryHandler : IRequestHandler<Query, bool>
        {
            public Task<bool> Handle(Query request, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }
        }

        public class Command : IRequest<bool>
        {
            [Required]
            [Remote(nameof(AccountController.IsUniqueEmail), "Account", ErrorMessage = "Sorry, that email is already linked to an account.", HttpMethod = "Get")]
            [EmailAddress(ErrorMessage = "Must be a valid email address.")]
            [UniqueEmail(ErrorMessage = "Sorry, that email is already linked to an account.")]
            [Display(Name ="Email", Prompt = "example@email.com")]
            public string EmailAddress { get; set; }

            [Required]
            [Remote(nameof(AccountController.IsUniqueUsername), "Account", ErrorMessage = "Sorry, that username is taken. Please choose another.", HttpMethod = "Get")]
            [MinLength(3, ErrorMessage = "{0} must be at least {1} characters long.")]
            [MaxLength(30, ErrorMessage = "{0} can be at most {1} characters long.")]
            [UniqueUsername(ErrorMessage = "Sorry, that username is taken. Please choose another.")]
            public string Username { get; set; }

            [Required]
            [DataType(DataType.Password)]
            [MaxLength(200, ErrorMessage = "We admire your sense of security! {0}s are limited to {1} characters.")]
            [MinLength(8, ErrorMessage = "{0} must be at least 8 characters long and must contain 1 uppercase, 1 lowercase, and 1 number or special character")]
            public string Password { get; set; }

            [Required]
            [DataType(DataType.Password)]
            [Compare(nameof(Password), ErrorMessage = "The password and confirmation password do not match.")]
            [Display(Name = "Confirm Password")]
            public string ConfirmPassword { get; set; }

            public TimeZoneInfo TimeZone
            {
                get
                {
                    try { return TimeZoneInfo.FindSystemTimeZoneById(TimeZoneSelection); }
                    catch { }
                    return null;
                }
            }

            [Required]
            [Display(Name = "Time Zone")]
            public string TimeZoneSelection { get; set; }
        }

        public class CommandHandler : IRequestHandler<Command, bool>
        {
            private readonly ApplicationDbContext _db;

            public CommandHandler(ApplicationDbContext db)
            {
                _db = db;
            }

            public Task<bool> Handle(Command request, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }
        }
    }
}
