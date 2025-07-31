using TodoAPI.DBContext;
using TodoAPI.Entities;
using Microsoft.EntityFrameworkCore;
using TodoAPI.Repositories.Base;

namespace TodoAPI.Repositories.TodoTasks;

public class TodoTaskRepository : BaseRepository<TodoTask>, ITodoTaskRepository
{

    public TodoTaskRepository(AppDbContext context) : base(context)
    {
    }

    public override async Task<IEnumerable<TodoTask>> GetAllAsync()
    {
        return await _context.TodoTasks
            .Where(t => !t.IsDeleted).ToListAsync();
    }

    public override async Task<TodoTask?> GetByIdAsync(int id)
    {
        return await _context.TodoTasks
            .FirstOrDefaultAsync(t => !t.IsDeleted && t.Id == id);
    }

    public async Task<TodoTask?> GetTodoTaskWithSubtasks(int taskId)
    {
        return await _context.TodoTasks
            .Include(t => t.TodoSubTasks)
            .FirstOrDefaultAsync(t => !t.IsDeleted);
    }

    public override void Remove(TodoTask task)
    {
        task.IsDeleted = true;
    }
}
