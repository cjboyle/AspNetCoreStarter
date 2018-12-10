using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreStarter.Infrastructure.ValidationAttributes
{
    public class YearToDateAttribute : RangeAttribute
    {
        public YearToDateAttribute(int earliest = 0) : base(earliest, DateTime.Now.Year)
        {
        }
    }
}
