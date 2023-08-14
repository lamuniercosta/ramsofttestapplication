using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using RamSoftTaskApplication.API;
using RamSoftTaskApplication.Models;

namespace RamSoftTaskApplication.Test.IntegrationTests;

public class TaskControllerIntegrationTests : IClassFixture<WebApplicationFactory<Startup>>
{
    private readonly HttpClient _client;

    public TaskControllerIntegrationTests(WebApplicationFactory<Startup> factory)
    {
        _client = factory.CreateClient();
    }
    
    [Fact]
    public async Task CreateTask_ShouldReturnCreatedStatusAndTask()
    {
        // Arrange
        var fixture = new Fixture();
        var taskToAdd = fixture.Create<TaskModel>();

        // Act
        var response = await _client.PostAsJsonAsync("/api/tasks", taskToAdd);

        // Assert
        response.EnsureSuccessStatusCode();
        var createdTask = await response.Content.ReadFromJsonAsync<TaskModel>();
        createdTask.Should().NotBeNull();
        createdTask!.Name.Should().Be(taskToAdd.Name);
    }
    
    [Fact]
    public async Task AddTask_ValidTask_ReturnsCreatedAtActionResult()
    {
        // Arrange
        var newTask = new Fixture().Create<TaskModel>();
            
        // Act
        var response = await _client.PostAsJsonAsync("/api/tasks", newTask);
            
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);
    }
    
    [Fact]
    public async Task UpdateTask_ValidTask_ReturnsNoContentResult()
    {
        // Arrange
        const int taskId = 1;
        var updatedTask = new Fixture().Create<TaskModel>();
            
        // Act
        var response = await _client.PutAsJsonAsync($"/api/tasks/{taskId}", updatedTask);
            
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
    
    [Fact]
    public async Task DeleteTask_ExistingTask_ReturnsNoContentResult()
    {
        // Arrange
        const int taskId = 1;
            
        // Act
        var response = await _client.DeleteAsync($"/api/tasks/{taskId}");
            
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
    
    [Fact]
    public async Task MoveTask_ValidMove_ReturnsNoContentResult()
    {
        // Arrange
        const int taskId = 1;
        var updatedTask = new Fixture().Create<TaskModel>();
        
        // Act
        var response = await _client.PutAsJsonAsync($"/api/tasks/{taskId}", updatedTask);
            
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
}