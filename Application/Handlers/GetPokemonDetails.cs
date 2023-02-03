using Application.Core;
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
    public GetPokemonDetailsHandler()
    {
    }

    public async Task<Result<GetPokemonDetailsResponse>> Handle(GetPokemonDetailsQuery request, CancellationToken cancellationToken)
    {
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