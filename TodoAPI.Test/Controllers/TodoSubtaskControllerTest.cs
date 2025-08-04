using Microsoft.AspNetCore.Mvc;
using Moq;
using TodoAPI.Controllers;
using TodoAPI.DTOs.TodoSubtasks;
using TodoAPI.Entities;
using TodoAPI.Services.TodoSubtasks;
using TodoAPI.Infrastructure.Helpers;

namespace TodoAPI.Test.Controllers;

public class TodoSubtaskControllerTest
{
    private readonly Mock<ITodoSubtaskService> _mockTodoSubtaskService;
    private readonly TodoSubtaskController _todoSubtaskController;

    public TodoSubtaskControllerTest()
    {
        _mockTodoSubtaskService = new Mock<ITodoSubtaskService>();
        _todoSubtaskController = new TodoSubtaskController(_mockTodoSubtaskService.Object);
    }

    [Fact]
    public async Task GetAllTodoSubtask_ShouldReturnListOfTodoSubtask()
    {
        // Arrange
        var todoSubtaskList = new List<TodoSubtaskReadDto>()
        {
            new TodoSubtaskReadDto { Id = 1, TodoTaskId = 1, Name = "Todo Subtask 1", IsChecked = true },
            new TodoSubtaskReadDto { Id = 2, TodoTaskId = 1, Name = "Todo Subtask 2", IsChecked = false }
        };
        
        _mockTodoSubtaskService.Setup(s => s.GetAllTodoSubtask()).ReturnsAsync(todoSubtaskList);
        
        // Act
        var result = await _todoSubtaskController.GetAllTodoSubtask();
        
        // Assert
        var resultObject = Assert.IsType<OkObjectResult>(result);
        var returnedTodoSubtasks = Assert.IsAssignableFrom<IEnumerable<TodoSubtaskReadDto>>(resultObject.Value);
        Assert.Equal(200, resultObject.StatusCode);
        Assert.Equal(todoSubtaskList, returnedTodoSubtasks);
    }
    
    [Fact]
    public async Task GetAllTodoSubtaskByTaskId_ShouldReturnListOfTodoSubtask()
    {
        // Arrange
        var todoTaskId = 1;
        var todoSubtaskList = new List<TodoSubtaskReadDto>()
        {
            new TodoSubtaskReadDto { Id = 1, TodoTaskId = todoTaskId, Name = "Todo Subtask 1", IsChecked = true },
            new TodoSubtaskReadDto { Id = 2, TodoTaskId = todoTaskId, Name = "Todo Subtask 2", IsChecked = false }
        };
        
        _mockTodoSubtaskService.Setup(s => s.GetAllTodoSubtaskByTaskId(todoTaskId)).ReturnsAsync(todoSubtaskList);
        
        // Act
        var result = await _todoSubtaskController.GetAllTodoSubtaskByTaskId(todoTaskId);
        
        // Assert
        var resultObject = Assert.IsType<OkObjectResult>(result);
        var returnedTodoSubtasks = Assert.IsAssignableFrom<IEnumerable<TodoSubtaskReadDto>>(resultObject.Value);
        Assert.Equal(200, resultObject.StatusCode);
        
        var returnedTodoSubtaskList = returnedTodoSubtasks.ToList();
        
        Assert.Equal(todoSubtaskList, returnedTodoSubtaskList);
        Assert.All(returnedTodoSubtaskList, subtask =>
        {
            Assert.Equal(todoTaskId, subtask.TodoTaskId);
        });
    }

    [Fact]
    public async Task GetTodoSubtaskById_ShouldReturnCorrectTodoSubtask()
    {
        // Arrange
        var todoSubtaskId = 1;
        var todoSubtask = new TodoSubtaskReadDto { Id = todoSubtaskId, TodoTaskId = 1, Name = "TodoSubtask 1" };
        
        _mockTodoSubtaskService.Setup(s => s.GetTodoSubtaskById(todoSubtaskId)).ReturnsAsync(todoSubtask);

        // Act
        var result = await  _todoSubtaskController.GetTodoSubtaskById(todoSubtaskId);
        
        // Assert
        var resultObject = Assert.IsType<OkObjectResult>(result);
        var returnedTodoTask = Assert.IsAssignableFrom<TodoSubtaskReadDto>(resultObject.Value);
        Assert.NotNull(returnedTodoTask);
        Assert.Equal(200, resultObject.StatusCode);
        Assert.Equal(todoSubtask, returnedTodoTask);
    }
    
    [Fact]
    public async Task GetTodoSubtaskById_ShouldThrowKeyNotFoundException_WhenNotFound()
    {
        // Arrange
        var todoSubtaskId = 101;

        _mockTodoSubtaskService
            .Setup(s => s.GetTodoSubtaskById(todoSubtaskId))
            .ThrowsAsync(ExceptionHelper.NotFound<TodoSubtask>(todoSubtaskId));
        
        // Act & Assert
        var exception = await Assert.ThrowsAsync<KeyNotFoundException>(() => _todoSubtaskController.GetTodoSubtaskById(todoSubtaskId));
        Assert.Equal(ExceptionHelper.NotFound<TodoSubtask>(todoSubtaskId).Message, exception.Message);
    }
    
