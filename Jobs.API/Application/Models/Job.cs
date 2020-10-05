using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jobs.API.Application.Models
{
    public class Job
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Requirements { get; set; }
        //public int Type { get; set; }
        //public int Vacancy { get; set; }
        //public decimal SalaryFrom { get; set; }
        //public decimal SalaryTo { get; set; }
        //public int ExperienceFrom { get; set; }
        //public int ExperienceTo { get; set; }
        //public int GenderType { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CityId { get; set; }
        public virtual City City { get; set; }
    }
}
