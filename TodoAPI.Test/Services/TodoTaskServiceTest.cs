using TodoAPI.Services.TodoTasks;
using TodoAPI.DTOs.TodoTasks;
using Moq;

namespace TodoAPI.Test.Services;

public class TodoTaskServiceTest
{
    private readonly Mock<ITodoTaskService> _mockTodoTaskService;

    public TodoTaskServiceTest()
    {
        _mockTodoTaskService = new Mock<ITodoTaskService>();
    }

    [Fact]
    public async Task GetAllTodoTask_ShouldReturnListOfTodoTasks()
    {
        // Arrange
        var todoTaskList = new List<TodoTaskReadDto>
        {
            new TodoTaskReadDto { Id = 1, Title = "Task 1", Description = "Task 1 Description"},
            new TodoTaskReadDto { Id = 2, Title = "Task 2", Description = "Task 2 Description" }
        };

        _mockTodoTaskService.Setup(s => s.GetAllTodoTask()).ReturnsAsync(todoTaskList);

        // Act
        var result = await _mockTodoTaskService.Object.GetAllTodoTask();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
    }

    [Fact]
    public async Task GetTodoTaskById_ShouldReturnCorrectTodoTask()
    {
        // Arrange
        var todoTaskId = 1;
        var todoTask = new TodoTaskReadDto { Id = todoTaskId, Title = "Sample Task", Description = " Sample Task Description" };

        _mockTodoTaskService.Setup(s => s.GetTodoTaskById(todoTaskId)).ReturnsAsync(todoTask);

        // Act
        var result = await _mockTodoTaskService.Object.GetTodoTaskById(todoTaskId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(todoTaskId, result?.Id);
    }

    [Fact]
    public async Task AddTodoTask_ShouldReturnNewId()
    {
        // Arrange
        var todoTaskAddDto = new TodoTaskAddDto { Title = "New Todo Task" };
        var expectedTodoTaskId = 101;

        _mockTodoTaskService.Setup(s => s.AddTodoTask(todoTaskAddDto)).ReturnsAsync(expectedTodoTaskId);

        // Act
        var result = await _mockTodoTaskService.Object.AddTodoTask(todoTaskAddDto);

        // Assert
        Assert.Equal(expectedTodoTaskId, result);
    }

    [Fact]
    public async Task UpdateTodoTask_ShouldNotThrow()
    {
        // Arrange
        var todoTaskUpdateDto = new TodoTaskUpdateDto { Id = 1, Title = "Updated Task" };

        _mockTodoTaskService.Setup(s => s.UpdateTodoTask(todoTaskUpdateDto)).Returns(Task.CompletedTask);

        // Act & Assert
        await _mockTodoTaskService.Object.UpdateTodoTask(todoTaskUpdateDto); // if no exception, test passes
    }

    [Fact]
    public async Task DeleteTodoTask_ShouldNotThrow()
    {
        // Arrange
        var todoTaskId = 1;

        _mockTodoTaskService.Setup(s => s.DeleteTodoTask(todoTaskId)).Returns(Task.CompletedTask);

        // Act & Assert
        await _mockTodoTaskService.Object.DeleteTodoTask(todoTaskId); // if no exception, test passes
    }
}