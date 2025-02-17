using Microsoft.EntityFrameworkCore;
using TaskManagementAPI.Data;
using TaskManagementAPI.Models;

namespace TaskManagementAPI.Services
{
   public class TaskService : ITaskService
    {
        private readonly ApplicationDbContext _context;

        public TaskService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<TodoTask> CreateTaskAsync(CreateTaskDto taskDto, int userId)
        {
            var task = new TodoTask
            {
                Title = taskDto.Title,
                Description = taskDto.Description,
                Priority = taskDto.Priority,
                Category = taskDto.Category,
                CreatedDate = DateTime.UtcNow,
                DueDate = taskDto.DueDate,
                EstimatedTime = taskDto.EstimatedTime,
                DependentTaskId = taskDto.DependentTaskId,
                Status = TaskStatus.Backlog,
                UserId = userId
            };

            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
            return task;
        }

        public async Task<TodoTask> UpdateTaskAsync(int taskId, CreateTaskDto taskDto)
        {
            var existingTask = await _context.Tasks
                .FirstOrDefaultAsync(t => t.Id == taskId);

            if (existingTask == null)
                throw new ArgumentException("Task not found");

            // Update task properties
            existingTask.Title = taskDto.Title;
            existingTask.Description = taskDto.Description;
            existingTask.Priority = taskDto.Priority;
            existingTask.Category = taskDto.Category;
            existingTask.DueDate = taskDto.DueDate;
            existingTask.EstimatedTime = taskDto.EstimatedTime;
            existingTask.DependentTaskId = taskDto.DependentTaskId;

            await _context.SaveChangesAsync();
            return existingTask;
        }

        public async Task<IEnumerable<TodoTask>> GetTasksByUserAsync(int userId)
        {
            return await _context.Tasks
                .Where(t => t.UserId == userId)
                .ToListAsync();
        }

        public async Task<TodoTask> GetTaskByIdAsync(int taskId, int userId)
        {
            return await _context.Tasks
                .FirstOrDefaultAsync(t => t.Id == taskId && t.UserId == userId);
        }

        public async Task<IEnumerable<TodoTask>> GetTasksByPriorityAsync(Priority priority, int userId)
        {
            return await _context.Tasks
                .Where(t => t.Priority == priority && t.UserId == userId)
                .ToListAsync();
        }

        public async Task<IEnumerable<TodoTask>> GetTasksByCategoryAsync(TaskCategory category, int userId)
        {
            return await _context.Tasks
                .Where(t => t.Category == category && t.UserId == userId)
                .ToListAsync();
        }

        public async Task<bool> DeleteTaskAsync(int taskId, int userId)
        {
            var task = await _context.Tasks
                .FirstOrDefaultAsync(t => t.Id == taskId && t.UserId == userId);

            if (task == null)
                return false;

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}