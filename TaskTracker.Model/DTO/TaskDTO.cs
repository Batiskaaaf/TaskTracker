
namespace TaskTracker.Model.DTO;

public class TaskDTO
{
    public int Id { get; set; }
    public required string Name { get; set; }

    public string? Description { get; set; }

    public required string Status { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? DueDate { get; set; }

    public int ProjectId { get; set; }
}

