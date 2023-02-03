using PokeApiNet;
using System.Net.Http.Json;

namespace Application.Clients;

public interface IPokemonClient
{
    Task<Pokemon> GetPokemonByName(string name);
}

public class PokemonClient : IPokemonClient
{
    private readonly HttpClient _httpClient;

    public PokemonClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<Pokemon?> GetPokemonByName(string name)
    {
        return await _httpClient.GetFromJsonAsync<Pokemon>($"{ name }");
    }
}
