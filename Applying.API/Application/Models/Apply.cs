using Applying.API.Application.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Applying.API.Application.Models
{
    public class Apply
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int JobId { get; set; }
        public int Status { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
