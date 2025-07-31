using TodoAPI.Entities;
using TodoAPI.Repositories.Base;

namespace TodoAPI.Repositories.TodoSubtasks;

public interface ITodoSubtaskRepository: IBaseRepository<TodoSubtask>
{
    Task<IEnumerable<TodoSubtask>> GetAllByTodoTaskId(int taskId);
}