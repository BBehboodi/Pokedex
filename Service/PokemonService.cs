using System.Linq;
using System.Threading.Tasks;
using TrueLayer.Pokedex.Domain.Responses.Pokemon;
using TrueLayer.Pokedex.Service.Dtos.Pokemon;

namespace TrueLayer.Pokedex.Service
{
  internal class PokemonService : IPokemonService
  {
    private readonly IPokemonProxy pokemonProxy;
    private const string EN_LANG = "en";
    private const string BREAK_LINE = "\r\n";

    public PokemonService(IPokemonProxy pokemonProxy)
    {
      this.pokemonProxy = pokemonProxy;
    }

    public async Task<ServiceResult<PokemonResponse>> GetAsync(string name)
    {
      if (string.IsNullOrEmpty(name))
      {
        var errorResult = new ErrorResult(ErrorTypes.InvalidArgument, $"{nameof(name)} could not be null or empty");
        var serviceResult = new ServiceResult<PokemonResponse>(errorResult);
        return serviceResult;
      }
      else
      {
        var pokemon = await pokemonProxy.GetAsync(name);
        if (pokemon is null)
        {
          var errorResult = new ErrorResult(ErrorTypes.NotFound, $"Could not found {name}");
          var serviceResult = new ServiceResult<PokemonResponse>(errorResult);
          return serviceResult;
        }
        else
        {
          var response = ToResponse(pokemon);
          var serviceResult = new ServiceResult<PokemonResponse>(response);
          return serviceResult;
        }
      }
    }

    private static PokemonResponse ToResponse(Pokemon pokemon)
    {
      string? description = null;
      var flavorTexts = pokemon.FlavorTextEntries?
        .Where(x => x.Language.Name == EN_LANG)
        .Select(x => x.FlavorText)
        .ToArray();
      if (flavorTexts is not null)
      {
        description = string.Join(BREAK_LINE, flavorTexts);
      }
      var response = new PokemonResponse(
        pokemon.Name,
        pokemon.IsLegendary,
        pokemon.Habitat?.Name,
        description
      );
      return response;
    }
  }
}