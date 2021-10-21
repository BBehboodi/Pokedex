using System.Collections.Generic;

namespace TrueLayer.Pokedex.Service
{
  public class ServiceResult<T>
  {
    public ServiceResult(T result)
     : this(succeed: true, result, errors: null)
    { }

    public ServiceResult(ErrorResult error)
     : this(succeed: false, result: default, errors: new ErrorResult[] { error })
    { }

    public ServiceResult(IReadOnlyList<ErrorResult> errors)
      : this(succeed: false, result: default, errors)
    { }

    public ServiceResult(bool succeed, T? result, IReadOnlyList<ErrorResult>? errors)
    {
      Succeed = succeed;
      Result = result;
      Errors = errors;
    }

    public bool Succeed { get; }

    public T? Result { get; }

    public IReadOnlyList<ErrorResult>? Errors { get; }
  }
}
