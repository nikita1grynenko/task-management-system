using TaskManagementSystem.Application.DTOs;
using TaskManagementSystem.Domain.Entities;

namespace TaskManagementSystem.Application.Contracts;

public interface IUserTaskService
{
    Task<(IEnumerable<UserTask>, int)> GetTasksAsync(Guid userId, string? status, DateTime? dueDate, string? priority, int page, int pageSize, string? sortBy, string sortDirection);
    Task<UserTask> GetTaskByIdAsync(Guid userId, Guid taskId);
    Task<UserTask> CreateTaskAsync(Guid userId, TaskDto taskDto);
    Task<UserTask> UpdateTaskAsync(Guid userId, Guid taskId, TaskDto taskDto);
    Task DeleteTaskAsync(Guid userId, Guid taskId);
}