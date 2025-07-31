using TodoAPI.Services.TodoTasks;
using TodoAPI.DTOs.TodoTasks;
using Moq;
using Xunit;

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
        var expectedTasks = new List<TodoTaskReadDto>
        {
            new TodoTaskReadDto { Id = 1, Title = "Task 1", Description = "Task 1 Description"},
            new TodoTaskReadDto { Id = 2, Title = "Task 2", Description = "Task 2 Description" }
        };

        _mockTodoTaskService.Setup(s => s.GetAllTodoTask()).ReturnsAsync(expectedTasks);

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
        var taskId = 1;
        var expectedTask = new TodoTaskReadDto { Id = taskId, Title = "Sample Task", Description = " Sample Task Description" };

        _mockTodoTaskService.Setup(s => s.GetTodoTaskById(taskId)).ReturnsAsync(expectedTask);

        // Act
        var result = await _mockTodoTaskService.Object.GetTodoTaskById(taskId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(taskId, result?.Id);
    }

    [Fact]
    public async Task AddTodoTask_ShouldReturnNewId()
    {
        // Arrange
        var newTask = new TodoTaskAddDto { Title = "New Task" };
        var expectedId = 101;

        _mockTodoTaskService.Setup(s => s.AddTodoTask(newTask)).ReturnsAsync(expectedId);

        // Act
        var result = await _mockTodoTaskService.Object.AddTodoTask(newTask);

        // Assert
        Assert.Equal(expectedId, result);
    }

    [Fact]
    public async Task UpdateTodoTask_ShouldNotThrow()
    {
        // Arrange
        var updateDto = new TodoTaskUpdateDto { Id = 1, Title = "Updated Task" };

        _mockTodoTaskService.Setup(s => s.UpdateTodoTask(updateDto)).Returns(Task.CompletedTask);

        // Act & Assert
        await _mockTodoTaskService.Object.UpdateTodoTask(updateDto); // if no exception, test passes
    }

    [Fact]
    public async Task DeleteTodoTask_ShouldNotThrow()
    {
        // Arrange
        var taskId = 1;

        _mockTodoTaskService.Setup(s => s.DeleteTodoTask(taskId)).Returns(Task.CompletedTask);

        // Act & Assert
        await _mockTodoTaskService.Object.DeleteTodoTask(taskId); // if no exception, test passes
    }
}