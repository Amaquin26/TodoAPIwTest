using Microsoft.EntityFrameworkCore;
using TodoAPI.DBContext;
using TodoAPI.Middlewares;
using TodoAPI.Repositories.TodoSubtasks;
using TodoAPI.Repositories.TodoTasks;
using TodoAPI.Services.TodoSubtasks;
using TodoAPI.Services.TodoTasks;
using TodoAPI.UnitOfWork;

namespace TodoAPI;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        // Services
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();
        builder.Services.AddScoped<ITodoTaskRepository, TodoTaskRepository>();
        builder.Services.AddScoped<ITodoSubtaskRepository, TodoSubtaskRepository>();
        builder.Services.AddScoped<ITodoTaskService, TodoTaskService>();
        builder.Services.AddScoped<ITodoSubtaskService, TodoSubtaskService>();
        
        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));
        
        var app = builder.Build();

        // Middlewares
        app.UseMiddleware<ExceptionHandlingMiddleware>();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}