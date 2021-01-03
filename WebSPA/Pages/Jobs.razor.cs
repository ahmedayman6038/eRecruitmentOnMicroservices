using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebSPA.Interfaces;
using WebSPA.Models;
using WebSPA.Services;

namespace WebSPA.Pages
{
    [Authorize(Roles = "Admin,SuperAdmin")]
    [Route("/job")]
    public partial class Jobs : IDisposable
    {
        [Inject]
        public IJobService JobService { get; set; }
        [Inject]
        public HttpInterceptorService Interceptor { get; set; }

        private JobModel jobModel = new JobModel();
        private List<CountryModel> countries = new List<CountryModel>();
        private List<CityModel> cities = new List<CityModel>();
        private List<JobModel> jobs;

        protected override async Task OnInitializedAsync()
        {           
            Interceptor.RegisterEvent();
            countries = await JobService.GetAllCountriesAsync();
            jobs = await JobService.GetAllJobsAsync();
        }

        private async Task OnCountryChanged(int? countryId, int? cityId)
        {
            jobModel.CountryId = countryId;
            cities = await JobService.GetAllCitiesAsync(countryId);
            jobModel.CityId = cityId;
        }

        private async void HandleValidSubmit()
        {
            if (jobModel.Id != 0)
            {
                var id = await JobService.PutJobAsync(jobModel.Id, jobModel);
                jobModel.Country = countries.Find(c => c.Id == jobModel.CountryId).Name;
                jobModel.City = cities.Find(c => c.Id == jobModel.CityId).Name;
                jobModel = new JobModel();
                StateHasChanged();
            }
            else
            {
                var id = await JobService.PostJobAsync(jobModel);
                jobModel.Id = id;
                jobModel.Country = countries.Find(c => c.Id == jobModel.CountryId).Name;
                jobModel.City = cities.Find(c => c.Id == jobModel.CityId).Name;
                jobs.Add(jobModel);
                jobModel = new JobModel();
                StateHasChanged();
            }
        }

        private async void DeleteHandler(JobModel job)
        {
            var id = await JobService.DeleteJobAsync(job.Id);
            jobs.Remove(job);
            StateHasChanged();
        }

        private async void EditHandler(JobModel job)
        {
            jobModel = job;
            await OnCountryChanged(job.CountryId, job.CityId);
            StateHasChanged();
        }

        public void Dispose() => Interceptor.DisposeEvent();
    }
}
