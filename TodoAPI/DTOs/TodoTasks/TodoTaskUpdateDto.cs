using System.ComponentModel.DataAnnotations;

namespace TodoAPI.DTOs.TodoTasks;

public class TodoTaskUpdateDto
{
    [Required]
    public int Id { get; set; }

    [Required]
    public string Title { get; set; } = null!;

    public string? Description { get; set; }
}