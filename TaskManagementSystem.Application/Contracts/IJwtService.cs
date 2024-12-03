using TaskManagementSystem.Domain.Entities;

namespace TaskManagementSystem.Application.Contracts;

public interface IJwtService
{
    string GenerateToken(User user); // Генерація JWT токена для користувача
}