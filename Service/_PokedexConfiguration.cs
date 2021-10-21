using Newtonsoft.Json;

namespace TrueLayer.Pokedex.Service
{
  public class PokedexConfiguration
  {
    [JsonConstructor]
    public PokedexConfiguration(string pokemonBaseUrl, string funtranslationsBaseUrl)
    {
      PokemonBaseUrl = pokemonBaseUrl;
      FuntranslationsBaseUrl = funtranslationsBaseUrl;
    }

    public string PokemonBaseUrl { get; }

    public string FuntranslationsBaseUrl { get; }
  }
}
