using Application;
using Application.Abstractions;
using Application.Services;
using Domain.Abstractions;
using FluentValidation;
using Infrastructure;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace Api.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureDb(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<RepositoryContext>(e => e.UseNpgsql(configuration.GetConnectionString("dbConnection"), 
                b => b.MigrationsAssembly("Infrastructure")));
        }

        public static void ConfigureRepositories(this IServiceCollection services)
        {
            services.AddScoped<IRepositoryManager, RepositoryManager>();
        }

        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddScoped<IAppointmentsService, AppointmentsService>();
            services.AddScoped<IResultsService, ResultsService>();
            services.AddScoped<IPdfGenerationService, PdfGenerationService>();
        }

        public static void ConfigureJwt(this IServiceCollection services, IConfiguration configuration)
        {
            var identityServerConfig = configuration
                        .GetSection("IdentityServer");

            var scopes = identityServerConfig
                        .GetSection("Scopes");

            services.AddAuthentication(config =>
                config.DefaultScheme = JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, config =>
            {
                config.Authority = identityServerConfig
                    .GetSection("Address").Value;
                config.Audience = identityServerConfig
                    .GetSection("Audience").Value;
                config.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = identityServerConfig
                        .GetSection("Address").Value,
                    ValidateIssuer = true,
                    ValidAudience = scopes.GetSection("Basic").Value,

                    ValidateAudience = true
                };
            });
        }

        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApi", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter a valid token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id="Bearer"
                            }
                        },
                        new string[]{}
                    }
                });
            });
        }

        public static void ConfigureValidators(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(AssemblyReference.Assembly);
        }
    }
}
