using Application.Core;
using PokeApiNetDomain.Models;
using System.Text.Json;

namespace ApplicationUnitTests.MockData;


public class PokemonClientMock
{
    public async static Task<Result<Pokemon?>> GetPokemon(string name)
    {
        var pikachu = await JsonFileReader.ReadAsync<Pokemon>($@".\MockData\{name}.json");
        return Result<Pokemon>.Success(pikachu);
    }
    

    public static Result<Pokemon?> GetPokemon_NotFound()
    {
        return Result<Pokemon>.Success(default);
    }

    public async static Task<Result<Characteristic?>> GetCharacteristic25()
    {
        var characteristic = await JsonFileReader.ReadAsync<Characteristic>(@".\MockData\characteristic25.json");
        return Result<Characteristic>.Success(characteristic);
    }

    public static Result<Characteristic?> GetCharacteristicById_NotFound()
    {
        return Result<Characteristic>.Success(default);
    }

    private static class JsonFileReader
    {
        public static async Task<T> ReadAsync<T>(string filePath)
        {
            using FileStream stream = File.OpenRead(filePath);
            return await JsonSerializer.DeserializeAsync<T>(stream);
        }
    }
       
}