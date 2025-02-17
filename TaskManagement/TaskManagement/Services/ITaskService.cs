public interface ITaskService
{
    Task<TodoTask> CreateTaskAsync(CreateTaskDto taskDto, int userId);
    Task<TodoTask> UpdateTaskAsync(int taskId, CreateTaskDto taskDto);
    Task<IEnumerable<TodoTask>> GetTasksByUserAsync(int userId);
    Task<TodoTask> GetTaskByIdAsync(int taskId, int userId);
    Task<IEnumerable<TodoTask>> GetTasksByPriorityAsync(Priority priority, int userId);
    Task<IEnumerable<TodoTask>> GetTasksByCategoryAsync(TaskCategory category, int userId);
    Task<bool> DeleteTaskAsync(int taskId, int userId);
}