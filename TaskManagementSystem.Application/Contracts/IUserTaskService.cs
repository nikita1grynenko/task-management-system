using TaskManagementSystem.Domain.Entities;

namespace TaskManagementSystem.Application.Contracts;

public interface IUserTaskService
{
    Task<IEnumerable<UserTask>> GetTasksAsync(Guid userId, string status, DateTime? dueDate, string priority);
    Task<UserTask?> GetTaskByIdAsync(Guid id, Guid userId);
    Task CreateTaskAsync(UserTask task);
    Task UpdateTaskAsync(UserTask task);
    Task DeleteTaskAsync(Guid id, Guid userId);
}