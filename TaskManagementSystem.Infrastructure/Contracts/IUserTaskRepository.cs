using TaskManagementSystem.Domain.Entities;

namespace TaskManagementSystem.Infrastructure.Contracts;

public interface IUserTaskRepository
{
    Task<UserTask?> GetByIdAsync(Guid taskId);
    IQueryable<UserTask> QueryTasks(Guid userId);
    Task AddAsync(UserTask task);
    Task UpdateAsync(UserTask task);
    Task DeleteAsync(UserTask task);
    Task<int> CountAsync(IQueryable<UserTask> query);
}