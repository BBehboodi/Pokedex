using Newtonsoft.Json;

namespace TrueLayer.Pokedex.Service.Dtos.Pokemon
{
  internal class Language
  {
    public Language(string name)
    {
      Name = name;
    }

    [JsonProperty("name")]
    public string Name { get; }
  }
}