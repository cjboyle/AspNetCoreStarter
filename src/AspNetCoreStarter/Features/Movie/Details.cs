using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace AspNetCoreStarter.Features.Movie
{
    public class Details
    {
        public class Query : IRequest
        {
            [Required]
            public int? Id { get; set; }
        }
    }
}
