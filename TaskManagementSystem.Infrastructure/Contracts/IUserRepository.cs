using TaskManagementSystem.Domain.Entities;

namespace TaskManagementSystem.Infrastructure.Contracts;

public interface IUserRepository : IGenericRepository<User>
{
    Task<User?> GetByEmailAsync(string email);
}