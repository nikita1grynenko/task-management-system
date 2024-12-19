using TaskManagementSystem.Application.DTOs;
using TaskManagementSystem.Domain.Entities;
using TaskManagementSystem.Domain.Enums;

namespace TaskManagementSystem.Application.Contracts;

public interface IUserTaskService
{
    Task<UserTask> GetTaskByIdAsync(Guid taskId);
    Task<List<UserTask>> GetTasksByFiltersAsync(Guid userId, UserTaskStatus? status, DateTime? dueDate,
        UserTaskPriority? priority);
    Task<UserTask> CreateTaskAsync(Guid userId, TaskDto taskDto);
    Task<UserTask> UpdateTaskAsync(Guid taskId, TaskDto taskDto);
    Task DeleteTaskAsync(Guid taskId);
}