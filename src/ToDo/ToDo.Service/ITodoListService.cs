using ToDo.Core;

namespace ToDo.Service
{
    public interface ITodoListService
    {
        Task<ICollection<TodoTask>> GetAllTasksAsync();
        Task<ICollection<string>> AddTaskAsync(TodoTask task);
        Task<ICollection<string>> UpdateTaskAsync(TodoTask task);
        Task<ICollection<string>> DeleteTaskAsync(Guid taskId);
    }
}
