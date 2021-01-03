using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebSPA.Models;

namespace WebSPA.Interfaces
{
    public interface IJobService
    {
        Task<List<JobModel>> GetAllJobsAsync();

        Task<List<CountryModel>> GetAllCountriesAsync();

        Task<List<CityModel>> GetAllCitiesAsync(int? countryId);

        Task<int> PostJobAsync(JobModel jobModel);

        Task<int> PutJobAsync(int id,JobModel jobModel);

        Task<int> DeleteJobAsync(int id);
    }
}
