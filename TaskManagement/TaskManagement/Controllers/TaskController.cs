using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TaskManagementAPI.DTOs;
using TaskManagementAPI.Services;

namespace TaskManagementAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TasksController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        // Get current user's ID from JWT token
        private int GetCurrentUserId()
        {
            return int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoTask>>> GetTasks()
        {
            var userId = GetCurrentUserId();
            var tasks = await _taskService.GetTasksByUserAsync(userId);
            return Ok(tasks);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TodoTask>> GetTask(int id)
        {
            var userId = GetCurrentUserId();
            var task = await _taskService.GetTaskByIdAsync(id, userId);
            
            if (task == null)
                return NotFound();

            return Ok(task);
        }

        [HttpGet("priority/{priority}")]
        public async Task<ActionResult<IEnumerable<TodoTask>>> GetTasksByPriority(Priority priority)
        {
            var userId = GetCurrentUserId();
            var tasks = await _taskService.GetTasksByPriorityAsync(priority, userId);
            return Ok(tasks);
        }

        [HttpGet("category/{category}")]
        public async Task<ActionResult<IEnumerable<TodoTask>>> GetTasksByCategory(TaskCategory category)
        {
            var userId = GetCurrentUserId();
            var tasks = await _taskService.GetTasksByCategoryAsync(category, userId);
            return Ok(tasks);
        }

        [HttpPost]
        public async Task<ActionResult<TodoTask>> CreateTask(CreateTaskDto taskDto)
        {
            var userId = GetCurrentUserId();
            var createdTask = await _taskService.CreateTaskAsync(taskDto, userId);
            return CreatedAtAction(nameof(GetTask), new { id = createdTask.Id }, createdTask);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(int id, CreateTaskDto taskDto)
        {
            try
            {
                var updatedTask = await _taskService.UpdateTaskAsync(id, taskDto);
                return Ok(updatedTask);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var userId = GetCurrentUserId();
            var result = await _taskService.DeleteTaskAsync(id, userId);
            
            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}