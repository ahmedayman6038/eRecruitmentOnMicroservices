// <auto-generated/>
#pragma warning disable 1591
#pragma warning disable 0414
#pragma warning disable 0649
#pragma warning disable 0169

namespace WebSPA.Pages
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
#nullable restore
#line 1 "E:\DotNetProjects\eRecruitmentOnMicroservices\WebSPA\_Imports.razor"
using System.Net.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "E:\DotNetProjects\eRecruitmentOnMicroservices\WebSPA\_Imports.razor"
using System.Net.Http.Json;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "E:\DotNetProjects\eRecruitmentOnMicroservices\WebSPA\_Imports.razor"
using Microsoft.AspNetCore.Components.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "E:\DotNetProjects\eRecruitmentOnMicroservices\WebSPA\_Imports.razor"
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "E:\DotNetProjects\eRecruitmentOnMicroservices\WebSPA\_Imports.razor"
using Microsoft.AspNetCore.Components.Forms;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "E:\DotNetProjects\eRecruitmentOnMicroservices\WebSPA\_Imports.razor"
using Microsoft.AspNetCore.Components.Routing;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "E:\DotNetProjects\eRecruitmentOnMicroservices\WebSPA\_Imports.razor"
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "E:\DotNetProjects\eRecruitmentOnMicroservices\WebSPA\_Imports.razor"
using Microsoft.AspNetCore.Components.Web.Virtualization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "E:\DotNetProjects\eRecruitmentOnMicroservices\WebSPA\_Imports.razor"
using Microsoft.AspNetCore.Components.WebAssembly.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 10 "E:\DotNetProjects\eRecruitmentOnMicroservices\WebSPA\_Imports.razor"
using Microsoft.JSInterop;

#line default
#line hidden
#nullable disable
#nullable restore
#line 11 "E:\DotNetProjects\eRecruitmentOnMicroservices\WebSPA\_Imports.razor"
using WebSPA.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 12 "E:\DotNetProjects\eRecruitmentOnMicroservices\WebSPA\_Imports.razor"
using WebSPA.Shared;

#line default
#line hidden
#nullable disable
#nullable restore
#line 13 "E:\DotNetProjects\eRecruitmentOnMicroservices\WebSPA\_Imports.razor"
using Microsoft.AspNetCore.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 14 "E:\DotNetProjects\eRecruitmentOnMicroservices\WebSPA\_Imports.razor"
using WebSPA.Wrappers;

#line default
#line hidden
#nullable disable
#nullable restore
#line 15 "E:\DotNetProjects\eRecruitmentOnMicroservices\WebSPA\_Imports.razor"
using WebSPA.Components;

#line default
#line hidden
#nullable disable
#nullable restore
#line 16 "E:\DotNetProjects\eRecruitmentOnMicroservices\WebSPA\_Imports.razor"
using System.Text.Json;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "E:\DotNetProjects\eRecruitmentOnMicroservices\WebSPA\Pages\Jobs.razor"
           [Authorize(Roles = "Admin,SuperAdmin")]

#line default
#line hidden
#nullable disable
    [Microsoft.AspNetCore.Components.RouteAttribute("/job")]
    public partial class Jobs : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
        }
        #pragma warning restore 1998
#nullable restore
#line 63 "E:\DotNetProjects\eRecruitmentOnMicroservices\WebSPA\Pages\Jobs.razor"
       
    private JobModel jobModel = new JobModel();
    private List<CountryModel> countries = new List<CountryModel>();
    private List<CityModel> cities = new List<CityModel>();
    private List<JobModel> jobs;

    protected override async Task OnInitializedAsync()
    {
        try
        {
            var response = await Http.GetFromJsonAsync<PagedResponse<List<CountryModel>>>("country");
            if (response.Succeeded)
            {
                countries = response.Data;
            }
            var response2 = await Http.GetFromJsonAsync<PagedResponse<List<JobModel>>>("job");
            if (response2.Succeeded)
            {
                jobs = response2.Data;

            }
        }
        catch (AccessTokenNotAvailableException exception)
        {
            exception.Redirect();
        }
    }

    private async Task OnCountryChanged(int? countryId, int? cityId)
    {
        try
        {
            jobModel.CountryId = countryId;
            var response = await Http.GetFromJsonAsync<PagedResponse<List<CityModel>>>($"city?CountryId={countryId ?? 0}");
            if (response.Succeeded)
            {
                cities = response.Data;
            }
            jobModel.CityId = cityId;
        }
        catch (AccessTokenNotAvailableException exception)
        {
            exception.Redirect();
        }
    }

    private async void HandleValidSubmit()
    {
        try
        {
            if(jobModel.Id != 0)
            {
                var responseMessage = await Http.PutAsJsonAsync($"job/{jobModel.Id}", jobModel);
                var response = await responseMessage.Content.ReadAsStringAsync();

                var jobResponse = JsonSerializer.Deserialize<Response<int>>(response, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                if (jobResponse.Succeeded)
                {
                    jobModel.Country = countries.Find(c => c.Id == jobModel.CountryId).Name;
                    jobModel.City = cities.Find(c => c.Id == jobModel.CityId).Name;
                    jobModel = new JobModel();
                    StateHasChanged();
                }
            }
            else
            {
                var responseMessage = await Http.PostAsJsonAsync("job", jobModel);
                var response = await responseMessage.Content.ReadAsStringAsync();

                var jobResponse = JsonSerializer.Deserialize<Response<int>>(response, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                if (jobResponse.Succeeded)
                {
                    jobModel.Id = jobResponse.Data;
                    jobModel.Country = countries.Find(c => c.Id == jobModel.CountryId).Name;
                    jobModel.City = cities.Find(c => c.Id == jobModel.CityId).Name;
                    jobs.Add(jobModel);
                    jobModel = new JobModel();
                    StateHasChanged();
                }
            }
        }
        catch (AccessTokenNotAvailableException exception)
        {
            exception.Redirect();
        }
    }

    private async void DeleteHandler(JobModel job)
    {
        try
        {
            var responseMessage = await Http.DeleteAsync($"job/{job.Id}");
            var response = await responseMessage.Content.ReadAsStringAsync();

            var jobResponse = JsonSerializer.Deserialize<Response<int>>(response, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            if (jobResponse.Succeeded)
            {
                jobs.Remove(job);
                StateHasChanged();
            }
        }
        catch (AccessTokenNotAvailableException exception)
        {
            exception.Redirect();
        }
    }

    private async void EditHandler(JobModel job)
    {
        jobModel = job;
        await OnCountryChanged(job.CountryId, job.CityId);
        //StateHasChanged();
    }

#line default
#line hidden
#nullable disable
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private HttpClient Http { get; set; }
    }
}
#pragma warning restore 1591
