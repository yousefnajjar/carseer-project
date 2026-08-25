using CarSeer.Application.Interfaces;
using CarSeer.Infrastructure.Nhtsa;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace CarSeer.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,IConfiguration configuration)
    {
        services.Configure<NhtsaOptions>(configuration.GetSection(NhtsaOptions.SectionName));
        services.AddMemoryCache();

        services.AddHttpClient<IVehicleCatalog, VehicleCatalog>((provider, client) =>
            {
                var options = provider.GetRequiredService<IOptions<NhtsaOptions>>().Value;
                client.BaseAddress = new Uri(options.BaseUrl);
                client.Timeout = TimeSpan.FromSeconds(options.TimeoutSeconds);
                client.DefaultRequestHeaders.Accept.ParseAdd("application/json");
            })
            .AddStandardResilienceHandler();

        return services;
    }
}
