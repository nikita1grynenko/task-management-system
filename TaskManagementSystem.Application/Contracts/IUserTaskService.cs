using TaskManagementSystem.Application.DTOs;
using TaskManagementSystem.Domain.Entities;
using TaskManagementSystem.Domain.Enums;

namespace TaskManagementSystem.Application.Contracts;

public interface IUserTaskService
{
    Task<UserTask> GetTaskByIdAsync(Guid taskId);

    Task<(List<UserTask>, int TotalCount)> GetTasksByFiltersAsync(Guid userId, UserTaskStatus? status,
        DateTime? dueDate,
        UserTaskPriority? priority, string sortBy,
        string sortOrder,
        int page,
        int pageSize);
    Task<UserTask> CreateTaskAsync(Guid userId, СreateTaskDto сreateTaskDto);
    Task<UserTask> UpdateTaskAsync(Guid taskId, UpdateTaskDto updateTaskDto);
    Task DeleteTaskAsync(Guid taskId);
}