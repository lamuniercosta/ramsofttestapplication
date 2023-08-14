using RamSoftTaskApplication.Data.Repositories;
using RamSoftTaskApplication.Models;

namespace RamSoftTaskApplication.Services.Tasks;

public class TaskService : ITaskService
{
    
    private readonly IRepository<TaskModel> _taskRepository;

    public TaskService(IRepository<TaskModel> taskRepository)
    {
        _taskRepository = taskRepository;
    }
    public async Task<TaskModel?> GetTaskByIdAsync(int id)
    {
        return await _taskRepository.GetByIdAsync(id).ConfigureAwait(false);
    }

    public async Task<IEnumerable<TaskModel>> GetAllTasksAsync()
    {
        return await _taskRepository.GetAllAsync().ConfigureAwait(false);
    }

    public Task<IEnumerable<TaskModel>> GetTasksByColumnAsync(string columnName)
    {
        throw new NotImplementedException();
    }

    public async Task CreateTaskAsync(TaskModel task)
    {
        await _taskRepository.AddAsync(task).ConfigureAwait(false);
    }

    public async Task UpdateTaskAsync(TaskModel task)
    {
        await _taskRepository.UpdateAsync(task).ConfigureAwait(false);
    }

    public async Task DeleteTaskAsync(int id)
    {
        var task = await _taskRepository.GetByIdAsync(id).ConfigureAwait(false);
        if (task != null)
        {
            await _taskRepository.RemoveAsync(task).ConfigureAwait(false);
        }
    }
}