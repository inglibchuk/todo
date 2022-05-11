using ToDo.Core.Extensions;

namespace ToDo.Service.Validation;

public class TaskValidatorResult
{
    public TaskValidatorResult(string error = "")
    {
        Error = error;
    }

    public bool IsSuccess => Error.IsNullOrEmpty();
    public string Error { get; set; }

    public static readonly TaskValidatorResult Success = new();
}