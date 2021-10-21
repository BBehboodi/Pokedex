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

    [HttpGet("{name}")]
    [ActionName("get")]
    public async Task<IActionResult> GetAsync(string name)
    {
      var serviceResult = await pokemonService.GetAsync(name);
      if (serviceResult.Succeeded)
      {
        return Ok(serviceResult.Result);
      }
      else
      {
        return BadRequest(serviceResult.Errors);
      }
    }
  }
}
