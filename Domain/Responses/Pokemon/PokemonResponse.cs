namespace TrueLayer.Pokedex.Domain.Responses.Pokemon
{
  public class PokemonResponse
  {
    public PokemonResponse(string name, bool isLegendary, string? habitat, string? description)
    {
      Name = name;
      IsLegendary = isLegendary;
      Habitat = habitat;
      Description = description;
    }

    public string Name { get; }

    public bool IsLegendary { get; }

    public string? Habitat { get; }

    public string? Description { get; }
  }
}