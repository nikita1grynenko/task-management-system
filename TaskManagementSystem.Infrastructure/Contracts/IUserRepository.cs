﻿using TaskManagementSystem.Domain.Entities;

namespace TaskManagementSystem.Infrastructure.Contracts;

public interface IUserRepository
{
    Task<User> GetByIdAsync(Guid id);
    Task<IEnumerable<User>> GetAllAsync();
    Task AddAsync(User entity);
    Task UpdateAsync(User entity);
    Task DeleteAsync(Guid id);
}