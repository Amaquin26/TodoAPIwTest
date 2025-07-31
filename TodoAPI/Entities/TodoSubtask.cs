using System.ComponentModel.DataAnnotations;

namespace TodoAPI.Entities;

public class TodoSubtask
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(255)]
    public string Name { get; set; } = null!;
    
    [Required]
    public bool IsDeleted { get; set; } = false;
    
    [Required]
    public bool IsChecked { get; set; } = false;
    
    public int TodoTaskId { get; set; }
    
    // Navigation Property
    public TodoTask? TodoTask { get; set; }
}