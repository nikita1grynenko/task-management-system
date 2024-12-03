using TaskManagementSystem.Api.Contracts;
using TaskManagementSystem.Domain.Entities;

namespace TaskManagementSystem.Infrastructure.Repository;

public class UserTaskRepository : IGenericRepository<UserTask>
{
    private IGenericRepository<UserTask> _genericRepositoryImplementation;
    public Task<UserTask> GetByIdAsync(Guid id)
    {
        return _genericRepositoryImplementation.GetByIdAsync(id);
    }

    public Task<IEnumerable<UserTask>> GetAllAsync()
    {
        return _genericRepositoryImplementation.GetAllAsync();
    }

    public Task AddAsync(UserTask entity)
    {
        return _genericRepositoryImplementation.AddAsync(entity);
    }

    public Task UpdateAsync(UserTask entity)
    {
        return _genericRepositoryImplementation.UpdateAsync(entity);
    }

    public Task DeleteAsync(Guid id)
    {
        return _genericRepositoryImplementation.DeleteAsync(id);
    }
}