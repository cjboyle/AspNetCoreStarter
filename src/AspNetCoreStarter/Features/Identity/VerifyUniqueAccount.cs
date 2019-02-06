using AspNetCoreStarter.Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AspNetCoreStarter.Features.Identity
{
    public static class VerifyUniqueAccount
    {
        public class UsernameQuery : IRequest<bool>
        {
            [Required]
            public string Username { get; set; }
        }

        public class UsernameQueryHandler : IRequestHandler<UsernameQuery, bool>
        {
            private readonly UserManager<ApplicationUser> _manager;

            public UsernameQueryHandler(UserManager<ApplicationUser> manager)
            {
                _manager = manager;
            }

            public Task<bool> Handle(UsernameQuery request, CancellationToken cancellationToken)
            {
                return Task.Run(() => !_manager.Users.Any(u => u.NormalizedUserName.Equals(request.Username, StringComparison.OrdinalIgnoreCase)));
            }
        }

        public class EmailQuery : IRequest<bool>
        {
            [Required]
            public string Email { get; set; }
        }

        public class EmailQueryHandler : IRequestHandler<EmailQuery, bool>
        {
            private readonly UserManager<ApplicationUser> _manager;

            public EmailQueryHandler(UserManager<ApplicationUser> manager)
            {
                _manager = manager;
            }

            public Task<bool> Handle(EmailQuery request, CancellationToken cancellationToken)
            {
                return Task.Run(() => !_manager.Users.Any(u => u.NormalizedEmail.Equals(request.Email, StringComparison.OrdinalIgnoreCase)));
            }
        }
    }
}
