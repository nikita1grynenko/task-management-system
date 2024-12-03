using Microsoft.AspNetCore.Mvc;
using TaskManagementSystem.Application.Contracts;
using TaskManagementSystem.Application.DTOs;

namespace TaskManagementSystem.Api.Controllers;

public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly ILogger<UserController> _logger;

    public UserController(IUserService userService, ILogger<UserController> logger)
    {
        _userService = userService;
        _logger = logger;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterUserDTO registerDto)
    {
        _logger.LogInformation("Received registration request for email: {Email}", registerDto.Email);

        try
        {
            await _userService.RegisterAsync(registerDto);
            return Ok("Registration successful");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while registering user with email: {Email}", registerDto.Email);
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginUserDTO loginDto)
    {
        _logger.LogInformation("Received login request for email: {Email}", loginDto.Email);

        try
        {
            var token = await _userService.AuthenticateAsync(loginDto);
            return Ok(new { Token = token });
        }
        catch (UnauthorizedAccessException ex)
        {
            _logger.LogWarning(ex, "Unauthorized login attempt for email: {Email}", loginDto.Email);
            return Unauthorized("Invalid credentials");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred during login for email: {Email}", loginDto.Email);
            return StatusCode(500, "Internal server error");
        }
    }
}