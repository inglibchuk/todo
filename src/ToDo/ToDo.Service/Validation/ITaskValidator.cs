using ToDo.Core;

namespace ToDo.Service.Validation;

public interface ITaskValidator
{
    TaskValidatorResult Validate(TodoTask task);
}