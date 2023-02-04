using Application.Handlers;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
/// <summary>
/// Controller for any Pokemon endpoint
/// </summary>
public class PokemonController : BaseApiController
{
    /// <summary>
    /// Method for getting a Pokemon by it's name
    /// </summary>
    /// <param name="name">Name of the pokemon to search</param>
    /// <returns>An error response or an OK Response with the Pokemon's information</returns>
    [HttpGet]
    public async Task<IActionResult> GetPokemon(string name, CancellationToken cancellationToken)
    {
        //with HandleResult we can simplify the logic for returning HTTP error codes or the correct response with the values
        //we use MediatR for decoupling the API layer from the Application layer
        return HandleResult(await Mediator.Send(new GetPokemonDetails.Query { Name = name }, cancellationToken));
    }
}
