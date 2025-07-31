using TodoAPI.Entities;
using TodoAPI.Repositories.Base;

namespace TodoAPI.Repositories.TodoTasks;

public interface ITodoTaskRepository: IBaseRepository<TodoTask>
{
    Task<TodoTask?> GetTodoTaskWithSubtasks(int taskId);
}