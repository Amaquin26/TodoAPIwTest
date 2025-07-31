using TodoAPI.Entities;

namespace TodoAPI.DTOs.TodoTasks;

public class TodoTaskReadDto
{
    public int Id { get; set; }
    
    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public static TodoTaskReadDto MapTodoTaskToTodoTaskReadDto(TodoTask task)
    {
        return new TodoTaskReadDto {
            Id = task.Id,
            Title = task.Title,
            Description = task.Description,
        };
    }
}