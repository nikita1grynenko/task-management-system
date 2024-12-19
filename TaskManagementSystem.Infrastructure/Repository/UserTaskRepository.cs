using Microsoft.EntityFrameworkCore;
using TaskManagementSystem.Domain.Entities;
using TaskManagementSystem.Domain.Enums;
using TaskManagementSystem.Infrastructure.Context;
using TaskManagementSystem.Infrastructure.Contracts;

namespace TaskManagementSystem.Infrastructure.Repository;

public class UserTaskRepository : GenericRepository<UserTask>, IUserTaskRepository
{
    private readonly AppDbContext _context;

    public UserTaskRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }
    
    public async Task<List<UserTask>> GetTasksByFiltersAsync(Guid userId, UserTaskStatus? status, DateTime? dueDate, UserTaskPriority? priority)
    {
        var query = _context.Tasks
            .Where(t => t.UserId == userId)
            .AsQueryable();

        if (status.HasValue)
            query = query.Where(t => t.Status == status.Value);

        if (dueDate.HasValue)
            query = query.Where(t => t.DueDate.Date == dueDate.Value.Date);

        if (priority.HasValue)
            query = query.Where(t => t.Priority == priority.Value);

        return await query.ToListAsync();
    }
}