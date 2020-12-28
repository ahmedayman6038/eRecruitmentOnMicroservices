using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebSPA.Models
{
    public class JobModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Requirements { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
    }
}
