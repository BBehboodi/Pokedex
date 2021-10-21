using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TrueLayer.Pokedex.Service;

namespace TrueLayer.Pokedex.API.Controllers.v1
{
  [Route("api/v1/[controller]")]
  [ApiController]
  public class PokemonController : ControllerBase
  {
    private readonly IPokemonService pokemonService;

    public PokemonController(IPokemonService pokemonService)
    {
      this.pokemonService = pokemonService;
    }

    [ActionName("get")]
    [HttpGet("{name}")]
    public async Task<IActionResult> GetAsync(string name)
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

    [ActionName("translate")]
    [HttpGet("translated/{name}")]
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
