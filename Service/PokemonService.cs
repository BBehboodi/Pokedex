using System;
using System.Linq;
using System.Threading.Tasks;
using TrueLayer.Pokedex.Domain.Dtos;
using TrueLayer.Pokedex.Service.Proxies;
using TrueLayer.Pokedex.Service.Responses.Pokemon;
using TrueLayer.Pokedex.Service.Responses.Translation;

namespace TrueLayer.Pokedex.Service
{
  internal class PokemonService : IPokemonService
  {
    private const string EN_LANG = "en";
    private const string CAVE_HABITAT = "cave";
    private readonly IPokemonProxy pokemonProxy;
    private readonly IFuntranslationProxy funtranslationProxy;

    public PokemonService(IPokemonProxy pokemonProxy, IFuntranslationProxy funtranslationProxy)
    {
      this.pokemonProxy = pokemonProxy;
      this.funtranslationProxy = funtranslationProxy;
    }

    public async Task<ServiceResult<Pokemon>> GetAsync(string name)
    {
      if (string.IsNullOrEmpty(name))
      {
        var errorResult = new ErrorResult(ErrorTypes.InvalidArgument, $"{nameof(name)} could not be null or empty");
        return new ServiceResult<Pokemon>(errorResult);
      }
      else
      {
        var pokemonResponse = await pokemonProxy.GetAsync(name);
        if (pokemonResponse is null)
        {
          var errorResult = new ErrorResult(ErrorTypes.NotFound, $"'{name}' does not exits");
          return new ServiceResult<Pokemon>(errorResult);
        }
        else
        {
          var pokemon = ToPokemon(pokemonResponse);
          return new ServiceResult<Pokemon>(pokemon);
        }
      }
    }

    public async Task<ServiceResult<Pokemon>> TranslateAsync(string name)
    {
      var pokemonResult = await GetAsync(name);
      if (!pokemonResult.Succeeded)
      {
        if (pokemonResult.Errors is null)
        {
          throw new NullReferenceException(nameof(pokemonResult.Errors));
        }
        return new ServiceResult<Pokemon>(pokemonResult.Errors);
      }

      var pokemon = pokemonResult.Result;
      if (pokemon is null)
      {
        throw new NullReferenceException(nameof(pokemon));
      }
      else if (pokemon.Description is null)
      {
        return new ServiceResult<Pokemon>(pokemon);
      }
      else
      {
        TranslationResponse? translationResponse;
        if (pokemon.IsLegendary || string.Equals(pokemon.Habitat, CAVE_HABITAT, StringComparison.OrdinalIgnoreCase))
        {
          translationResponse = await funtranslationProxy.GetYodaTranslation(pokemon.Description);
        }
        else
        {
          translationResponse = await funtranslationProxy.GetShakespeareTranslation(pokemon.Description);
        }
        if (translationResponse is null || (translationResponse.Success?.Total ?? 0) == 0)
        {
          return new ServiceResult<Pokemon>(pokemon);
        }
        else
        {
          pokemon = new Pokemon(
            pokemon.Name,
            pokemon.IsLegendary,
            pokemon.Habitat,
            description: translationResponse.Content.Translated
          );
          return new ServiceResult<Pokemon>(pokemon);
        }
      }
    }

    private static Pokemon ToPokemon(PokemonResponse pokemonResponse)
    {
      string? description = pokemonResponse
        .FlavorTextEntries
        ?.FirstOrDefault(x => string.Equals(x.Language.Name, EN_LANG))
        ?.FlavorText;
      var pokemon = new Pokemon(
        pokemonResponse.Name,
        pokemonResponse.IsLegendary,
        pokemonResponse.Habitat?.Name,
        description
      );
      return pokemon;
    }
  }
}