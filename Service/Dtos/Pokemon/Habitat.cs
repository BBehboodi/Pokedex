using Newtonsoft.Json;

namespace TrueLayer.Pokedex.Service.Dtos.Pokemon
{
  internal class Habitat
  {
    [JsonConstructor]
    public Habitat(int id, string name)
    {
      Id = id;
      Name = name;
    }

    [JsonProperty("id")]
    public int Id { get; }

    [JsonProperty("name")]
    public string Name { get; }
  }
}