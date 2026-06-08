using Mazad.SharedKernel.Common;
using Mazad.SharedKernel.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Mazad.Infrastructure.Persistence;
using Mazad.Infrastructure.Services;
using System.Reflection;

namespace Mazad.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddMazadInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<MazadDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("MazadDb")));

        services.AddHttpContextAccessor();
        services.AddScoped<ICurrentUserService, CurrentUserService>();
        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<MazadDbContext>());

        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        return services;
    }
}
