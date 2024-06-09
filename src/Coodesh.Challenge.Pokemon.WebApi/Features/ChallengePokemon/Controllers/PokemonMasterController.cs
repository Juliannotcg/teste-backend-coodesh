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
[ControllerName("pokemon-master")]
[Route("v{version:apiVersion}/pokemon-master")]
public class PokemonMasterController : CustomControllerBase
{
    private readonly IPokemonMasterService _pokemonMasterService;

    public PokemonMasterController(IPokemonMasterService pokemonMasterService) => _pokemonMasterService = pokemonMasterService;

    [SwaggerOperation(Summary = ChallengePokemonOperationSummary.AddPokemonMaster)]
    [SwaggerResponse((int)HttpStatusCode.Accepted, Description = "Add pokemon master", ContentTypes = new[] { "application/json" })]
    [HttpPost("/v{version:apiVersion}/add-pokemon-master", Name = "add-pokemon-master")]
    public async Task<IActionResult> AddPokemonMasterAsync([FromBody] PokemonMasterViewModel pokemonMaster)
    {
        await _pokemonMasterService.AddPokemonMasterAsync(pokemonMaster, CancellationToken.None);

        return new OkResult();
    }

    [SwaggerOperation(Summary = ChallengePokemonOperationSummary.CapturePokemon)]
    [SwaggerResponse((int)HttpStatusCode.Accepted, Description = "Register Capture pokemon", ContentTypes = new[] { "application/json" })]
    [HttpPost("/v{version:apiVersion}/add-capture-pokemon", Name = "register-capture-pokemon")]
    public async Task<IActionResult> CapturePokemonAsync([FromBody] RegisterCapturePokemonViewModel pokemonMaster)
    {
        await _pokemonMasterService.RegisterCapturePokemonAsync(pokemonMaster, CancellationToken.None);
        return new OkResult();
    }

    [SwaggerOperation(Summary = ChallengePokemonOperationSummary.GetCapturedPokemonByIdPokemonMaster)]
    [SwaggerResponse((int)HttpStatusCode.Accepted, Description = "Get captured pokemons", ContentTypes = new[] { "application/json" })]
    [HttpGet("/v{version:apiVersion}/get-captured-pokemons/{idPokemonMaster:required}", Name = "get-captured-pokemons")]
    public async Task<IActionResult> GetCapturedPokemonAsync([FromRoute, Required] int idPokemonMaster)
    {
        var result = await _pokemonMasterService.GetCapturePokemonAsync(idPokemonMaster, CancellationToken.None);
        return new OkObjectResult(result);
    }
}
