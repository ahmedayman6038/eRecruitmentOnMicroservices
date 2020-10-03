using System;

namespace Shared.Models
{
    public class ApplyMessage
    {
        public int JobId { get; set; }
        public string UserId { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
