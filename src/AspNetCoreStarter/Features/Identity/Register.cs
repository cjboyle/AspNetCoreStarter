using AspNetCoreStarter.Domain;
using AspNetCoreStarter.Infrastructure;
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
            [Remote(nameof(AccountController.VerifyUniqueEmail), "Account", ErrorMessage = "Sorry, that email is already linked to an account.")]
            [EmailAddress(ErrorMessage = "Must be a valid email address.")]
            public string Email { get; set; }

            [Required]
            [Remote(nameof(AccountController.VerifyUniqueUsername), "Account", ErrorMessage = "Sorry, that username is taken. Please choose another.")]
            [MinLength(3, ErrorMessage = "{0} must be at least {1} characters long.")]
            [MaxLength(30, ErrorMessage = "{0} can be at most {1} characters long.")]
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

            public TimeZoneInfo TimeZoneSelection { get; set; }

            [Display(Name = "Timezone")]
            public IEnumerable<SelectListItem> TimeZoneOptions => TimeZoneInfo.GetSystemTimeZones()
                .Select(tz => new SelectListItem(tz.Id, tz.StandardName, (TimeZoneSelection?.Id ?? "") == tz.Id));
            //public SelectList TimeZoneDropDown => new SelectList(TimeZoneInfo.GetSystemTimeZones(), TimeZoneSelection);
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
