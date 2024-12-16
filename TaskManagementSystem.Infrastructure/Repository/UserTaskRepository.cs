using Microsoft.EntityFrameworkCore;
using TaskManagementSystem.Domain.Entities;
using TaskManagementSystem.Domain.Enums;
using TaskManagementSystem.Infrastructure.Context;
using TaskManagementSystem.Infrastructure.Contracts;

namespace TaskManagementSystem.Infrastructure.Repository;

public class UserTaskRepository : IUserTaskRepository
{
    private readonly AppDbContext _context;

    public async Task<UserTask?> GetByIdAsync(Guid taskId)
    {
        return await _context.Tasks.FindAsync(taskId);
    }

    public IQueryable<UserTask> QueryTasks(Guid userId)
    {
        return _context.Tasks.Where(t => t.UserId == userId);
    }

    public async Task AddAsync(UserTask task)
    {
        await _context.Tasks.AddAsync(task);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(UserTask task)
    {
        _context.Tasks.Update(task);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(UserTask task)
    {
        _context.Tasks.Remove(task);
        await _context.SaveChangesAsync();
    }

    public async Task<int> CountAsync(IQueryable<UserTask> query)
    {
        return await query.CountAsync();
    }
}