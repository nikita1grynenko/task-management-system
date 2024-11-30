using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TaskManagementSystem.Domain.Enums;

namespace TaskManagementSystem.Domain.Entities;

public class Task : BaseEntity
{
    
    [Required]
    [MaxLength(100)]
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime DueDate { get; set; }
    
    [Required]
    public UserTaskStatus Status { get; set; }
    
    [Required]
    public UserTaskPriority Priority { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    
    [ForeignKey("User")]
    public Guid UserId { get; set; }
    public User? User { get; set; }
}