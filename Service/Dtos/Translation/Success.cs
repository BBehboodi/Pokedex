using Newtonsoft.Json;

namespace TrueLayer.Pokedex.Service.Dtos.Translation
{
  internal class Success
  {
    [JsonConstructor]
    public Success(int total)
    {
      Total = total;
    }

    [JsonProperty("total")]
    public int Total { get; }
  }
}