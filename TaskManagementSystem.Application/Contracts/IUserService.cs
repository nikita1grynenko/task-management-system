using TaskManagementSystem.Application.DTOs;

namespace TaskManagementSystem.Application.Contracts;

public interface IUserService
{
    Task RegisterAsync(RegisterUserDTO registerDto);
    Task<string> AuthenticateAsync(LoginUserDTO loginDto);
}