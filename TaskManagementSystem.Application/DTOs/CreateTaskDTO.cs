namespace TaskManagementSystem.Application.DTOs;

public class CreateTaskDTO
{
    public string Title { get; set; }
    public string? Description { get; set; }
    public DateTime? DueDate { get; set; }
    public string Status { get; set; }
    public string Priority { get; set; }
}