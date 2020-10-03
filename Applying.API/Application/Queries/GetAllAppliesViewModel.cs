using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Applying.API.Application.Queries
{
    public class GetAllAppliesViewModel
    {
        public int Id { get; set; }
        public int JobId { get; set; }
        public string UserId { get; set; }
        public string Status { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
