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
    public class ApplyingService : IApplyingService
    {
        private readonly UrlsConfig _urls;
        private readonly ILogger<ApplyingService> _logger;
        private readonly HttpClient _httpClient;

        public ApplyingService(HttpClient httpClient, IOptions<UrlsConfig> config, ILogger<ApplyingService> logger)
        {
            _urls = config.Value;
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<IEnumerable<ApplyingData>> GetAll()
        {
            var url = _urls.Applying + UrlsConfig.ApplyingOperations.GetAllApplies();
            var httpResponse = await _httpClient.GetAsync(url);

            var response = await httpResponse.Content.ReadAsStringAsync();

            var jobResponse = JsonConvert.DeserializeObject<Response<IEnumerable<ApplyingData>>>(response);
            if (!jobResponse.Succeeded)
            {
                throw new ApiException(jobResponse.Message);
            }
            return jobResponse.Data;
        }

        public async Task<ApplyingData> GetById(int id)
        {
            var url = _urls.Applying + UrlsConfig.ApplyingOperations.GetApplyById(id);
            var httpResponse = await _httpClient.GetAsync(url);

            var response = await httpResponse.Content.ReadAsStringAsync();

            var jobResponse = JsonConvert.DeserializeObject<Response<ApplyingData>>(response);
            if (!jobResponse.Succeeded)
            {
                throw new ApiException(jobResponse.Message);
            }
            return jobResponse.Data;
        }
    }
}
