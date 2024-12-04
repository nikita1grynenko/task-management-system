using Microsoft.EntityFrameworkCore;
using TaskManagementSystem.Domain.Entities;
using TaskManagementSystem.Infrastructure.Context;
using TaskManagementSystem.Infrastructure.Contracts;

namespace TaskManagementSystem.Infrastructure.Repository;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        if (string.IsNullOrEmpty(email))
        {
            throw new ArgumentException("Email cannot be null or empty.", nameof(email));
        }

        return await _context.Set<User>()
            .FirstOrDefaultAsync(u => u.Email == email);
    }
}