    [Fact]
    public async Task AddTodoSubtask_ShouldReturnNewId()
    {
        //Arrange
        var todoSubtaskAddDto = new TodoSubtaskAddDto()
        {
            Name = "New Todo Subtask",
            TodoTaskId = 1
        };
        var expectedTodoSubtaskId = 101;
        
        _mockTodoSubtaskService.Setup(s => s.AddTodoSubtask(todoSubtaskAddDto)).ReturnsAsync(expectedTodoSubtaskId);
        
        // Act
        var result = await  _todoSubtaskController.AddTodoSubtask(todoSubtaskAddDto);
        
        // Assert
        var resultObject = Assert.IsType<CreatedAtRouteResult>(result);
        var returnedTodoSubtaskId = Assert.IsAssignableFrom<int>(resultObject.Value);
        Assert.Equal(201, resultObject.StatusCode);
        Assert.Equal(expectedTodoSubtaskId, returnedTodoSubtaskId);
    }
    
    [Fact]
    public async Task UpdateTodoSubtask_ShouldNotThrow()
    {
        //Arrange
        var todoSubtaskUpdateDto = new TodoSubtaskUpdateDto() { Id = 1, Name = "Updated Todo Subtask" };
        
        _mockTodoSubtaskService.Setup(s => s.UpdateTodoSubtask(todoSubtaskUpdateDto)).Returns(Task.CompletedTask);
        
        // Act
        var result = await  _todoSubtaskController.UpdateTodoSubtask(todoSubtaskUpdateDto);
        
        // Assert
        var resultObject = Assert.IsType<NoContentResult>(result);
        Assert.Equal(204, resultObject.StatusCode);
    }
    
    [Fact]
    public async Task UpdateTodoSubtask_ShouldThrowKeyNotFoundException_WhenNotFound()
    {
        //Arrange
        var todoSubtaskUpdateDto = new TodoSubtaskUpdateDto() { Id = 1, Name = "Updated Todo Subtask" };

        _mockTodoSubtaskService
            .Setup(s => s.UpdateTodoSubtask(todoSubtaskUpdateDto))
            .ThrowsAsync(ExceptionHelper.NotFound<TodoSubtask>(todoSubtaskUpdateDto.Id));
        
        // Act & Assert
        var exception = await Assert.ThrowsAsync<KeyNotFoundException>(() => _todoSubtaskController.UpdateTodoSubtask(todoSubtaskUpdateDto));
        Assert.Equal(ExceptionHelper.NotFound<TodoSubtask>(todoSubtaskUpdateDto.Id).Message, exception.Message);
    }
    
    [Fact]
    public async Task DeleteTodoSubtask_ShouldNotThrow()
    {
        // Arrange
        var todoSubtaskId = 1;

        _mockTodoSubtaskService.Setup(s => s.DeleteTodoSubtask(todoSubtaskId)).Returns(Task.CompletedTask);

        // Act
        var result =  await _todoSubtaskController.DeleteTodoSubtask(todoSubtaskId);

        // Assert
        var okResult = Assert.IsType<NoContentResult>(result);
        Assert.Equal(204, okResult.StatusCode);
    }
    
    [Fact]
    public async Task DeleteTodoSubtask_ShouldThrowKeyNotFoundException_WhenNotFound()
    {
        // Arrange
        var todoSubtaskId = 1;

        _mockTodoSubtaskService
            .Setup(s => s.DeleteTodoSubtask(todoSubtaskId))
            .ThrowsAsync(ExceptionHelper.NotFound<TodoSubtask>(todoSubtaskId));
        
        // Act & Assert
        var exception = await Assert.ThrowsAsync<KeyNotFoundException>(() => _todoSubtaskController.DeleteTodoSubtask(todoSubtaskId));
        Assert.Equal(ExceptionHelper.NotFound<TodoSubtask>(todoSubtaskId).Message, exception.Message);
    }
    
    [Fact]
    public async Task ToggleTodoSubtaskCheckStatus_ShouldReturnNewCheckStatus()
    {
        // Arrange
        var todoSubtaskId = 1;
        var expectedTodoSubtaskCheckStatus = true;
        
        _mockTodoSubtaskService.Setup(s => s.ToggleTodoSubtaskCheckStatus(todoSubtaskId))
            .ReturnsAsync(expectedTodoSubtaskCheckStatus);
        
        // Act
        var result = await _todoSubtaskController.ToggleTodoSubtaskCheckStatus(todoSubtaskId);
        
        // Assert
        var resultObject = Assert.IsType<OkObjectResult>(result);
        var returnedNewTodoSubtaskCheckStatus = Assert.IsAssignableFrom<bool>(resultObject.Value);
        Assert.Equal(200, resultObject.StatusCode);
        Assert.Equal(expectedTodoSubtaskCheckStatus, returnedNewTodoSubtaskCheckStatus);
    }
    
    [Fact]
    public async Task ToggleTodoSubtaskCheckStatus_ShouldThrowKeyNotFoundException_WhenNotFound()
    {
        // Arrange
        var todoSubtaskId = 1;

        _mockTodoSubtaskService
            .Setup(s => s.ToggleTodoSubtaskCheckStatus(todoSubtaskId))
            .ThrowsAsync(ExceptionHelper.NotFound<TodoSubtask>(todoSubtaskId));
        
        // Act & Assert
        var exception = await Assert.ThrowsAsync<KeyNotFoundException>(() => _todoSubtaskController.ToggleTodoSubtaskCheckStatus(todoSubtaskId));
        Assert.Equal(ExceptionHelper.NotFound<TodoSubtask>(todoSubtaskId).Message, exception.Message);
    }
}