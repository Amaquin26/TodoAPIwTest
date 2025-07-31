using TodoAPI.DTOs.TodoSubtasks;

namespace TodoAPI.Services.TodoSubtasks;

public interface ITodoSubtaskService
{
    Task<IEnumerable<TodoSubtaskReadDto>> GetAllTodoSubtask();
    Task<IEnumerable<TodoSubtaskReadDto>> GetAllTodoSubtaskByTaskId(int todoTaskId);
    Task<TodoSubtaskReadDto?> GetTodoSubtaskById(int id);
    Task<int> AddTodoSubtask(TodoSubtaskAddDto todoSubtaskDto);
    Task UpdateTodoSubtask(TodoSubtaskUpdateDto todoSubtaskDto);
    Task DeleteTodoSubtask(int id);
    Task<bool> ToggleTodoSubtaskCheckStatus(int id);
}