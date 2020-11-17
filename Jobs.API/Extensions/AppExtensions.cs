using Jobs.API.Middlewares;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jobs.API.Extensions
{
    public static class AppExtensions
    {
        public static IApplicationBuilder UseSwaggerExtension(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "eRecruitmentOnMicroservices.Job.API");
            });
            return app;
        }

        public static IApplicationBuilder UseErrorHandlingMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ErrorHandlerMiddleware>();
            return app;
        }
    }
}
