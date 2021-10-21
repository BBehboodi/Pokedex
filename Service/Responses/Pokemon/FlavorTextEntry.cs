using Newtonsoft.Json;

namespace TrueLayer.Pokedex.Service.Responses.Pokemon
{
  internal class FlavorTextEntry
  {
    [JsonConstructor]
    public FlavorTextEntry(string flavorText, Language language)
    {
      FlavorText = flavorText;
      Language = language;
    }

    [JsonProperty("flavor_text")]
    public string FlavorText { get; }

    [JsonProperty("language")]
    public Language Language { get; }
  }
}