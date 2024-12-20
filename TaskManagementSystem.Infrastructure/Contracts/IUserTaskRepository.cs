﻿using TaskManagementSystem.Domain.Entities;
using TaskManagementSystem.Domain.Enums;

namespace TaskManagementSystem.Infrastructure.Contracts;

public interface IUserTaskRepository : IGenericRepository<UserTask>
{
    Task<(List<UserTask> tasks, int totalCount)> GetTasksByFiltersAsync(Guid userId, UserTaskStatus? status,
        DateTime? dueDate,
        UserTaskPriority? priority, string sortBy,
        string sortOrder,
        int page,
        int pageSize);
}