using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Toolbelt.Blazor;
using WebSPA.Exceptions;

namespace WebSPA.Services
{
    public class HttpInterceptorService
    {
        private readonly HttpClientInterceptor _interceptor;
        private readonly NavigationManager _navManager;
        public HttpInterceptorService(HttpClientInterceptor interceptor, NavigationManager navManager)
        {
            _interceptor = interceptor;
            _navManager = navManager;
        }
        public void RegisterEvent() => _interceptor.AfterSend += InterceptResponse;

        private void InterceptResponse(object sender, HttpClientInterceptorEventArgs e)
        {
            string message = string.Empty;
            if (!e.Response.IsSuccessStatusCode)
            {
                var statusCode = e.Response.StatusCode;
                switch (statusCode)
                {
                    case HttpStatusCode.NotFound:
                        _navManager.NavigateTo("/error");
                        message = "The requested resorce was not found.";
                        break;
                    case HttpStatusCode.Unauthorized:
                        _navManager.NavigateTo("/error");
                        message = "User is not authorized";
                        break;
                    default:
                        _navManager.NavigateTo("/error");
                        message = "Something went wrong, please contact Administrator";
                        break;
                }
                throw new HttpResponseException(message);
            }
        }
        public void DisposeEvent() => _interceptor.AfterSend -= InterceptResponse;
    }
}
