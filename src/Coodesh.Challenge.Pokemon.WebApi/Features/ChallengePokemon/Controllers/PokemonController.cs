using System.ComponentModel.DataAnnotations;
using System.Net;
using Coodesh.Challenge.Pokemon.WebApi.Features.ChallengePokemon.Application.Constants;
using Coodesh.Challenge.Pokemon.WebApi.Features.ChallengePokemon.Application.Interfaces;
using Coodesh.Challenge.Pokemon.WebApi.Features.ChallengePokemon.Application.Models.Request;
using Coodesh.Challenge.Pokemon.WebApi.Shared.Controllers;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Coodesh.Challenge.Pokemon.WebApi.Features.ChallengePokemon.Controllers;

[ApiVersion("1.0")]
[ControllerName("pokemon")]
[Route("v{version:apiVersion}/pokemon")]
public class PokemonController : CustomControllerBase
{
    private readonly IPokemonService _challengePokemonService;

    public PokemonController(IPokemonService challengePokemonService) => _challengePokemonService = challengePokemonService;

    [SwaggerOperation(Summary = ChallengePokemonOperationSummary.GetRandomPokemons)]
    [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(IEnumerable<PokemonViewModel>), ContentTypes = new[] { "application/json" })]
    [HttpGet("/v{version:apiVersion}/get-random-pokemons", Name = "get-random-pokemons")]
    public async Task<IActionResult> GetListRandomPokemonsAsync()
    {
        var paginatedResult = await _challengePokemonService.GetListRandomPokemonAsync(CancellationToken.None);

        return new OkObjectResult(paginatedResult);
    }

    [SwaggerOperation(Summary = ChallengePokemonOperationSummary.GetPokemonById)]
    [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(IEnumerable<PokemonViewModel>), ContentTypes = new[] { "application/json" })]
    [HttpGet("/v{version:apiVersion}/get-pokemon/{id:required}", Name = "get-pokemon-id")]
    public async Task<IActionResult> GetPokemonByIdAsync([FromRoute, Required] int id)
    {
        var paginatedResult = await _challengePokemonService.GetPokemonByIdAsync(id, CancellationToken.None);

        return new OkObjectResult(paginatedResult);
    }

}
