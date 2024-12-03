using TaskManagementSystem.Api.Contracts;
using TaskManagementSystem.Domain.Entities;
using TaskManagementSystem.Infrastructure.Contracts;

namespace TaskManagementSystem.Infrastructure.Repository;

public class UserRepository : IUserRepository
{
    private IGenericRepository<User> _genericRepositoryImplementation;
    public Task<User> GetByIdAsync(Guid id)
    {
        return _genericRepositoryImplementation.GetByIdAsync(id);
    }

    public Task<IEnumerable<User>> GetAllAsync()
    {
        return _genericRepositoryImplementation.GetAllAsync();
    }

    public Task AddAsync(User entity)
    {
        return _genericRepositoryImplementation.AddAsync(entity);
    }

    public Task UpdateAsync(User entity)
    {
        return _genericRepositoryImplementation.UpdateAsync(entity);
    }

    public Task DeleteAsync(Guid id)
    {
        return _genericRepositoryImplementation.DeleteAsync(id);
    }
}