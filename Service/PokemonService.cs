using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TrueLayer.Pokedex.Domain.Responses.Pokemon;
using TrueLayer.Pokedex.Service.Dtos.Pokemon;
using TrueLayer.Pokedex.Service.Dtos.Translation;

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

    public async Task<ServiceResult<PokemonResponse>> GetAsync(string name)
    {
      if (string.IsNullOrEmpty(name))
      {
        var errorResult = new ErrorResult(ErrorTypes.InvalidArgument, $"{nameof(name)} could not be null or empty");
        return new ServiceResult<PokemonResponse>(errorResult);
      }
      else
      {
        var pokemon = await pokemonProxy.GetAsync(name);
        if (pokemon is null)
        {
          var errorResult = new ErrorResult(ErrorTypes.NotFound, $"'{name}' does not exits");
          return new ServiceResult<PokemonResponse>(errorResult);
        }
        else
        {
          var response = ToResponse(pokemon);
          return new ServiceResult<PokemonResponse>(response);
        }
      }
    }

    public async Task<ServiceResult<PokemonResponse>> TranslateAsync(string name)
    {
      var pokemonResult = await GetAsync(name);
      if (!pokemonResult.Succeeded)
      {
#pragma warning disable CS8604 // Could not be null
        return new ServiceResult<PokemonResponse>(pokemonResult.Errors);
#pragma warning restore CS8604 // Could not be null
      }
      else
      {
        var pokemon = pokemonResult.Result;
        if (pokemon is null)
        {
          throw new NullReferenceException(nameof(pokemon));
        }
        else if (pokemon.Description is null)
        {
          return new ServiceResult<PokemonResponse>(pokemon);
        }
        else
        {
          Translation? translation;
          if (pokemon.IsLegendary || string.Equals(pokemon.Habitat, CAVE_HABITAT, StringComparison.OrdinalIgnoreCase))
          {
            translation = await funtranslationProxy.GetYodaTranslation(pokemon.Description);
          }
          else
          {
            translation = await funtranslationProxy.GetShakespeareTranslation(pokemon.Description);
          }
          if (translation is not null)
          {
            if (translation.Success.Total > 0)
            {
              pokemon = new PokemonResponse(
                pokemon.Name,
                pokemon.IsLegendary,
                pokemon.Habitat,
                description: translation.Content.Translated
              );
            }
          }
        }
        return new ServiceResult<PokemonResponse>(pokemon);
      }
    }

    private static PokemonResponse ToResponse(Pokemon pokemon)
    {
      string? dirtyDescription = pokemon.FlavorTextEntries?.FirstOrDefault(x => x.Language.Name == EN_LANG)?.FlavorText;
      string? description = null;
      if (dirtyDescription is not null)
      {
        description = Regex.Replace(dirtyDescription, "\n", string.Empty);
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