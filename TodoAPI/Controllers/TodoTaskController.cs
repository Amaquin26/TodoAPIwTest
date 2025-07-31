using Microsoft.AspNetCore.Mvc;
using TodoAPI.DTOs.TodoTasks;
using TodoAPI.Services.TodoTasks;

namespace TodoAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TodoTaskController: ControllerBase
{
    private readonly ITodoTaskService _todoTaskService;

    public TodoTaskController(ITodoTaskService todoTaskService)
    {
        _todoTaskService = todoTaskService;
    }
    
    [HttpGet]
    public async Task<ActionResult> GetAll()
    {
        var todoTasks = await _todoTaskService.GetAllTodoTask();
        return Ok(todoTasks);
    }
    
    [HttpGet("{id:int}", Name = "GetTodoTaskById")]
    public async Task<ActionResult> GetTodoTaskById(int id)
    {
        var todoTasks = await _todoTaskService.GetTodoTaskById(id);
        return Ok(todoTasks);
    }
    
    [HttpPost]
    public async Task<IActionResult> AddTodoTask([FromBody] TodoTaskAddDto todoTaskDto)
    {
        var newTodoTaskId = await _todoTaskService.AddTodoTask(todoTaskDto);
        return CreatedAtRoute("GetTodoTaskById",new {id = newTodoTaskId},newTodoTaskId);
    }
    
    [HttpPut]
    public async Task<IActionResult> UpdateTodoTask([FromBody] TodoTaskUpdateDto todoTaskDto)
    {
        await _todoTaskService.UpdateTodoTask(todoTaskDto);
        return NoContent();
    }
    
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteTodoTask(int id)
    {
        await _todoTaskService.DeleteTodoTask(id);
        return NoContent();
    }
}