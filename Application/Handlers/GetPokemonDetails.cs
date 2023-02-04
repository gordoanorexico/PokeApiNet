using Application.Core;
using Application.Clients;
using MediatR;
using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Logging;

namespace Application.Handlers;
/// <summary>
/// Class GetPokemonDetails for grouping the Query (request), Handler and Response for the GetPokemonDetails functionality
/// </summary>
public class GetPokemonDetails
{
    /// <summary>
    /// Class for the Query parameters definition
    /// </summary>
    public class Query : IRequest<Result<Response>>
    {
        //Name of the pokemon to search, it uses DataAnnotations for marking the Name as Required (for more complex validations we could use Fluent Validations
        [Required]
        public string Name { get; set; }
    }

    /// <summary>
    /// Handler class called by the API Controller for getting the Pokemon and Characteristic data from the external services
    /// </summary>
    public class Handler : IRequestHandler<Query, Result<Response>>
    {
        //clients for the pokemon and characteristic services
        private readonly IPokemonClient _pokemonClient;
        private readonly ICharacteristicClient _characteristicClient;
        public Handler(IPokemonClient pokemonClient, ICharacteristicClient characteristicClient)
        {
            //services initialized by Dependency Injection
            _pokemonClient = pokemonClient;
            _characteristicClient = characteristicClient;
        }

        /// <summary>
        /// Handle Method that receives a Pokemon's name and search in the external services that Pokemon
        /// </summary>
        /// <param name="request">Encapsulates the Name parameter of the Pokemon</param>
        /// <param name="cancellationToken">Optional cancellation Token</param>
        /// <returns></returns>
        public async Task<Result<Response>> Handle(Query request, CancellationToken cancellationToken)
        {
            var pokemon = await _pokemonClient.GetPokemonByName(request.Name.ToLower(), cancellationToken);
            if (pokemon is null)
            {
                return Result<Response>.Failure($"Pokemon {request.Name} not found");
            }
                

            Response response = new Response();
            response.Name = request.Name;
            response.Type = pokemon.Types.FirstOrDefault()?.Type.Name;
            

            var characteristic = await _characteristicClient.GetCharacteristicById(pokemon.Id, cancellationToken);

            if(characteristic is null)
            {
                return Result<Response>.Failure($"Characteristic not found for Pokemon {request.Name}");
            }

            response.Description = characteristic?.Descriptions.FirstOrDefault(x => x.Language.Name == "en")?.Description;

            return Result<Response>.Success(response);
        }
    }

    public class Response
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? Type { get; set; }
    }
}
