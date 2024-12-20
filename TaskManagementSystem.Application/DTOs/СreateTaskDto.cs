using TaskManagementSystem.Domain.Enums;

namespace TaskManagementSystem.Application.DTOs;

public class СreateTaskDto
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime DueDate { get; set; }
    public UserTaskStatus Status { get; set; } = UserTaskStatus.Pending;
    public UserTaskPriority Priority { get; set; } = UserTaskPriority.Medium;

}