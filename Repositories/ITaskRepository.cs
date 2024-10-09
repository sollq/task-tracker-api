using TaskTrackerAPI.DTOs;
using TaskTrackerAPI.Models;

namespace TaskTrackerAPI.Repositories;

public interface ITaskRepository
{
    Task<IEnumerable<TaskModel>> GetAllTasksByUsernameAsync(string username);
    Task<TaskModel?> GetByIdAsync(int id);
    Task<Task> AddAsync(TaskModel taskItem);
    Task<Task> UpdateTaskAsync(UpdateTaskDto taskItem, int id);
    Task<Task> DeleteAsync(int id);
    Task<Task> CompleteTaskAsync(TaskModel task, int id);
}