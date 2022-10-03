
namespace TaskTracker.Model.DTO;

public class ProjectDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public string Status { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? DueDate { get; set; }     

}

