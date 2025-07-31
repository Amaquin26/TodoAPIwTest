using System.ComponentModel.DataAnnotations;

namespace TodoAPI.Entities;

public class TodoTask
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    [StringLength(100)]
    public string Title { get; set; } = null!;

    [StringLength(255)]
    public string? Description { get; set; }
    
    [Required]
    public bool IsDeleted { get; set; } = false;
    
    //Navigation Property
    public ICollection<TodoSubtask> TodoSubTasks { get; set; } = new List<TodoSubtask>();
}