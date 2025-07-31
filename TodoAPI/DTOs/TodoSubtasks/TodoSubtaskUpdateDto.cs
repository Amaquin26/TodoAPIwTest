using System.ComponentModel.DataAnnotations;

namespace TodoAPI.DTOs.TodoSubtasks;

public class TodoSubtaskUpdateDto
{
    [Required]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = null!;
}