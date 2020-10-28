using ApiGateway.Config;
using ApiGateway.Exceptions;
using ApiGateway.Interfaces;
using ApiGateway.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ApiGateway.Services
{
    public class JobsService : IJobsService
    {
        private readonly UrlsConfig _urls;
        private readonly ILogger<JobsService> _logger;
        private readonly HttpClient _httpClient;

        public JobsService(HttpClient httpClient, IOptions<UrlsConfig> config, ILogger<JobsService> logger)
        {
            _urls = config.Value;
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<IEnumerable<JobData>> GetAll()
        {
            var url = _urls.Jobs + UrlsConfig.JobsOperations.GetAllJobs();
            var httpResponse = await _httpClient.GetAsync(url);

            var response = await httpResponse.Content.ReadAsStringAsync();

            var jobResponse = JsonConvert.DeserializeObject<Response<IEnumerable<JobData>>>(response);
            if (!jobResponse.Succeeded)
            {
                throw new ApiException(jobResponse.Message);
            }
            return jobResponse.Data;
        }

        public async Task<JobData> GetById(int id)
        {
            var url = _urls.Jobs + UrlsConfig.JobsOperations.GetJobById(id);
            var httpResponse = await _httpClient.GetAsync(url);

            var response = await httpResponse.Content.ReadAsStringAsync();

            var jobResponse = JsonConvert.DeserializeObject<Response<JobData>>(response);
            if (!jobResponse.Succeeded)
            {
                throw new ApiException(jobResponse.Message);
            }
            return jobResponse.Data;
        }
    }
}
