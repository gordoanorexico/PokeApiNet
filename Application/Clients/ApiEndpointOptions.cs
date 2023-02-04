namespace Application.Clients;

/// <summary>
/// Record for apply the options pattern and add strong typing for configurations comming from the appsettings file
/// </summary>
/// <param name="PokemonClientUrl"></param>
/// <param name="CharacteristicClientUrl"></param>
public class ApiEndpointOptions
{
    ///Property with the name of the section in the appsettings file with the urls for the external endpoints
    public string ConfigurationSectionName { get; init; } = "ApiEndpoints";
    //URL of the Pokemon service
    public string PokemonClientUrl { get; set; } = string.Empty;
}
