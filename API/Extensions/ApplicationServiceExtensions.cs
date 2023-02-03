using Application.Core;
using Application.Handlers;
using Application.Clients;
using MediatR;

namespace API.Extensions;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddSingleton<IPokemonClient, PokemonClient>();
        services.AddHttpClient<IPokemonClient, PokemonClient>(client =>
        {
            client.BaseAddress = new Uri("https://pokeapi.co/api/v2/pokemon/");
        });

        services.AddSingleton<ICharacteristicClient, CharacteristicClient>();
        services.AddHttpClient<ICharacteristicClient, CharacteristicClient>(client =>
        {
            client.BaseAddress = new Uri("https://pokeapi.co/api/v2/characteristic/");
        });


        //services.AddSingleton<ITypeClient, TypeClient>();
        //services.AddHttpClient<ITypeClient, TypeClient>(client =>
        //{
        //    client.BaseAddress = new Uri("https://pokeapi.co/api/v2/type/");
        //});

        services.AddMediatR(typeof(GetPokemonDetailsHandler).Assembly);
        services.AddAutoMapper(typeof(MappingProfiles).Assembly);

        return services;
    }
}
