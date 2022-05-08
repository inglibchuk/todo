using ToDo.Core;
using ToDo.Core.Extensions;

namespace ToDo.Service.Validation;

public class TaskNameValidator : ITaskValidator
{
    public TaskValidatorResult Validate(TodoTask? task)
    {
        if (task == null)
        {
            return new TaskValidatorResult($"The '{nameof(task)}' argument can not be empty");
        }

        return task.Name.IsNullOrEmpty()
            ? new TaskValidatorResult($"The field '{nameof(TodoTask.Name)}' should be filled")
            : TaskValidatorResult.Success;
    }
}