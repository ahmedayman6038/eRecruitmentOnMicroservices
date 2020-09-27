using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.API.Interfaces
{
    public interface IDateTimeService
    {
        DateTime NowUtc { get; }
    }
}
