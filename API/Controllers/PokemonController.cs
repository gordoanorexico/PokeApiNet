using Application.Handlers;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PokemonController : BaseApiController
{
    [HttpGet]
    public async Task<IActionResult> GetPokemon(string name)
    {
        return HandleResult(await Mediator.Send(new GetPokemonDetailsQuery { Name = name }));
    }
}
