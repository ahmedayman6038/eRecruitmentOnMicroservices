﻿using AutoMapper;
using EventBus.Abstractions;
using EventBusRabbitMQ;
using EventBusRabbitMQ.Extensions;
using EventBusRabbitMQ.Options;
using FluentValidation;
using Jobs.API.Application.Behaviors;
using Jobs.API.Application.IntegrationEvents;
using Jobs.API.Application.Interfaces;
using Jobs.API.Infrastructure.Contexts;
using Jobs.API.Infrastructure.Repositories;
using Jobs.API.Infrastructure.Services;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
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
            services.AddRabbitMQConnection(rabbitMQOptions);
            services.AddRabbitMQRegistration(rabbitMQOptions);
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
            //services.AddAuthentication(options =>
            //{
            //    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //}).AddJwtBearer(o =>
            //{
            //    o.RequireHttpsMetadata = false;
            //    o.SaveToken = false;
            //    o.TokenValidationParameters = new TokenValidationParameters
            //    {
            //        ValidateIssuerSigningKey = true,
            //        ValidateIssuer = true,
            //        ValidateAudience = true,
            //        ValidateLifetime = true,
            //        ClockSkew = TimeSpan.Zero,
            //        ValidIssuer = configuration["JWTSettings:Issuer"],
            //        ValidAudience = configuration["JWTSettings:Audience"],
            //        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWTSettings:Key"]))
            //    };
            //    o.Events = new JwtBearerEvents()
            //    {
            //        OnAuthenticationFailed = context =>
            //        {
            //            context.Response.OnStarting(async () =>
            //            {
            //                context.NoResult();
            //                context.Response.StatusCode = 500;
            //                context.Response.ContentType = "text/plain";
            //                await context.Response.WriteAsync(context.Exception.ToString());
            //            });

            //            return Task.CompletedTask;
            //        },
            //        OnChallenge = context =>
            //        {
            //            context.HandleResponse();
            //            context.Response.StatusCode = 401;
            //            context.Response.ContentType = "application/json";
            //            var result = JsonConvert.SerializeObject(new Application.Wrappers.Response<string>("You are not Authorized"));
            //            return context.Response.WriteAsync(result);
            //        },
            //        OnForbidden = context =>
            //        {
            //            context.Response.StatusCode = 403;
            //            context.Response.ContentType = "application/json";
            //            var result = JsonConvert.SerializeObject(new Application.Wrappers.Response<string>("You are not authorized to access this resource"));
            //            return context.Response.WriteAsync(result);
            //        },
            //    };
            //});
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
                        context.Response.OnStarting(async () =>
                        {
                            context.NoResult();
                            context.Response.StatusCode = 500;
                            context.Response.ContentType = "text/plain";
                            await context.Response.WriteAsync(context.Exception.ToString());
                        });

                        return Task.CompletedTask;
                    },
                    OnChallenge = context =>
                    {
                        context.HandleResponse();
                        context.Response.StatusCode = 401;
                        context.Response.ContentType = "application/json";
                        var result = JsonConvert.SerializeObject(new Application.Wrappers.Response<string>("You are not Authorized"));
                        return context.Response.WriteAsync(result);
                    },
                    OnForbidden = context =>
                    {
                        context.Response.StatusCode = 403;
                        context.Response.ContentType = "application/json";
                        var result = JsonConvert.SerializeObject(new Application.Wrappers.Response<string>("You are not authorized to access this resource"));
                        return context.Response.WriteAsync(result);
                    },
                };
            });
            services.AddScoped<IIdentityService, IdentityService>();
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
                                { "jobs", "Jobs API" }
                            }
                        }
                    }
                });
            });
            return services;
        }
    }
}
