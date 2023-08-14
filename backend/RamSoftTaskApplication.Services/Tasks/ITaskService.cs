using RamSoftTaskApplication.Models;

namespace RamSoftTaskApplication.Services.Tasks;

public interface ITaskService
{
    Task<TaskModel?> GetTaskByIdAsync(int id);
    Task<IEnumerable<TaskModel>> GetAllTasksAsync();
    Task<IEnumerable<TaskModel>> GetTasksByColumnAsync(string columnName);
    Task CreateTaskAsync(TaskModel task);
    Task UpdateTaskAsync(TaskModel task);
    Task DeleteTaskAsync(int id);
}