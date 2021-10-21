using Newtonsoft.Json;

namespace TrueLayer.Pokedex.Service.Configuration
{
  public class PokedexConfiguration
  {
    [JsonConstructor]
    public PokedexConfiguration(string pokemonBaseUrl)
    {
      PokemonBaseUrl = pokemonBaseUrl;
    }

    public string PokemonBaseUrl { get; }
  }
}
