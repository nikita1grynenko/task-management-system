using TaskManagementSystem.Application.Contracts;

namespace TaskManagementSystem.Application.Helpers;

public class PasswordHasher : IPasswordHasher
{
    public string GenerateHash(string password) =>
        BCrypt.Net.BCrypt.EnhancedHashPassword(password);
    
    public bool VerifyHash(string password, string hash) =>
        BCrypt.Net.BCrypt.EnhancedVerify(password, hash);
}