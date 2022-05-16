using ToDo.Core;
using ToDo.Data;
using ToDo.Service.Validation;

namespace ToDo.Service;

public class TodoListService : ITodoListService
{
    private readonly IRepository<TodoTask> _taskRepository;
    private readonly ITaskValidationService _validationService;

    public TodoListService(IRepository<TodoTask> taskRepository, ITaskValidationService validationService)
    {
        _taskRepository = taskRepository;
        _validationService = validationService;
    }

    public virtual async Task<ICollection<TodoTask>> GetAllTasksAsync()
    {
        return await _taskRepository.GetAllAsync();
    }

    public virtual async Task<ICollection<string>> AddTaskAsync(TodoTask task)
    {
        if (task.Priority == 1)
        {
            task.Category = TaxonomyRepository.CategoryC;
        }

        var validationResults = _validationService.Validate(task);
        if (validationResults.Count > 0)
        {
            return validationResults.Select(x=>x.Error).ToArray();
        }

        try
        {
            await _taskRepository.AddAsync(task);
        }
        catch (UniqueNameException e)
        {
            return new[] { e.Message };
        }
        
        return Array.Empty<string>();
    }

    public virtual async Task<ICollection<string>> UpdateTaskAsync(TodoTask task)
    {
        var validationResults = _validationService.Validate(task);
        if (validationResults.Count > 0)
        {
            return validationResults.Select(x=>x.Error).ToArray();
        }
        await _taskRepository.UpdateAsync(task);
        return Array.Empty<string>();
    }

    public virtual async Task<ICollection<string>> DeleteTaskAsync(Guid taskId)
    {
        var task = await _taskRepository.GetByIdAsync(taskId);
        if (task == null)
        {
            return new []{ $"Task '{taskId}' does not exist" };
        }

        if (task.Status != TodoTaskStatus.Completed)
        {
            return new []{ $"Task '{task.Name}' is not completed" };
        }

        await _taskRepository.DeleteAsync(task);
        return Array.Empty<string>();
    }
}