using Newtonsoft.Json;
using System.Collections.Generic;

namespace TrueLayer.Pokedex.Service.Dtos.Pokemon
{
  internal class Pokemon
  {
    [JsonConstructor]
    public Pokemon(int id, string name, bool isLegendary, IReadOnlyList<FlavorTextEntry>? flavorTextEntries, Habitat? habitat)
    {
      Id = id;
      Name = name;
      IsLegendary = isLegendary;
      FlavorTextEntries = flavorTextEntries;
      Habitat = habitat;
    }

    [JsonProperty("id")]
    public int Id { get; }

    [JsonProperty("name")]
    public string Name { get; }

    [JsonProperty("is_legendary")]
    public bool IsLegendary { get; }

    [JsonProperty("flavor_text_entries")]
    public IReadOnlyList<FlavorTextEntry>? FlavorTextEntries { get; }

    [JsonProperty("habit")]
    public Habitat? Habitat { get; }
  }
}