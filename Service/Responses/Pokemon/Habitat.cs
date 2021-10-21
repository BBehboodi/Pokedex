using Newtonsoft.Json;

namespace TrueLayer.Pokedex.Service.Responses.Pokemon
{
  internal class Habitat
  {
    [JsonConstructor]
    public Habitat(string name)
    {
      Name = name;
    }

    [JsonProperty("name")]
    public string Name { get; }
  }
}