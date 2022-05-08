using ToDo.Core;
using ToDo.Data;

namespace ToDo.Service;

public class TodoListService : ITodoListService
{
    private readonly IRepository<TodoTask> _taskRepository;

    public TodoListService(IRepository<TodoTask> taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public virtual async Task<ICollection<TodoTask>> GetAllTasksAsync()
    {
        return await _taskRepository.GetAllAsync();
    }

    public virtual Task AddTaskAsync(TodoTask task)
    {
        throw new NotImplementedException();
    }

    public virtual Task UpdateTaskAsync(TodoTask task)
    {
        throw new NotImplementedException();
    }

    public virtual Task DeleteTaskAsync(TodoTask task)
    {
        throw new NotImplementedException();
    }
}