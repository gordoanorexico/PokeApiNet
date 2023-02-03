using Application.Core;
using Application.Clients;
using MediatR;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace Application.Handlers;

public class GetPokemonDetailsQuery : IRequest<Result<GetPokemonDetailsResponse>>
{
    [Required]
    public string Name { get; set; }
}

public class GetPokemonDetailsHandler : IRequestHandler<GetPokemonDetailsQuery, Result<GetPokemonDetailsResponse>>
{
    private readonly IPokemonClient _pokemonClient;
    public GetPokemonDetailsHandler(IPokemonClient pokemonClient)
    {
        _pokemonClient = pokemonClient;
    }

    public async Task<Result<GetPokemonDetailsResponse>> Handle(GetPokemonDetailsQuery request, CancellationToken cancellationToken)
    {
        var pokemon = await _pokemonClient.GetPokemonByName(request.Name.ToLower());

        GetPokemonDetailsResponse response = new GetPokemonDetailsResponse
        {
            Name = request.Name,
            Description = "Likes to relax",
            Type = "electric"
        };
        return Result<GetPokemonDetailsResponse>.Success(response);
    }
}

public class GetPokemonDetailsResponse
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Type { get; set; }
}