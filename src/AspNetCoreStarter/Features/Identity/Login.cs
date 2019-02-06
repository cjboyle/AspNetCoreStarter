using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AspNetCoreStarter.Domain;
using AspNetCoreStarter.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace AspNetCoreStarter.Features.Identity
{
    public static class Login
    {
        public class Command : IRequest<SignInResult>
        {
            [Required(AllowEmptyStrings = false)]
            [Display(Name = "User Name")]
            public string UserName { get; set; }

            [DataType(DataType.Password)]
            [Required(AllowEmptyStrings = false)]
            public string Password { get; set; }

            [Display(Name = "Remember Me")]
            public bool RememberMe { get; set; }
        }

        public class CommandHandler : IRequestHandler<Command, SignInResult>
        {
            private readonly SignInManager<ApplicationUser> _manager;

            public CommandHandler(SignInManager<ApplicationUser> manager)
            {
                _manager = manager;
            }

            public async Task<SignInResult> Handle(Command request, CancellationToken cancellationToken)
            {
                return await _manager.PasswordSignInAsync(request.UserName, request.Password, request.RememberMe, false);
            }
        }
    }
}
