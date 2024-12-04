using Microsoft.EntityFrameworkCore;
using TaskManagementSystem.Domain.Entities;
using TaskManagementSystem.Infrastructure.Context;
using TaskManagementSystem.Infrastructure.Contracts;

namespace TaskManagementSystem.Infrastructure.Repository;

public class UserTaskRepository : IUserTaskRepository
{
    private readonly AppDbContext _context;

    public UserTaskRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<UserTask>> GetTasksAsync(Guid userId, string status = null, DateTime? dueDate = null, string priority = null)
    {
        var query = _context.Tasks.AsQueryable();

        if (!string.IsNullOrEmpty(status))
            query = query.Where(t => t.Status.ToString() == status);
        
        if (dueDate.HasValue)
            query = query.Where(t => t.DueDate == dueDate.Value);
        
        if (!string.IsNullOrEmpty(priority))
            query = query.Where(t => t.Priority.ToString() == priority);
        
        return await query.ToListAsync();
    }

    public async Task<UserTask?> GetTaskByIdAsync(Guid id, Guid userId)
    {
        return await _context.Tasks.FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task CreateTaskAsync(UserTask task)
    {
        await _context.Tasks.AddAsync(task);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateTaskAsync(UserTask task)
    {
        _context.Tasks.Update(task);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteTaskAsync(Guid id, Guid userId)
    {
        var task = await GetTaskByIdAsync(id, userId);
        if (task != null)
        {
            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
        }
    }
}