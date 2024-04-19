using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Scheduler.Application.Common.Interfaces.Authentication;
using Scheduler.Application.Common.Interfaces.Persistance;
using Scheduler.Application.Common.Interfaces.Services;
using Scheduler.Infrastructure.Authentication;
using Scheduler.Infrastructure.Persistance;
using Scheduler.Infrastructure.Persistance.Repositories;
using Scheduler.Infrastructure.Service;

namespace Scheduler.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, ConfigurationManager configurationManager)
    {
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        services.AddSingleton<IHashProvider, HashProvider>();
        services.AddRepositories(configurationManager);
        services.AddAuth(configurationManager);
        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services, ConfigurationManager configurationManager)
    {
        services.AddScoped<IUsersRepository, UsersRepository>();
        services.AddScoped<IProblemsRepository, ProblemsRepository>();
        services.AddScoped<IGroupsRepository, GroupsRepository>();
        services.AddScoped<IFinancialPlansRepository, FinancialPlansRepository>();
        services.AddDbContext<SchedulerDbContext>(options => options.UseNpgsql(configurationManager.GetConnectionString(nameof(SchedulerDbContext))));
        return services;
    }

    private static IServiceCollection AddAuth(this IServiceCollection services, ConfigurationManager configurationManager)
    {
        var jwtSettings = configurationManager.GetSection(JwtSettings.SECTION_NAME)
            .Get<JwtSettings>() ?? throw new NullReferenceException("No jwt settings specified in appsettings");
        services.AddSingleton(Options.Create(jwtSettings));
        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
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
                };
                options.MapInboundClaims = false;
            });
        return services;
    }
}