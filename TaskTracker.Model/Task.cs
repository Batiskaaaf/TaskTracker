using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskTracker.Model;
public class Task
{
    [Key]
    public int Id { get; set; }

    [Required]
    public required string Name { get; set; }

    public string? Description { get; set; }

    [Required]
    public required string Status { get; set; }

    [DataType(DataType.DateTime)]
    public DateTime CreatedDate { get; set; } = DateTime.Now;


    [DataType(DataType.DateTime)]
    public DateTime? StartDate { get; set; }


    [DataType(DataType.DateTime)]
    public DateTime? DueDate { get; set; }


    public int ProjectId { get; set; }

    [ForeignKey("ProjectId")]
    public required Project Project { get; set; }

}
