using Application.Core;
using PokeApiNetDomain.Models;

namespace ApplicationUnitTests.MockData;

/// <summary>
/// Class for Mocking data from the external PokeApi services from json files with real data
/// </summary>
public class PokemonClientMock
{
    /// <summary>
    /// Read a Pokemon from a json file by name (the file with that name must be created in the MockData folder with: name_of_the_pokemon.json)
    /// </summary>
    /// <param name="name">name of the pokemon to read</param>
    /// <returns>Demo data serialized in a Pokemon object inside the success result</returns>
    public async static Task<Result<Pokemon?>> GetPokemon(string name)
    {
        var pikachu = await JsonFileReader.ReadAsync<Pokemon>(name);
        return Result<Pokemon>.Success(pikachu);
    }
    
    /// <summary>
    /// Simulate a Not Found data from a Pokemon endpoint call
    /// </summary>
    /// <returns>Result success object with a default value interpreted for the API Controller as a Not Found</returns>
    public static Result<Pokemon?> GetPokemon_NotFound()
    {
        return Result<Pokemon>.Success(default);
    }

    /// <summary>
    /// Get mock data from characteristic for value 25
    /// </summary>
    /// <returns>Result success object with a characteristic value serialized from JSON</returns>
    public async static Task<Result<Characteristic?>> GetCharacteristic25()
    {
        var characteristic = await JsonFileReader.ReadAsync<Characteristic>("characteristic25");
        return Result<Characteristic>.Success(characteristic);
    }

    /// <summary>
    /// Simulate a Not Found data from a Characteristic endpoint call
    /// </summary>
    /// <returns>Result success object with a default value interpreted for the API Controller as a Not Found</returns>
    public static Result<Characteristic?> GetCharacteristicById_NotFound()
    {
        return Result<Characteristic>.Success(default);
    }
       
}