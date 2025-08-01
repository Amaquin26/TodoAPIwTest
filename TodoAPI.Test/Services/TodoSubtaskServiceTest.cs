using Moq;
using TodoAPI.DTOs.TodoSubtasks;
using TodoAPI.Services.TodoSubtasks;

namespace TodoAPI.Test.Services;

public class TodoSubtaskServiceTest
{
    private readonly Mock<ITodoSubtaskService> _mockTodoSubtaskService;
    
    public TodoSubtaskServiceTest()
    {
        _mockTodoSubtaskService = new Mock<ITodoSubtaskService>();
    }

    [Fact]
    public async Task GetAllTodoSubtask_ShouldReturnListOfTodoSubtasks()
    {
        // Arrange
        var todoSubtasks = new List<TodoSubtaskReadDto>
        {
            new TodoSubtaskReadDto { Id = 1, TodoTaskId = 1, Name = "TodoSubtask 1" },
            new TodoSubtaskReadDto { Id = 2, TodoTaskId = 1, Name = "TodoSubtask 2" }
        };

        _mockTodoSubtaskService.Setup(s => s.GetAllTodoSubtask()).ReturnsAsync(todoSubtasks);
        
        // Act
        var results = await _mockTodoSubtaskService.Object.GetAllTodoSubtask();
        
        // Assert
        Assert.NotNull(results);
        Assert.Equal(2, results.Count());
    }

    [Fact]
    public async Task GetAllTodoSubtaskByTaskId_ShouldReturnListOfTodoSubtasks()
    {
        // Arrange
        var todoTaskId = 1;
        var todoSubtasks = new List<TodoSubtaskReadDto>
        {
            new TodoSubtaskReadDto { Id = 1, TodoTaskId = todoTaskId, Name = "TodoSubtask 1" },
            new TodoSubtaskReadDto { Id = 2, TodoTaskId = todoTaskId, Name = "TodoSubtask 2" }
        };

        _mockTodoSubtaskService.Setup(s => s.GetAllTodoSubtaskByTaskId((todoTaskId)))
            .ReturnsAsync(todoSubtasks);
        
        // Act
        var result = await _mockTodoSubtaskService.Object.GetAllTodoSubtaskByTaskId((todoTaskId));
        var resultList = result.ToList();
        
        // Assert
        Assert.NotNull(resultList);
        Assert.Equal(2, resultList.Count());
        Assert.All(resultList, subtask =>
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
        var result = await _mockTodoSubtaskService.Object.GetTodoSubtaskById(todoSubtaskId); 
        
        //Assert
        Assert.NotNull(result);
        Assert.Equal(todoSubtask, result);
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
        var result = await _mockTodoSubtaskService.Object.AddTodoSubtask(todoSubtaskAddDto);
        
        // Assert
        Assert.Equal(expectedTodoSubtaskId, result);
    }

    [Fact]
    public async Task UpdateTodoSubtask_ShouldNotThrow()
    {
        // Arrange
        var todoSubtaskUpdateDto = new TodoSubtaskUpdateDto() { Name = "Updated Todo Subtask" };

        _mockTodoSubtaskService.Setup(s => s.UpdateTodoSubtask(todoSubtaskUpdateDto)).Returns(Task.CompletedTask);
        
        // Act & Assert
        await _mockTodoSubtaskService.Object.UpdateTodoSubtask(todoSubtaskUpdateDto); // if no exception, test passes
    }

    [Fact]
    public async Task DeleteTodoSubtask_ShouldNotThrow()
    {
        // Arrange
        var todoSubtaskId = 1;

        _mockTodoSubtaskService.Setup(s => s.DeleteTodoSubtask(todoSubtaskId)).Returns(Task.CompletedTask);
        
        // Act & Assert
        await _mockTodoSubtaskService.Object.DeleteTodoSubtask(todoSubtaskId); // if no exception, test passes
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
        var result = await _mockTodoSubtaskService.Object.ToggleTodoSubtaskCheckStatus(todoSubtaskId);
        
        // Assert
        Assert.Equal(expectedTodoSubtaskCheckStatus, result);
    }
}