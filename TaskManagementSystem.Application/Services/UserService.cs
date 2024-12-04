using Microsoft.Extensions.Logging;
using TaskManagementSystem.Application.Contracts;
using TaskManagementSystem.Application.DTOs;
using TaskManagementSystem.Domain.Entities;
using TaskManagementSystem.Infrastructure.Contracts;


namespace TaskManagementSystem.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtService _jwtService;
    private readonly IPasswordHasher _passwordHasher;
    private readonly ILogger<UserService> _logger;

    public UserService(IUserRepository userRepository, IJwtService jwtService, IPasswordHasher passwordHasher, ILogger<UserService> logger)
    {
        _userRepository = userRepository;
        _jwtService = jwtService;
        _passwordHasher = passwordHasher;
        _logger = logger;
    }

    public async Task RegisterAsync(RegisterDTO registerDto)
    {
        _logger.LogInformation("Attempting to register user with email: {Email}", registerDto.Email);

        var hashedPassword = _passwordHasher.GenerateHash(registerDto.Password);
        var user = new User
        {
            Username = registerDto.Username,
            Email = registerDto.Email,
            PasswordHash = hashedPassword
        };

        await _userRepository.AddAsync(user);
        _logger.LogInformation("User registered successfully: {Username}", registerDto.Username);
    }

    public async Task<string> AuthenticateAsync(LoginDTO loginDto)
    {
        _logger.LogInformation("Attempting login for email: {Email}", loginDto.Email);

        var user = await _userRepository.GetByEmailAsync(loginDto.Email);
        if (user == null || !_passwordHasher.VerifyHash(user.PasswordHash, loginDto.Password))
        {
            _logger.LogWarning("Invalid login attempt for email: {Email}", loginDto.Email);
            throw new UnauthorizedAccessException("Invalid credentials");
        }

        _logger.LogInformation("User logged in successfully: {Username}", user.Username);
        return _jwtService.GenerateToken(user);
    }
}
