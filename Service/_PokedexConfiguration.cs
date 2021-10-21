using Newtonsoft.Json;

namespace TrueLayer.Pokedex.Service
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
