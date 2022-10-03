
namespace TaskTracker.Model.DTO;

public class TaskDTO
{
    public int Id { get; set; }
    public string Name { get; set; }

    public string? Description { get; set; }

    public string Status { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? DueDate { get; set; }

    public int ProjectId { get; set; }

    public int? FatherTaskId { get; set; }

}

