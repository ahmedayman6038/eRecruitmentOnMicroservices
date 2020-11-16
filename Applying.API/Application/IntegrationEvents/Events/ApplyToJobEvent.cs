using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Applying.API.Application.IntegrationEvents.Events
{
    public class ApplyToJobEvent : IntegrationEvent
    {
        public ApplyToJobEvent(int jobId, string userId)
        {
            JobId = jobId;
            UserId = userId;
        }

        public int JobId { get; set; }
        public string UserId { get; set; }
    }
}
