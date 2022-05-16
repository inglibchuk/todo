using ToDo.Core;
using ToDo.Data;

namespace ToDo.Service.Validation;

public class CategoryTaskValidator : ITaskValidator
{
    public TaskValidatorResult Validate(TodoTask task)
    {
        if (task.Priority == 1 && task.Category?.Id != TaxonomyRepository.CategoryC.Id)
        {
            return new TaskValidatorResult($"Task with High priority should contain category {TaxonomyRepository.CategoryC.Name}");
        }
        return TaskValidatorResult.Success;
    }
}