using Application.Core;
using PokeApiNetDomain.Models;
using System.Net.Http.Json;
using System.Text.Json;

namespace Application.Clients;
/// <summary>
/// Definition of the interface of PokemonClient for Dependency Injection
/// </summary>
public interface IPokemonClient
{
    Task<Result<Pokemon?>> GetPokemonByName(string name, CancellationToken cancellationToken);
    Task<Result<Characteristic?>> GetCharacteristicById(int id, CancellationToken cancellationToken);
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
    public async Task<Result<Pokemon?>> GetPokemonByName(string name, CancellationToken cancellationToken)
    {
        return await GetFromService<Pokemon>(name, "pokemon", cancellationToken);
    }

    /// <summary>
    /// Method for getting the Characteristic by ID from the External service of the Pokemon API
    /// </summary>
    /// <param name="id">id of the Characteristic row</param>
    /// <param name="cancellationToken">Optional Cancellation Token</param>
    /// <returns>Returns the corresponding characteristic for the given ID</returns>
    public async Task<Result<Characteristic?>> GetCharacteristicById(int id, CancellationToken cancellationToken)
    {
        return await GetFromService<Characteristic>(id.ToString(), "characteristic", cancellationToken);
    }

    /// <summary>
    /// Internal method for service invoke and controlling the possible exceptions or a Not Found response
    /// </summary>
    /// <typeparam name="T">Generic type for returning any kind of object from the service</typeparam>
    /// <param name="parameter">The parameter in the HTTP call</param>
    /// <param name="endpoint">The endpoint of the service</param>
    /// <param name="cancellationToken">Optional Cancellation Token</param>
    /// <returns>it returns a Result with a value of type T</returns>
    protected async Task<Result<T?>> GetFromService<T>(string parameter, string endpoint, CancellationToken cancellationToken)
    {
        //It could use GetJsonAsync<T>, but when the response is not found it throws an Exception,
        //it can be capturated in a try block with HTTPRequestException, but Exceptions have a cost in performance, so it's better to avoid them
        //if an Exception ocurrs anyway in the call, the Exception Handler will manage it
        var response = await _httpClient.GetAsync($"{endpoint}/{parameter}", cancellationToken);
        if (response.IsSuccessStatusCode)
        {
            var data = await response.Content.ReadFromJsonAsync<T>();
            return Result<T>.Success(data);
        }
        if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return Result<T>.Success(default);
        }
        else
        {
            return Result<T>.Failure(response.Content?.ToString() ?? "Server error");
        }
    }
}
