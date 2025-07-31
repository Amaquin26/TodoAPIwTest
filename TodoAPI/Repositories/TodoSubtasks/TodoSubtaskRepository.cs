using TodoAPI.Entities;
using TodoAPI.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using TodoAPI.DBContext;

namespace TodoAPI.Repositories.TodoSubtasks;

public class TodoSubtaskRepository: BaseRepository<TodoSubtask>, ITodoSubtaskRepository
{
    public TodoSubtaskRepository(AppDbContext context) : base(context)
    {
    }
    
    public override async Task<IEnumerable<TodoSubtask>> GetAllAsync()
    {
        return await _context.TodoSubtasks
            .Where(t => !t.IsDeleted).ToListAsync();
    }
    
    public override async Task<TodoSubtask?> GetByIdAsync(int id)
    {
        return await _context.TodoSubtasks
            .FirstOrDefaultAsync(t => !t.IsDeleted && t.Id == id);
    }

    public async Task<IEnumerable<TodoSubtask>> GetAllByTodoTaskId(int taskId)
    {
        return await _context.TodoSubtasks
            .Where(t => !t.IsDeleted && t.TodoTaskId == taskId).ToListAsync();
    }
    
    public override void Remove(TodoSubtask task)
    {
        task.IsDeleted = true;
    }
}