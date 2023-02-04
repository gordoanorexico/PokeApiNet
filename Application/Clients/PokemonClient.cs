using PokeApiNet;
using System.Net.Http.Json;

namespace Application.Clients;
/// <summary>
/// Definition of the interface of PokemonClient for Dependency Injection
/// </summary>
public interface IPokemonClient
{
    Task<Pokemon?> GetPokemonByName(string name, CancellationToken cancellationToken);
}

/// <summary>
/// Initialization of the Client Class with the HTTP Client instance
/// </summary>
public class PokemonClient : IPokemonClient
{
    private readonly HttpClient _httpClient;

    //Injecting the client in the constructor
    public PokemonClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    /// <summary>
    /// Method for getting the Pokemon by Name from the External service of the Pokemon API
    /// </summary>
    /// <param name="name">name of the pokemon</param>
    /// <param name="cancellationToken">Optional Cancellation Token</param>
    /// <returns>Returns the corresponding Pokemon for the given ID</returns>
    public async Task<Pokemon?> GetPokemonByName(string name, CancellationToken cancellationToken)
    {
        return await _httpClient.GetFromJsonAsync<Pokemon>($"{ name }", cancellationToken);
    }
}
