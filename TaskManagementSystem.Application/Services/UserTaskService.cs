using Microsoft.EntityFrameworkCore;
using TaskManagementSystem.Application.Contracts;
using TaskManagementSystem.Domain.Entities;
using Microsoft.Extensions.Logging;
using TaskManagementSystem.Application.DTOs;
using TaskManagementSystem.Domain.Enums;
using TaskManagementSystem.Infrastructure.Contracts;

namespace TaskManagementSystem.Application.Services;

public class UserTaskService : IUserTaskService
{
    private readonly IUserTaskRepository _taskRepository;

    public UserTaskService(IUserTaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }
    

    public async Task<UserTask> GetTaskByIdAsync(Guid taskId)
    {
        var task = await _taskRepository.GetByIdAsync(taskId);
        if (task == null)
            throw new UnauthorizedAccessException("Task not found or access denied.");

        return task;
    }

    public async Task<List<UserTask>> GetTasksByFiltersAsync(Guid userId, UserTaskStatus? status, DateTime? dueDate,
        UserTaskPriority? priority)
    {
        var tasks = await _taskRepository.GetTasksByFiltersAsync(userId, status, dueDate, priority);
        if (tasks == null)
            throw new UnauthorizedAccessException("Tasks not found or access denied.");
        return tasks;
    }

    public async Task<UserTask> CreateTaskAsync(Guid userId, TaskDto taskDto)
    {
        var newTask = new UserTask
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            Title = taskDto.Title,
            Description = taskDto.Description,
            DueDate = taskDto.DueDate,
            Status = taskDto.Status,
            Priority = taskDto.Priority,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        await _taskRepository.AddAsync(newTask);
        return newTask;
    }

    public async Task<UserTask> UpdateTaskAsync(Guid taskId, TaskDto taskDto)
    {
        var task = await GetTaskByIdAsync(taskId);

        task.Title = taskDto.Title;
        task.Description = taskDto.Description;
        task.DueDate = taskDto.DueDate;
        task.Status = taskDto.Status;
        task.Priority = taskDto.Priority;
        task.UpdatedAt = DateTime.UtcNow;

        await _taskRepository.UpdateAsync(task);
        return task;
    }

    public async Task DeleteTaskAsync(Guid taskId)
    {
        await _taskRepository.DeleteAsync(taskId);
    }
}