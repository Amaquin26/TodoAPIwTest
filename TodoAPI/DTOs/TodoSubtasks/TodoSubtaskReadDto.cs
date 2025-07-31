using TodoAPI.Entities;

namespace TodoAPI.DTOs.TodoSubtasks;

public class TodoSubtaskReadDto
{
    public int Id { get; set; }

    public int TodoTaskId { get; set; }

    public string Name { get; set; } = null!;

    public bool IsChecked { get; set; }

    public static TodoSubtaskReadDto MapTodoSubtaskToTodoSubtaskReadDto(TodoSubtask todoSubtask)
    {
        return new TodoSubtaskReadDto
        {
            Id = todoSubtask.Id,
            TodoTaskId = todoSubtask.TodoTaskId,
            Name = todoSubtask.Name,
            IsChecked = todoSubtask.IsChecked
        };
    }
}