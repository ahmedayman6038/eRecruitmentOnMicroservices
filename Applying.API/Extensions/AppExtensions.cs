using Applying.API.Application.IntegrationEvents.EventHandling;
using Applying.API.Application.IntegrationEvents.Events;
using Applying.API.Middlewares;
using EventBus.Abstractions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Applying.API.Extensions
{
    public static class AppExtensions
    {
        public static IApplicationBuilder UseSwaggerExtension(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "eRecruitmentOnMicroservices.Appling.API");
            });
            return app;
        }

        public static IApplicationBuilder UseErrorHandlingMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ErrorHandlerMiddleware>();
            return app;
        }

        public static IApplicationBuilder SubscribeToEvents(this IApplicationBuilder app)
        {
            var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();
            eventBus.Subscribe<ApplyToJobEvent, ApplyToJobEventHandler>();
            return app;
        }
    }
}
