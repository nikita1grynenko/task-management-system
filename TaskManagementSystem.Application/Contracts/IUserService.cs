using TaskManagementSystem.Application.DTOs;

namespace TaskManagementSystem.Application.Contracts;

public interface IUserService
{
    Task RegisterAsync(RegisterDTO registerDto);
    Task<string> AuthenticateAsync(LoginDTO loginDto);
}