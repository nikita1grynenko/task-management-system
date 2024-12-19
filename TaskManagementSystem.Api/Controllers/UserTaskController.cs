using Microsoft.AspNetCore.Mvc;
using TaskManagementSystem.Application.Contracts;
using TaskManagementSystem.Application.DTOs;
using TaskManagementSystem.Domain.Enums;

namespace TaskManagementSystem.Api.Controllers;

[ApiController]
[Route("tasks")]
public class UserTasksController : ControllerBase
{
    private readonly IUserTaskService _taskService;

    public UserTasksController(IUserTaskService taskService)
    {
        _taskService = taskService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetTask(Guid id, UserTaskStatus? status, DateTime? dueDate,
        UserTaskPriority? priority)
    {
        var tasks = await _taskService.GetTasksByFiltersAsync(id, status, dueDate, priority);
        return Ok(tasks);
    }

    [HttpPost]
    public async Task<IActionResult> CreateTask(Guid userId, [FromBody] TaskDto taskDto)
    {
        var task = await _taskService.CreateTaskAsync(userId, taskDto);
        return CreatedAtAction(nameof(GetTask), new { id = task.Id }, task);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTask(Guid id, [FromBody] TaskDto taskDto)
    {
        var updatedTask = await _taskService.UpdateTaskAsync(id, taskDto);
        return Ok(updatedTask);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTask(Guid id)
    {
        await _taskService.DeleteTaskAsync(id);
        return NoContent();
    }
    
}