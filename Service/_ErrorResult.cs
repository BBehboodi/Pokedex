namespace TrueLayer.Pokedex.Service
{
  public class ErrorResult
  {
    public ErrorResult(ErrorTypes type)
      : this(type, message: null)
    { }

    public ErrorResult(ErrorTypes type, string? message)
    {
      Type = type;
      Message = message;
    }

    public ErrorTypes Type { get; }

    public string? Message { get; }
  }
}