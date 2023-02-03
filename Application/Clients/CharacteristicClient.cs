using PokeApiNet;
using System.Net.Http.Json;

namespace Application.Clients;

public interface ICharacteristicClient
{
    Task<Characteristic> GetCharacteristicById(int id);
}

public class CharacteristicClient : ICharacteristicClient
{
    private readonly HttpClient _httpClient;

    public CharacteristicClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<Characteristic?> GetCharacteristicById(int id)
    {
        return await _httpClient.GetFromJsonAsync<Characteristic>($"{id}");
    }
}
