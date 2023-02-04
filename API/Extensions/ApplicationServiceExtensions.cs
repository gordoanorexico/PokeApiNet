using Application.Handlers;
using Application.Clients;
using MediatR;

namespace API.Extensions;
/// <summary>
/// Initialization of the services needed from the Application layer
/// </summary>
public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
    {
        var apiEndpointOptions = new ApiEndpointOptions();
        config.GetSection(apiEndpointOptions.ConfigurationSectionName).Bind(apiEndpointOptions);

        //Initializing the Client with HTTP Client Factory
        services.AddSingleton<IPokemonClient, PokemonClient>();
        services.AddHttpClient<IPokemonClient, PokemonClient>(client =>
        {
            client.BaseAddress = new Uri(apiEndpointOptions.PokemonClientUrl);
        });

        //Registering the MediatR endpoints
        services.AddMediatR(typeof(GetPokemonDetails.Handler).Assembly);

        return services;
    }
}
