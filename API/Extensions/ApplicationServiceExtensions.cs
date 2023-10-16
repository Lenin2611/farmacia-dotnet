using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreRateLimit;
using Core.Interfaces;
using Infrastructure.UnitOfWork;

namespace API.Extensions;

public static class ApplicationServiceExtensions
{
    public static void ConfigureCors(this IServiceCollection services) => services.AddCors(options =>
    {
        options.AddPolicy("CorsPolicy", builder =>
        {
            builder.AllowAnyOrigin() // WithOrigins("https://domain.com")
            .AllowAnyMethod() // WithMethods("GET", "POST")
            .AllowAnyHeader(); // WithHeaders("accept", "content-type")
        });
    }); // Remember to put 'static' on the class and to add builder.Services.ConfigureCors(); and app.UseCors("CorsPolicy"); to Program.cs

    public static void ConfigureRateLimiting(this IServiceCollection services)
    {
        services.AddMemoryCache();
        services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
        services.AddInMemoryRateLimiting();
        services.Configure<IpRateLimitOptions>(options =>
        {
            options.EnableEndpointRateLimiting = true;
            options.StackBlockedRequests = false;
            options.HttpStatusCode = 429;
            options.RealIpHeader = "X-Real-IP";
            options.GeneralRules = new List<RateLimitRule>
            {
                new RateLimitRule
                {
                    Endpoint = "*",  // Si quiere usar todos ponga *
                    Period = "10s", // Periodo de tiempo para hacer peticiones
                    Limit = 2         // Numero de peticiones durante el periodo de tiempo
                }
            };
        });
    } // Remember adding builder.Services.ConfigureRateLimiting(); and builder.Services.AddAutoMapper(Assembly.GetEntryAssembly()); and app.UseIpRateLimiting(); to Program.cs

    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    } // Remember to add builder.Services.AddApplicationServices(); to Program.cs
}
