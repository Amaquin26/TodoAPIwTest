using System.ComponentModel.DataAnnotations;

namespace TodoAPI.DTOs.TodoTasks;

public class TodoTaskAddDto
{
    [Required]
    public string Title { get; set; } = null!;

    public string? Description { get; set; }
}