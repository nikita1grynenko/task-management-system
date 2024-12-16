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
    
      public async Task<(IEnumerable<UserTask>, int)> GetTasksAsync(Guid userId, string? status, DateTime? dueDate, string? priority, int page, int pageSize, string? sortBy, string sortDirection)
    {
        var query = _taskRepository.QueryTasks(userId);

        // Фільтрація
        if (!string.IsNullOrEmpty(status))
            query = query.Where(t => t.Status.ToString() == status);
        if (dueDate.HasValue)
            query = query.Where(t => t.DueDate.Date == dueDate.Value.Date);
        if (!string.IsNullOrEmpty(priority))
            query = query.Where(t => t.Priority.ToString() == priority);

        // Сортування
        query = sortBy?.ToLower() switch
        {
            "duedate" => sortDirection.ToLower() == "desc"
                ? query.OrderByDescending(t => t.DueDate)
                : query.OrderBy(t => t.DueDate),

            "priority" => sortDirection.ToLower() == "desc"
                ? query.OrderByDescending(t => t.Priority)
                : query.OrderBy(t => t.Priority),

            _ => query.OrderBy(t => t.CreatedAt)
        };

        // Пагінація
        var totalCount = await _taskRepository.CountAsync(query);
        var tasks = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (tasks, totalCount);
    }

    public async Task<UserTask> GetTaskByIdAsync(Guid userId, Guid taskId)
    {
        var task = await _taskRepository.GetByIdAsync(taskId);
        if (task == null || task.UserId != userId)
            throw new UnauthorizedAccessException("Task not found or access denied.");

        return task;
    }

    public async Task<UserTask> CreateTaskAsync(Guid userId, TaskDto taskDto)
    {
        var newTask = new UserTask
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            Title = taskDto.Title,
            Description = taskDto.Description,
            DueDate = taskDto.DueDate ?? DateTime.UtcNow.AddDays(1),
            Status = taskDto.Status ?? UserTaskStatus.Pending,
            Priority = taskDto.Priority ?? UserTaskPriority.Medium,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        await _taskRepository.AddAsync(newTask);
        return newTask;
    }

    public async Task<UserTask> UpdateTaskAsync(Guid userId, Guid taskId, TaskDto taskDto)
    {
        var task = await GetTaskByIdAsync(userId, taskId);

        task.Title = taskDto.Title ?? task.Title;
        task.Description = taskDto.Description ?? task.Description;
        task.DueDate = taskDto.DueDate ?? task.DueDate;
        task.Status = taskDto.Status ?? task.Status;
        task.Priority = taskDto.Priority ?? task.Priority;
        task.UpdatedAt = DateTime.UtcNow;

        await _taskRepository.UpdateAsync(task);
        return task;
    }

    public async Task DeleteTaskAsync(Guid userId, Guid taskId)
    {
        var task = await GetTaskByIdAsync(userId, taskId);
        await _taskRepository.DeleteAsync(task);
    }
}