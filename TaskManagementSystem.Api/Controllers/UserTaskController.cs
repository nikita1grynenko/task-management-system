using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using TaskManagementSystem.Application.Contracts;
using TaskManagementSystem.Application.DTOs;
using TaskManagementSystem.Application.Services;
using TaskManagementSystem.Domain.Entities;

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

    [HttpGet]
    public async Task<IActionResult> GetTasks([FromQuery] string? status, [FromQuery] DateTime? dueDate, [FromQuery] string? priority, [FromQuery] int page = 1, [FromQuery] int pageSize = 10, [FromQuery] string? sortBy = "duedate", [FromQuery] string sortDirection = "asc")
    {
        var userId = GetUserId();
        var (tasks, totalCount) = await _taskService.GetTasksAsync(userId, status, dueDate, priority, page, pageSize, sortBy, sortDirection);
        return Ok(new { tasks, totalCount });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetTask(Guid id)
    {
        var userId = GetUserId();
        var task = await _taskService.GetTaskByIdAsync(userId, id);
        return Ok(task);
    }

    [HttpPost]
    public async Task<IActionResult> CreateTask([FromBody] TaskDto taskDto)
    {
        var userId = GetUserId();
        var task = await _taskService.CreateTaskAsync(userId, taskDto);
        return CreatedAtAction(nameof(GetTask), new { id = task.Id }, task);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTask(Guid id, [FromBody] TaskDto taskDto)
    {
        var userId = GetUserId();
        var updatedTask = await _taskService.UpdateTaskAsync(userId, id, taskDto);
        return Ok(updatedTask);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTask(Guid id)
    {
        var userId = GetUserId();
        await _taskService.DeleteTaskAsync(userId, id);
        return NoContent();
    }

    private Guid GetUserId()
    {
        return Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
    }
}