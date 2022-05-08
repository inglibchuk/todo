namespace ToDo.Core
{
    public class TodoTask : EntityBase
    {
        public string? Name { get; set; }
        public int Priority { get; set; }
        public TodoTaskStatus Status { get; set; }
    }
}