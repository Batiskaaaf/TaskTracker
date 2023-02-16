using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskTracker.Model;
public class Project
{
    [Key]
    public int Id { get; set; }


    [Required]
    public string Name { get; set; }


    public string? Description { get; set; }


    [Required]
    public string Status { get; set; }


    [DataType(DataType.Date)]
    public DateTime CreatedDate { get; set; } = DateTime.Now;



    [DataType(DataType.Date)]
    public DateTime? StartDate { get; set; }



    [DataType(DataType.Date)]
    public DateTime? DueDate { get; set; }


    public ICollection<Task> Tasks { get; set; }   
}
