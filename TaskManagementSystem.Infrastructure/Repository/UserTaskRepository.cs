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
    
    public async Task<(List<UserTask> tasks, int totalCount)> GetTasksByFiltersAsync(Guid userId,
        UserTaskStatus? status, DateTime? dueDate, UserTaskPriority? priority, string sortBy,
        string sortOrder,
        int page,
        int pageSize)
    {
        var query = _context.Tasks
            .Where(t => t.UserId == userId)
            .AsQueryable();

        // Фільтрація
        if (status.HasValue)
            query = query.Where(t => t.Status == status.Value);

        if (dueDate.HasValue)
            query = query.Where(t => t.DueDate.Date == dueDate.Value.Date);

        if (priority.HasValue)
            query = query.Where(t => t.Priority == priority.Value);

        // Загальна кількість записів
        var totalCount = await query.CountAsync();

        // Сортування
        switch (sortBy?.ToLower())
        {
            case "duedate":
                query = sortOrder.ToLower() == "desc"
                    ? query.OrderByDescending(t => t.DueDate)
                    : query.OrderBy(t => t.DueDate);
                break;

            case "priority":
                query = sortOrder.ToLower() == "desc"
                    ? query.OrderByDescending(t => t.Priority)
                    : query.OrderBy(t => t.Priority);
                break;

            default:
                query = query.OrderBy(t => t.CreatedAt); // За замовчуванням
                break;
        }

        // Пагінація
        var tasks = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (tasks, totalCount);
    }
}