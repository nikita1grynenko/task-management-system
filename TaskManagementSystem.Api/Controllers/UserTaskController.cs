using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManagementSystem.Application.Contracts;
using TaskManagementSystem.Domain.Entities;

namespace TaskManagementSystem.Api.Controllers;

[Authorize]
[ApiController]
[Route("tasks")]
public class UserTasksController : ControllerBase
{
    private readonly IUserTaskService _taskService;

    public UserTasksController(IUserTaskService taskService)
    {
        _taskService = taskService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateTask([FromBody] UserTask task)
    {
        if (task == null) return BadRequest();

        task.CreatedAt = DateTime.UtcNow;
        task.UpdatedAt = DateTime.UtcNow;
        await _taskService.CreateTaskAsync(task);

        return CreatedAtAction(nameof(GetTaskById), new { id = task.Id }, task);
    }

    [HttpGet]
    public async Task<IActionResult> GetTasks([FromQuery] string status = null, [FromQuery] DateTime? dueDate = null, [FromQuery] string priority = null)
    {
        var userId = GetUserIdFromToken();
        var tasks = await _taskService.GetTasksAsync(userId, status, dueDate, priority);
        return Ok(tasks);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetTaskById(Guid id)
    {
        var userId = GetUserIdFromToken();
        var task = await _taskService.GetTaskByIdAsync(id, userId);
        if (task == null) return NotFound();
        return Ok(task);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTask(Guid id, [FromBody] UserTask updatedTask)
    {
        if (updatedTask == null || id != updatedTask.Id) return BadRequest();

        var userId = GetUserIdFromToken();
        var task = await _taskService.GetTaskByIdAsync(id, userId);
        if (task == null) return NotFound();

        task.Title = updatedTask.Title;
        task.Description = updatedTask.Description;
        task.DueDate = updatedTask.DueDate;
        task.Status = updatedTask.Status;
        task.Priority = updatedTask.Priority;
        await _taskService.UpdateTaskAsync(task);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTask(Guid id)
    {
        var userId = GetUserIdFromToken();
        var task = await _taskService.GetTaskByIdAsync(id, userId);
        if (task == null) return NotFound();

        await _taskService.DeleteTaskAsync(id, userId);
        return NoContent();
    }

    private Guid GetUserIdFromToken()
    {
        return Guid.Parse(User.FindFirst("UserId").Value);
    }
}