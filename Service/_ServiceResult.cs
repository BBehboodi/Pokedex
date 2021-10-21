using System;
using System.Collections.Generic;

namespace TrueLayer.Pokedex.Service
{
  public class ServiceResult<T>
  {
    public ServiceResult(T result)
     : this(succeeded: true, result, errors: null)
    { }

    public ServiceResult(ErrorResult error)
     : this(succeeded: false, result: default, errors: new ErrorResult[] { error })
    { }

    public ServiceResult(IReadOnlyList<ErrorResult> errors)
      : this(succeeded: false, result: default, errors)
    { }

    public ServiceResult(bool succeeded, T? result, IReadOnlyList<ErrorResult>? errors)
    {
      Succeeded = succeeded;
      Result = result;
      Errors = errors;
    }

    public bool Succeeded { get; }

    public T? Result { get; }

    public IReadOnlyList<ErrorResult>? Errors { get; }
  }
}
