using PokeApiNet;
using System.Net.Http.Json;

namespace Application.Clients;
/// <summary>
/// Definition of the Interface of CharacteristicClient for Dependency Injection
/// </summary>
public interface ICharacteristicClient
{
    Task<Characteristic?> GetCharacteristicById(int id, CancellationToken cancellationToken);
}

/// <summary>
/// Initialization of the Client Class with the HTTP Client instance
/// </summary>
public class CharacteristicClient : ICharacteristicClient
{
    private readonly HttpClient _httpClient;

    //Injecting the client in the constructor
    public CharacteristicClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    /// <summary>
    /// Method for getting the Characteristic by ID from the External service of the Pokemon API
    /// </summary>
    /// <param name="id">id of the Characteristic row</param>
    /// <param name="cancellationToken">Optional Cancellation Token</param>
    /// <returns>Returns the corresponding characteristic for the given ID</returns>
    public async Task<Characteristic?> GetCharacteristicById(int id, CancellationToken cancellationToken)
    {
        return await _httpClient.GetFromJsonAsync<Characteristic>($"{id}", cancellationToken);
    }
}
