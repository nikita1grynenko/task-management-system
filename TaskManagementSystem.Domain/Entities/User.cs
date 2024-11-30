using System.ComponentModel.DataAnnotations;

namespace TaskManagementSystem.Domain.Entities;

public class User : BaseEntity
{
    
    [Required]
    [MaxLength(50)]
    public string Username { get; set; } = string.Empty;
    
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    
    public ICollection<Task> Tasks { get; set; } = new List<Task>();
}