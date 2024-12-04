using TaskManagementSystem.Domain.Entities;

namespace TaskManagementSystem.Infrastructure.Contracts;

public interface IUserTaskRepository
{
    Task<IEnumerable<UserTask>> GetTasksAsync(Guid userId, string status = null, DateTime? dueDate = null, string priority = null);
    Task<UserTask?> GetTaskByIdAsync(Guid id, Guid userId);
    Task CreateTaskAsync(UserTask task);
    Task UpdateTaskAsync(UserTask task);
    Task DeleteTaskAsync(Guid id, Guid userId);
}