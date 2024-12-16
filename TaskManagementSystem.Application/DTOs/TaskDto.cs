using TaskManagementSystem.Domain.Enums;

namespace TaskManagementSystem.Application.DTOs;

public class TaskDto
{
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTime? DueDate { get; set; }
    public UserTaskStatus? Status { get; set; }
    public UserTaskPriority? Priority { get; set; }
}