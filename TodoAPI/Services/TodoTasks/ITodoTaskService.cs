using TodoAPI.DTOs.TodoTasks;

namespace TodoAPI.Services.TodoTasks;

public interface ITodoTaskService
{
    Task<IEnumerable<TodoTaskReadDto>> GetAllTodoTask();
    Task<TodoTaskReadDto?> GetTodoTaskById(int id);
    Task<int> AddTodoTask(TodoTaskAddDto todoTaskDto);
    Task UpdateTodoTask(TodoTaskUpdateDto todoTaskDto);
    Task DeleteTodoTask(int id);
}