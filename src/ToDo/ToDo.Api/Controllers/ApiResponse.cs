namespace ToDo.Api.Controllers;

public class ApiResponse<T> : ApiResponse
{
    public T? Payload { get; set; }
}

public class ApiResponse
{
    public IEnumerable<string>? Errors { get; set; }
}