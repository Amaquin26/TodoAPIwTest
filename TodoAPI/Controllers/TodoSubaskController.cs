using Microsoft.AspNetCore.Mvc;
using TodoAPI.DTOs.TodoSubtasks;
using TodoAPI.DTOs.TodoTasks;
using TodoAPI.Repositories.TodoTasks;
using TodoAPI.Services.TodoSubtasks;
using TodoAPI.Services.TodoTasks;

namespace TodoAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TodoSubaskController: ControllerBase
{
    private readonly ITodoSubtaskService _todoSubtaskService;
    
    public TodoSubaskController(ITodoSubtaskService todoSubtaskService)
    {
        _todoSubtaskService = todoSubtaskService;
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TodoSubtaskReadDto>>> GetAllTodoSubtask()
    {
        var todoSubtasks = await _todoSubtaskService.GetAllTodoSubtask();
        return Ok(todoSubtasks);
    }
    
    [HttpGet("task/{todoTaskId:int}")]
    public async Task<ActionResult<IEnumerable<TodoSubtaskReadDto>>> GetAllTodoSubtask(int todoTaskId)
    {
        var todoSubtasks = await _todoSubtaskService.GetAllTodoSubtaskByTaskId(todoTaskId);
        return Ok(todoSubtasks);
    }
    
    [HttpGet("{id:int}", Name = "GetTodoSubtaskById")]
    public async Task<ActionResult<TodoSubtaskReadDto>> GetTodoSubtaskById(int id)
    {
        var todoSubtask = await _todoSubtaskService.GetTodoSubtaskById(id);
        return Ok(todoSubtask);
    }
    
    [HttpPost]
    public async Task<ActionResult<int>> AddTodoSubtask([FromBody] TodoSubtaskAddDto todoSubtaskDto)
    {
        var newTodoSubtaskId = await _todoSubtaskService.AddTodoSubtask(todoSubtaskDto);
        return CreatedAtRoute("GetTodoSubtaskById",new {id = newTodoSubtaskId},newTodoSubtaskId);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateTodoSubtask([FromBody] TodoSubtaskUpdateDto todoSubtaskDto)
    {
        await _todoSubtaskService.UpdateTodoSubtask(todoSubtaskDto);
        return NoContent();
    }
    
    [HttpPatch("{id:int}")]
    public async Task<IActionResult> ToggleTodoSubtaskCheckStatus(int id)
    {
        var newTodoSubtaskCheckStatus = await _todoSubtaskService.ToggleTodoSubtaskCheckStatus(id);
        return Ok(newTodoSubtaskCheckStatus);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteTodoSubtask(int id)
    {
        await _todoSubtaskService.DeleteTodoSubtask(id);
        return NoContent();
    }
}