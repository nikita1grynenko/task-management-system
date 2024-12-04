﻿namespace TaskManagementSystem.Infrastructure.Contracts;

public interface IGenericRepository<T> 
{
    Task<T> GetByIdAsync(Guid id);
    Task<IEnumerable<T>> GetAllAsync();
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(Guid id);

}