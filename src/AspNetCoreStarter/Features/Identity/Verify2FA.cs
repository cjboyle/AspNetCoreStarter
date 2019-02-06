using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AspNetCoreStarter.Features.Identity
{
    public static class Verify2FA
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

        }

        public class CommandHandler : IRequestHandler<Command, bool>
        {
            public Task<bool> Handle(Command request, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }
        }
    }
}
