using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebSPA.Models
{
    public class CityModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CountryId { get; set; }
    }
}
