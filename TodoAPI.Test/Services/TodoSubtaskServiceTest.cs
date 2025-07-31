using Moq;
using TodoAPI.DTOs.TodoSubtasks;
using TodoAPI.Services.TodoSubtasks;
using TodoAPI.Services.TodoTasks;

namespace TodoAPI.Test.Services;

public class TodoSubtaskServiceTest
{
    private readonly Mock<ITodoSubtaskService> _mockTodoSubtaskTaskService;
    
    public TodoSubtaskServiceTest()
    {
        _mockTodoSubtaskTaskService = new Mock<ITodoSubtaskService>();
    }

    [Fact]
    public async Task GetAllTodoSubtask_ShouldReturnListOfTodoSubtasks()
    {
        // Arrange
        var expectedTodoSubtasks = new List<TodoSubtaskReadDto>
        {
            new TodoSubtaskReadDto { Id = 1, TodoTaskId = 1, Name = "TodoSubtask 1" },
            new TodoSubtaskReadDto { Id = 2, TodoTaskId = 1, Name = "TodoSubtask 2" }
        };

        _mockTodoSubtaskTaskService.Setup(s => s.GetAllTodoSubtask()).ReturnsAsync(expectedTodoSubtasks);
        
        // Act
        var results = await _mockTodoSubtaskTaskService.Object.GetAllTodoSubtask();
        
        // Assert
        Assert.NotNull(results);
        Assert.Equal(2, results.Count());
    }

    [Fact]
    public async Task GetAllTodoSubtaskByTaskId_ShouldReturnListOfTodoSubtasks()
    {
        // Arrange
        int expectedTodoSubtaskId = 1;
        var expectedTodoSubtasks = new List<TodoSubtaskReadDto>
        {
            new TodoSubtaskReadDto { Id = 1, TodoTaskId = 1, Name = "TodoSubtask 1" },
            new TodoSubtaskReadDto { Id = 2, TodoTaskId = 1, Name = "TodoSubtask 2" }
        };

        _mockTodoSubtaskTaskService.Setup(s => s.GetAllTodoSubtaskByTaskId((expectedTodoSubtaskId)))
            .ReturnsAsync(expectedTodoSubtasks);
        
        // Act
        var result = await _mockTodoSubtaskTaskService.Object.GetAllTodoSubtaskByTaskId((expectedTodoSubtaskId));
        
        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
    }
}