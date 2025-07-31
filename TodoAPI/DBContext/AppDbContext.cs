using Microsoft.EntityFrameworkCore;
using TodoAPI.Entities;

namespace TodoAPI.DBContext;

public class AppDbContext: DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
    {
        
    }
    
    public DbSet<TodoTask> TodoTasks { get; set; }

    public DbSet<TodoSubtask> TodoSubtasks { get; set; }
}