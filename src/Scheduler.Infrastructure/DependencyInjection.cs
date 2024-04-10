using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Scheduler.Application.Common.Interfaces.Authentication;
using Scheduler.Application.Common.Interfaces.Services;
using Scheduler.Infrastructure.Authentication;
using Scheduler.Infrastructure.Service;

namespace Scheduler.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, ConfigurationManager configurationManager)
    {
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        services.AddAuth(configurationManager);
        return services;
    }

    public static IServiceCollection AddAuth(this IServiceCollection services, ConfigurationManager configurationManager)
    {
        var jwtSettings = new JwtSettings();
        configurationManager.Bind(JwtSettings.SECTION_NAME, jwtSettings);
        services.AddSingleton(Options.Create(jwtSettings));
        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings.Issuer,
                ValidAudience = jwtSettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(jwtSettings.Secret)
                )
            });
        return services;
    }
}