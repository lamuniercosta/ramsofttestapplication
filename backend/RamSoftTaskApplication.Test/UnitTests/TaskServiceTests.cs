using RamSoftTaskApplication.Data.Repositories;
using RamSoftTaskApplication.Models;
using RamSoftTaskApplication.Services.Tasks;

namespace RamSoftTaskApplication.Test.UnitTests;

public class TaskServiceTests
{
    [Fact]
    public async Task GetTaskByIdAsync_ShouldReturnTask_WhenTaskExists()
    {
        // Arrange
        var fixture = new Fixture();
        var taskId = fixture.Create<int>();
        var task = fixture.Create<TaskModel>();
        var taskRepository = Substitute.For<IRepository<TaskModel>>();
        taskRepository.GetByIdAsync(taskId).Returns(task);

        var taskService = new TaskService(taskRepository);

        // Act
        var result = await taskService.GetTaskByIdAsync(taskId);

        // Assert
        result.Should().Be(task);
    }

}