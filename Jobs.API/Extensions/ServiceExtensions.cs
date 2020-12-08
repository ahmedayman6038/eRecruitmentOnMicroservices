using AutoMapper;
using EventBus.Abstractions;
using EventBusRabbitMQ;
using EventBusRabbitMQ.Extensions;
using EventBusRabbitMQ.Options;
using FluentValidation;
using Jobs.API.Application.Behaviors;
using Jobs.API.Application.IntegrationEvents;
using Jobs.API.Application.Interfaces;
using Jobs.API.Application.Models;
using Jobs.API.Infrastructure.Contexts;
using Jobs.API.Infrastructure.Repositories;
using Jobs.API.Infrastructure.Services;
using Jobs.API.Middlewares;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using RabbitMQ.Client;
using Serilog;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Jobs.API.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(typeof(Startup));
            services.AddValidatorsFromAssembly(typeof(Startup).Assembly);
            services.AddMediatR(typeof(Startup));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IDateTimeService, DateTimeService>();
            var rabbitMQOptions = configuration.GetSection("RabbitMQ").Get<RabbitMQOptions>();
            //services.AddRabbitMQConnection(rabbitMQOptions);
            //services.AddRabbitMQRegistration(rabbitMQOptions);
            return services;
        }

        public static IServiceCollection AddPersistenceInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<JobContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddTransient<IJobRepository, JobRepository>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            return services;
        }

        public static IServiceCollection AddIdentityInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Remove("sub");

            var identityUrl = configuration.GetValue<string>("IdentityUrl");

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(options =>
            {
                options.Authority = identityUrl;
                options.RequireHttpsMetadata = false;
                options.Audience = "jobs";
                options.Events = new JwtBearerEvents()
                {
                    OnAuthenticationFailed = context =>
                    {
                        context.NoResult();
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        context.Response.ContentType = "application/json";
                        var result = JsonConvert.SerializeObject(new Application.Wrappers.Response<string>(context.Exception.ToString()));
                        return context.Response.WriteAsync(result);
                    },
                    OnChallenge = context =>
                    {
                        context.HandleResponse();
                        context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        context.Response.ContentType = "application/json";
                        var result = JsonConvert.SerializeObject(new Application.Wrappers.Response<string>("You are not Authorized"));
                        return context.Response.WriteAsync(result);
                    },
                    OnForbidden = context =>
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                        context.Response.ContentType = "application/json";
                        var result = JsonConvert.SerializeObject(new Application.Wrappers.Response<string>("You are not authorized to access this resource"));
                        return context.Response.WriteAsync(result);
                    },
                };
            });

            //services.AddAuthorization(options =>
            //{
            //    options.AddPolicy("full_access", policy => policy.Requirements.Add(new HasScopeRequirement("jobs.full_access", identityUrl)));
            //    options.AddPolicy("read_only", policy => policy.Requirements.Add(new HasScopeRequirement("jobs.read_only", identityUrl)));
            //});

            services.AddScoped<IIdentityService, IdentityService>();
            services.AddSingleton<IAuthorizationPolicyProvider, AuthorizationPolicyProvider>();
            services.AddSingleton<IAuthorizationHandler, HasScopeHandler>();
            return services;
        }

        public static IServiceCollection AddSwaggerExtension(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "eRecruitmentOnMicroservices - Jobs.API",
                    Description = "This Api will be responsible for jobs data service.",
                    Contact = new OpenApiContact
                    {
                        Name = "Ahmed Ayman",
                        Email = "ahmedayman6038@gmail.com",
                        Url = new Uri("https://www.linkedin.com/in/ahmedayman6038/"),
                    }
                });
                options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows()
                    {
                        Implicit = new OpenApiOAuthFlow()
                        {
                            AuthorizationUrl = new Uri($"{configuration.GetValue<string>("IdentityUrl")}/connect/authorize"),
                            TokenUrl = new Uri($"{configuration.GetValue<string>("IdentityUrl")}/connect/token"),
                            Scopes = new Dictionary<string, string>()
                            {
                                { "manage", "Provides all access to jobs service" },
                                { "jobs.read", "Read data from jobs service" },
                                { "jobs.write", "Write data to jobs service" },
                                { "jobs.post", "Post jobs to jobs service" }
                            }
                        }
                    }
                });
                options.OperationFilter<AuthorizeCheckOperationFilter>();
            });
            return services;
        }
    }
}
