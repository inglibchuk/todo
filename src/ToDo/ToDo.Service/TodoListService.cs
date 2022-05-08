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

    public virtual async Task AddTaskAsync(TodoTask task)
    {
        if (_validationService.Validate(task).Count > 0)
        {
            return;
        }
        await _taskRepository.AddAsync(task);
    }

    public virtual async Task UpdateTaskAsync(TodoTask task)
    {
        if (_validationService.Validate(task).Count > 0)
        {
            return;
        }
        await _taskRepository.UpdateAsync(task);
    }

    public virtual async Task DeleteTaskAsync(TodoTask task)
    {
        if (task.Status != TodoTaskStatus.Completed)
        {
            return;
        }

        await _taskRepository.DeleteAsync(task);
    }
}