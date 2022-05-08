using ToDo.Core;

namespace ToDo.Service.Validation;

public class TaskValidationService : ITaskValidationService
{
    public TaskValidationService()
    {
        Validators = new[] { new TaskNameValidator() };
    }

    public TaskNameValidator[] Validators { get; }

    public ICollection<TaskValidatorResult> Validate(TodoTask task)
    {
        return Validators
            .Select(x=>x.Validate(task))
            .Where(x=>!x.IsSuccess)
            .ToArray();
    }
}