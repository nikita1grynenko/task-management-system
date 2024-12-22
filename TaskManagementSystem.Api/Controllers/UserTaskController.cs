﻿using Microsoft.AspNetCore.Mvc;
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
        UserTaskPriority? priority,string sortBy = "createdat", string sortOrder = "asc", int page = 1, int pageSize = 20)
    {
        var (tasks, totalCount) = await _taskService.GetTasksByFiltersAsync(id, status, dueDate, priority, sortBy, sortOrder,page, pageSize);
        
        var response = new
        {
            Data = tasks,
            Page = page,
            PageSize = pageSize,
            TotalCount = totalCount,
            TotalPages = (int)Math.Ceiling((double)totalCount / pageSize)
        };
        
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> CreateTask(Guid userId, [FromBody] СreateTaskDto сreateTaskDto)
    {
        var task = await _taskService.CreateTaskAsync(userId, сreateTaskDto);
        return CreatedAtAction(nameof(GetTask), new { id = task.Id }, task);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTask(Guid id, [FromBody] UpdateTaskDto updateTaskDto)
    {
        var updatedTask = await _taskService.UpdateTaskAsync(id, updateTaskDto);
        return Ok(updatedTask);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTask(Guid id)
    {
        await _taskService.DeleteTaskAsync(id);
        return NoContent();
    }
    
}