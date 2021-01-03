using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using WebSPA.Interfaces;
using WebSPA.Models;
using WebSPA.Wrappers;

namespace WebSPA.Services
{
    public class JobService : IJobService
    {
        private readonly HttpClient _httpClient;

        public JobService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<JobModel>> GetAllJobsAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<PagedResponse<List<JobModel>>>("job", new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            if (!response.Succeeded)
            {
                throw new ApplicationException($"response not succseded: {response.Message}");
            }
            return response.Data;

        }

        public async Task<List<CountryModel>> GetAllCountriesAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<PagedResponse<List<CountryModel>>>("country", new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            if (!response.Succeeded)
            {
                throw new ApplicationException($"response not succseded: {response.Message}");
            }
            return response.Data;
        }

        public async Task<List<CityModel>> GetAllCitiesAsync(int? countryId)
        {
            var response = await _httpClient.GetFromJsonAsync<PagedResponse<List<CityModel>>>($"city?CountryId={countryId ?? 0}", new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            if (!response.Succeeded)
            {
                throw new ApplicationException($"response not succseded: {response.Message}");
            }
            return response.Data;
        }

        public async Task<int> PostJobAsync(JobModel jobModel)
        {
            var responseMessage = await _httpClient.PostAsJsonAsync("job", jobModel);
            var response = await responseMessage.Content.ReadAsStringAsync();

            var jobResponse = JsonSerializer.Deserialize<Response<int>>(response, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            if (!jobResponse.Succeeded)
            {
                throw new ApplicationException($"response not succseded: {jobResponse.Message}");
            }
            return jobResponse.Data;
        }

        public async Task<int> PutJobAsync(int id, JobModel jobModel)
        {
            var responseMessage = await _httpClient.PutAsJsonAsync($"job/{id}", jobModel);
            var response = await responseMessage.Content.ReadAsStringAsync();

            var jobResponse = JsonSerializer.Deserialize<Response<int>>(response, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            if (!jobResponse.Succeeded)
            {
                throw new ApplicationException($"response not succseded: {jobResponse.Message}");
            }
            return jobResponse.Data;
        }

        public async Task<int> DeleteJobAsync(int id)
        {
            var responseMessage = await _httpClient.DeleteAsync($"job/{id}");
            var response = await responseMessage.Content.ReadAsStringAsync();

            var jobResponse = JsonSerializer.Deserialize<Response<int>>(response, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            if (!jobResponse.Succeeded)
            {
                throw new ApplicationException($"response not succseded: {jobResponse.Message}");
            }
            return jobResponse.Data;
        }
    }
}
