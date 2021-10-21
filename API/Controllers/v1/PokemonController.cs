using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TrueLayer.Pokedex.Domain.Dtos;
using TrueLayer.Pokedex.Service;

namespace TrueLayer.Pokedex.API.Controllers.v1
{
  /// <summary>
  /// Pokemon controller.
  /// </summary>
  [Produces("application/json")]
  [Route("api/v1/[controller]")]
  [ApiController]
  public class PokemonController : ControllerBase
  {
    private readonly IPokemonService pokemonService;

    /// <summary>
    /// Pokemon controller's constructor.
    /// </summary>
    public PokemonController(IPokemonService pokemonService)
    {
      this.pokemonService = pokemonService;
    }

    /// <summary>
    /// Retrieves a specific pokemon
    /// </summary>
    /// <remarks>
    /// Sample value of name
    /// 
    ///   wormadam
    /// 
    /// </remarks>
    /// <param name="name"></param>
    /// <returns>pokemon information</returns>
    /// <response code="400">If the given name is null or does not exist</response>
    [ActionName("get")]
    [HttpGet("{name}")]
    [ProducesResponseType(typeof(Pokemon), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResult[]), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pokemon>> GetAsync(string name)
    {
      var pokemonResult = await pokemonService.GetAsync(name);
      if (pokemonResult.Succeeded)
      {
        return Ok(pokemonResult.Result);
      }
      else
      {
        return BadRequest(pokemonResult.Errors);
      }
    }

    /// <summary>
    /// Translates a specific pokemon
    /// </summary>
    /// /// <remarks>
    /// Sample value of name
    /// 
    ///   wormadam
    /// 
    /// </remarks>
    /// <param name="name"></param>
    /// <returns>Translation of a the given pokemon</returns>
    /// <response code="400">If the given name is null or does not exist</response>
    [ActionName("translate")]
    [HttpGet("translated/{name}")]
    [ProducesResponseType(typeof(Pokemon), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResult[]), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> TranslateAsync(string name)
    {
      var translateResult = await pokemonService.TranslateAsync(name);
      if (translateResult.Succeeded)
      {
        return Ok(translateResult.Result);
      }
      else
      {
        return BadRequest(translateResult.Errors);
      }
    }
  }
}
