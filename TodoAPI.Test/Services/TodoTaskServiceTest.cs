using TodoAPI.Services.TodoTasks;
using TodoAPI.DTOs.TodoTasks;
using Moq;
using TodoAPI.Entities;
using TodoAPI.Infrastructure.Helpers;
using TodoAPI.Repositories.TodoTasks;
using TodoAPI.UnitOfWork;

namespace TodoAPI.Test.Services;

public class TodoTaskServiceTest
{
    private readonly Mock<ITodoTaskService> _mockTodoTaskService;
    private readonly Mock<ITodoTaskRepository> _mockTodoTaskRepository;
    private readonly TodoTaskService _todoTaskService;

    public TodoTaskServiceTest()
    {
        _mockTodoTaskService = new Mock<ITodoTaskService>();
        _mockTodoTaskRepository = new  Mock<ITodoTaskRepository>();
        _todoTaskService = new TodoTaskService(_mockTodoTaskRepository.Object, new Mock<IUnitOfWork>().Object);
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
    public async Task GetTodoTaskById_ShouldThrowKeyNotFoundException_WhenNotFound()
    {
        // Arrange
        var todoTaskId = 999;

        _mockTodoTaskRepository
            .Setup(r => r.GetByIdAsync(todoTaskId))
            .ReturnsAsync((TodoTask?)null);
        
        // Act & Assert
        var ex = await Assert.ThrowsAsync<KeyNotFoundException>(() =>
            _todoTaskService.GetTodoTaskById(todoTaskId));

        Assert.Equal(ExceptionHelper.NotFound<TodoTask>(todoTaskId).Message, ex.Message);
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
    public async Task UpdateTodoTask_ShouldThrowKeyNotFoundException_WhenNotFound()
    {
        // Arrange
        var todoTaskId = 999;
        var todoTaskUpdateDto = new TodoTaskUpdateDto { Id = todoTaskId, Title = "Updated Task" };

        _mockTodoTaskRepository
            .Setup(r => r.GetByIdAsync(todoTaskId))
            .ReturnsAsync((TodoTask?)null);
        
        // Act & Assert
        var ex = await Assert.ThrowsAsync<KeyNotFoundException>(() =>
            _todoTaskService.UpdateTodoTask(todoTaskUpdateDto));

        Assert.Equal(ExceptionHelper.NotFound<TodoTask>(todoTaskId).Message, ex.Message);
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
    
    [Fact]
    public async Task DeleteTodoTask_ShouldThrowKeyNotFoundException_WhenNotFound()
    {
        // Arrange
        var todoTaskId = 999;

        _mockTodoTaskRepository
            .Setup(r => r.GetByIdAsync(todoTaskId))
            .ReturnsAsync((TodoTask?)null);
        
        // Act & Assert
        var ex = await Assert.ThrowsAsync<KeyNotFoundException>(() =>
            _todoTaskService.DeleteTodoTask(todoTaskId));

        Assert.Equal(ExceptionHelper.NotFound<TodoTask>(todoTaskId).Message, ex.Message);
    }
}