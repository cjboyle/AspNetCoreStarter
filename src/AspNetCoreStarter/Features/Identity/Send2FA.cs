using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AspNetCoreStarter.Features.Identity
{
    public static class Send2FA
    {
        public class Query : IRequest<string>
        {

        }

        public class QueryHandler : IRequestHandler<Query, string>
        {
            public Task<string> Handle(Query request, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
                return Task.Run(() => Guid.NewGuid().ToString());
            }
        }

        public class Command : IRequest<bool>
        {

        }

        public class CommandHandler : IRequestHandler<Command, bool>
        {
            public Task<bool> Handle(Command request, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
                return Task.Run(() => false);
            }
        }
    }
}
