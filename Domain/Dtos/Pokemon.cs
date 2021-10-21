namespace TrueLayer.Pokedex.Domain.Dtos
{
  public class Pokemon
  {
    public Pokemon(string name, bool isLegendary, string? habitat, string? description)
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