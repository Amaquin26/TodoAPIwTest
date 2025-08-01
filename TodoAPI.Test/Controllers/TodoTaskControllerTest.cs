using Microsoft.AspNetCore.Mvc;
using Moq;
using TodoAPI.Controllers;
using TodoAPI.DTOs.TodoTasks;
using TodoAPI.Services.TodoTasks;

namespace TodoAPI.Test.Controllers;

public class TodoTaskControllerTest
{
    private readonly Mock<ITodoTaskService> _mockTodoTaskService;
    private readonly TodoTaskController _todoTaskController;

    public TodoTaskControllerTest()
    {
        _mockTodoTaskService = new Mock<ITodoTaskService>();
        _todoTaskController = new TodoTaskController(_mockTodoTaskService.Object);
    }

    [Fact]
    public async Task GetAllTodoTask_ShouldReturnListOfTodoTasks()
    {
        // Arrange
        var todoTaskList = new List<TodoTaskReadDto>
        {
            new TodoTaskReadDto { Id = 1, Title = "Task 1", Description = "Task Description 1"},
            new TodoTaskReadDto { Id = 2, Title = "Task 2", Description = "Task Description 2" }
        };
        
        _mockTodoTaskService.Setup(r => r.GetAllTodoTask()).ReturnsAsync(todoTaskList);
        
        // Act
        var result =  await _todoTaskController.GetAllTodoTask();
        
        // Assert
        var objectResult = Assert.IsType<OkObjectResult>(result);
        var returnedTodoTasks = Assert.IsAssignableFrom<IEnumerable<TodoTaskReadDto>>(objectResult.Value);
        Assert.Equal(200, objectResult.StatusCode);
        Assert.Equal(todoTaskList, returnedTodoTasks);
    }
    
    [Fact]
    public async Task GetTodoTaskById_ShouldReturnCorrectTodoTask()
    {
        // Arrange
        var todoTaskId = 1;
        var todoTask = new TodoTaskReadDto { Id = todoTaskId, Title = "Sample Task", Description = "Sample Task Description" };
        
        _mockTodoTaskService.Setup(r => r.GetTodoTaskById(todoTaskId)).ReturnsAsync(todoTask);
        
        // Act
        var result =  await _todoTaskController.GetTodoTaskById(todoTaskId);
        
        // Assert
        var resultObject = Assert.IsType<OkObjectResult>(result);
        var returnedTodoTask = Assert.IsAssignableFrom<TodoTaskReadDto>(resultObject.Value);
        Assert.Equal(200, resultObject.StatusCode);
        Assert.Equal(todoTask, returnedTodoTask);
    }

    [Fact]
    public async Task AddTodoTask_ShouldReturnNewId()
    {
        // Arrange
        var todoTaskAddDto = new TodoTaskAddDto { Title = "New Todo Task", Description = "New Todo Task Description"};
        var expectedTodoTaskId = 101;

        _mockTodoTaskService.Setup(s => s.AddTodoTask(todoTaskAddDto)).ReturnsAsync(expectedTodoTaskId);

        // Act
        var result =  await _todoTaskController.AddTodoTask(todoTaskAddDto);

        // Assert
        var resultObject = Assert.IsType<CreatedAtRouteResult>(result);
        var returnedTodoTaskId = Assert.IsAssignableFrom<int>(resultObject.Value);
        Assert.Equal(201, resultObject.StatusCode);
        Assert.Equal(expectedTodoTaskId, returnedTodoTaskId);
    }
    
    [Fact]
    public async Task UpdateTodoTask_ShouldNotThrow()
    {
        // Arrange
        var todoTaskUpdateDto = new TodoTaskUpdateDto { Id = 101, Title = "New Todo Task", Description = "New Todo Task Description"};

        _mockTodoTaskService.Setup(s => s.UpdateTodoTask(todoTaskUpdateDto)).Returns(Task.CompletedTask);

        // Act
        var result =  await _todoTaskController.UpdateTodoTask(todoTaskUpdateDto);

        // Assert
        var resultObject = Assert.IsType<NoContentResult>(result);
        Assert.Equal(204, resultObject.StatusCode);
    }

    [Fact]
    public async Task DeleteTodoTask_ShouldNotThrow()
    {
        // Arrange
        var todoTaskId = 1;

        _mockTodoTaskService.Setup(s => s.DeleteTodoTask(todoTaskId)).Returns(Task.CompletedTask);

        // Act
        var result =  await _todoTaskController.DeleteTodoTask(todoTaskId);

        // Assert
        var resultObject = Assert.IsType<NoContentResult>(result);
        Assert.Equal(204, resultObject.StatusCode);
    }
}