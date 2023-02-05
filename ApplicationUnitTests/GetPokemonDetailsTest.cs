using Moq;
using ApplicationUnitTests.MockData;
using Application.Clients;
using Application.Handlers;
using FluentAssertions;

namespace ApplicationUnitTests;

public class GetPokemonDetailsTest
{

    /// <summary>
    /// Unit Test for a valid Pokemon with a valid Characteristic
    /// </summary>
    [Fact]
    public async Task GetPokemon_PokemonValid_CharacteristicValid()
    {
        ///Arrange
        string pokemonName = "pikachu";
        int characteristic = 25;
        string type = "electric";
        string description = "Likes to relax";

        var cancelationToken = new CancellationToken();
        
        //Mocking the PokeApi services for testing only the Handle method behavior
        var pokemonService = new Mock<IPokemonClient>();
        pokemonService.Setup(_ => 
            _.GetPokemonByName(pokemonName, cancelationToken)).ReturnsAsync(await PokemonClientMock.GetPokemon(pokemonName)
         );
        pokemonService.Setup(_ =>
            _.GetCharacteristicById(characteristic, cancelationToken)).ReturnsAsync(await PokemonClientMock.GetCharacteristic25()
         );

        var sut = new GetPokemonDetails.Handler(pokemonService.Object);
        ///Act

        var result = await sut.Handle(new GetPokemonDetails.Query { Name = pokemonName }, cancelationToken);

        ///Assert
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeTrue();
        result.Value.Name.Should().Be(pokemonName);
        result.Value.Type.Should().Be(type);
        result.Value.Description.Should().Be(description);

    }

    /// <summary>
    /// Unit Test for a valid Pokemon with an invalid Characteristic
    /// </summary>
    [Fact]
    public async Task GetPokemon_PokemonValid_CharacteristicInValid()
    {
        ///Arrange
        string pokemonName = "pikachu";
        int characteristic = 25;
        string type = "electric";
        string description = "";

        var cancelationToken = new CancellationToken();

        //Mocking the PokeApi services for testing only the Handle method behavior
        var pokemonService = new Mock<IPokemonClient>();
        pokemonService.Setup(_ =>
            _.GetPokemonByName(pokemonName, cancelationToken)).ReturnsAsync(await PokemonClientMock.GetPokemon(pokemonName)
         );
        pokemonService.Setup(_ =>
            _.GetCharacteristicById(characteristic, cancelationToken)).ReturnsAsync(PokemonClientMock.GetCharacteristicById_NotFound()
         );

        var sut = new GetPokemonDetails.Handler(pokemonService.Object);
        ///Act

        var result = await sut.Handle(new GetPokemonDetails.Query { Name = pokemonName }, cancelationToken);

        ///Assert
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeTrue();
        result.Value.Name.Should().Be(pokemonName);
        result.Value.Type.Should().Be(type);
        result.Value.Description.Should().Be(description);
    }

    /// <summary>
    /// Unit Test for an inalid Pokemon
    /// </summary>
    [Fact]
    public async Task GetPokemon_PokemonInValid()
    {
        ///Arrange
        string pokemonName = "pokefake";

        var cancelationToken = new CancellationToken();

        //Mocking the PokeApi services for testing only the Handle method behavior
        var pokemonService = new Mock<IPokemonClient>();
        pokemonService.Setup(_ =>
            _.GetPokemonByName(pokemonName, cancelationToken)).ReturnsAsync(PokemonClientMock.GetPokemon_NotFound()
         );
        

        var sut = new GetPokemonDetails.Handler(pokemonService.Object);
        ///Act

        var result = await sut.Handle(new GetPokemonDetails.Query { Name = pokemonName }, cancelationToken);

        ///Assert
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeNull();
    }
}
