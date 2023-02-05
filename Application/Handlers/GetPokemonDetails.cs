using Application.Core;
using Application.Clients;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.Handlers;
/// <summary>
/// Class GetPokemonDetails for grouping the Query (request), Handler and Response for the GetPokemonDetails functionality
/// </summary>
public class GetPokemonDetails
{
    /// <summary>
    /// Class for the Query parameters definition
    /// </summary>
    public class Query : IRequest<Result<Response?>>
    {
        //Name of the pokemon to search, it uses DataAnnotations for marking the Name as Required (for more complex validations we could use Fluent Validations
        [Required]
        public string Name { get; set; }
    }

    /// <summary>
    /// Handler class called by the API Controller for getting the Pokemon and Characteristic data from the external services
    /// </summary>
    public class Handler : IRequestHandler<Query, Result<Response?>>
    {
        private readonly IPokemonClient _pokemonClient;
        public Handler(IPokemonClient pokemonClient)
        {
            _pokemonClient = pokemonClient;
        }

        /// <summary>
        /// Handle Method that receives a Pokemon's name and search in the external services that Pokemon
        /// </summary>
        /// <param name="request">Encapsulates the Name parameter of the Pokemon</param>
        /// <param name="cancellationToken">Optional cancellation Token</param>
        /// <returns></returns>
        public async Task<Result<Response?>> Handle(Query request, CancellationToken cancellationToken)
        {
            var pokemonResult = await _pokemonClient.GetPokemonByName(request.Name.ToLower(), cancellationToken);
            if (pokemonResult.Value is null)
            {
                return Result<Response>.Success(default);
            }

            var characteristicResult = await _pokemonClient.GetCharacteristicById(pokemonResult.Value.Id, cancellationToken);
            
            Response response = new Response
            {
                Name = request.Name,
                Type = pokemonResult.Value.Types.FirstOrDefault()?.Type.Name ?? string.Empty,
                Description = characteristicResult.Value?.Descriptions.FirstOrDefault(x => x.Language.Name == "en")?.Description ?? string.Empty
            };
            return Result<Response?>.Success(response);
        }
    }
    /// <summary>
    /// Response object for this Handler, it will return the Name of the Pokemon, the Description and the Type
    /// </summary>
    public class Response
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
    }
}
