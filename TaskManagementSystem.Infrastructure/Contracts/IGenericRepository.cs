using TaskManagementSystem.Domain.Entities;

namespace TaskManagementSystem.Infrastructure.Contracts;

public interface IGenericRepository<T> where T : BaseEntity
{
    public void Add(T entity);
    public void Delete(T entity);
    public void Update(T entity);
    public List<T> GetAll();
    public T GetById(int id);
}