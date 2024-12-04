using TaskManagementSystem.Application.Contracts;
using TaskManagementSystem.Domain.Entities;
using Microsoft.Extensions.Logging;
using TaskManagementSystem.Infrastructure.Contracts;

namespace TaskManagementSystem.Application.Services;

public class UserTaskService : IUserTaskService
{
    private readonly IUserTaskRepository _repository;
    private readonly ILogger<UserTaskService> _logger;

    public UserTaskService(IUserTaskRepository repository, ILogger<UserTaskService> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<IEnumerable<UserTask>> GetTasksAsync(Guid userId, string status, DateTime? dueDate, string priority)
    {
        _logger.LogInformation("Retrieving tasks for user {UserId}", userId);
        return await _repository.GetTasksAsync(userId, status, dueDate, priority);
    }

    public async Task<UserTask?> GetTaskByIdAsync(Guid id, Guid userId)
    {
        _logger.LogInformation("Retrieving task {TaskId} for user {UserId}", id, userId);
        return await _repository.GetTaskByIdAsync(id, userId);
    }

    public async Task CreateTaskAsync(UserTask task)
    {
        _logger.LogInformation("Creating a new task for user {UserId}", task.UserId);
        await _repository.CreateTaskAsync(task);
    }

    public async Task UpdateTaskAsync(UserTask task)
    {
        _logger.LogInformation("Updating task {TaskId} for user {UserId}", task.Id, task.UserId);
        await _repository.UpdateTaskAsync(task);
    }

    public async Task DeleteTaskAsync(Guid id, Guid userId)
    {
        _logger.LogInformation("Deleting task {TaskId} for user {UserId}", id, userId);
        await _repository.DeleteTaskAsync(id, userId);
    }
}