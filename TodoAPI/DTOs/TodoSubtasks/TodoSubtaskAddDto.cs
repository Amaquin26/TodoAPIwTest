using System.ComponentModel.DataAnnotations;

namespace TodoAPI.DTOs.TodoSubtasks;

public class TodoSubtaskAddDto
{
    [Required]
    public string Name { get; set; } = null!;

    [Required]
    public int TodoTaskId { get; set; }
}