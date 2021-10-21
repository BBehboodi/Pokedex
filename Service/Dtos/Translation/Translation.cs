using Newtonsoft.Json;

namespace TrueLayer.Pokedex.Service.Dtos.Translation
{
  internal class Translation
  {
    [JsonConstructor]
    public Translation(Success success, Content content)
    {
      Success = success;
      Content = content;
    }

    [JsonProperty("success")]
    public Success Success { get; }

    [JsonProperty("contents")]
    public Content Content { get; }
  }
}