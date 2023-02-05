using System.Text.Json;

namespace ApplicationUnitTests.MockData;

/// <summary>
/// Static class that allows to read a JSON file and serialize it into a given type of object
/// </summary>
public static class JsonFileReader
{
    private static string filePath = @".\MockData\JsonFiles\";
    /// <summary>
    /// Method for the async read from the json file
    /// </summary>
    /// <typeparam name="T">Generic type of parameter allows to specify the type of object for the serialization</typeparam>
    /// <param name="filename">name of the file without extension</param>
    /// <returns>A serialized object of the specified type</returns>
    public static async Task<T> ReadAsync<T>(string filename)
    {
        using FileStream stream = File.OpenRead($"{filePath}{filename}.json");
        return await JsonSerializer.DeserializeAsync<T>(stream);
    }
}
