using Microsoft.AspNetCore.Mvc;
using ToDo.Core;
using ToDo.Service;

namespace ToDo.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly ITodoListService _taskListService;

        public TasksController(ITodoListService taskListService)
        {
            _taskListService = taskListService;
        }

        [HttpGet]
        public async Task<IEnumerable<TodoTask>> Get()
        {
            return await _taskListService.GetAllTasksAsync();
        }

        [HttpPost]
        public async Task<ApiResponse<TodoTask>> Add(TodoTask task)
        {
            var result = await _taskListService.AddTaskAsync(task);
            return result.Any()
                ? new ApiResponse<TodoTask> { Errors = result }
                : new ApiResponse<TodoTask> { Payload = task };
        }

        [HttpPut]
        public async Task<ApiResponse<TodoTask>> Update(TodoTask task)
        {
            var result = await _taskListService.UpdateTaskAsync(task);
            return result.Any()
                ? new ApiResponse<TodoTask> { Errors = result }
                : new ApiResponse<TodoTask> { Payload = task };
        }

        [HttpDelete]
        public async Task<ApiResponse> Delete(Guid taskId)
        {
            return new ApiResponse
            {
                Errors = await _taskListService.DeleteTaskAsync(taskId)
            };
        }
    }
}