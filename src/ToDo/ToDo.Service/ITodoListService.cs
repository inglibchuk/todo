using ToDo.Core;

namespace ToDo.Service
{
    public interface ITodoListService
    {
        Task<ICollection<TodoTask>> GetAllTasksAsync();
        Task AddTaskAsync(TodoTask task);
        Task UpdateTaskAsync(TodoTask task);
        Task DeleteTaskAsync(TodoTask task);
    }
}
