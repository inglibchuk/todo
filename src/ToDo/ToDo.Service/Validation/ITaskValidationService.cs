using ToDo.Core;

namespace ToDo.Service.Validation;

public interface ITaskValidationService
{
    ICollection<TaskValidatorResult> Validate(TodoTask task);
}