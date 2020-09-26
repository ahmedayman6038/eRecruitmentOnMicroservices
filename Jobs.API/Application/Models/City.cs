using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jobs.API.Application.Models
{
    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CountryId { get; set; }
        public virtual Country Country { get; set; }
    }
}
