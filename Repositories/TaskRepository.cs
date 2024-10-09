using Microsoft.EntityFrameworkCore;
using TaskTrackerAPI.Data;
using TaskTrackerAPI.DTOs;
using TaskTrackerAPI.Models;

namespace TaskTrackerAPI.Repositories;

public class TaskRepository(AppDbContext context) : ITaskRepository
{
    public async Task<IEnumerable<TaskModel>> GetAllTasksByUsernameAsync(string username)
    {
        var tasks = await context.Tasks.ToListAsync();
        return tasks.FindAll(t => t.Username == username).AsEnumerable();
    }

    public async Task<TaskModel?> GetByIdAsync(int id)
    {
        return await context.Tasks.SingleOrDefaultAsync(t => t.Id == id);
    }

    public async Task<Task> AddAsync(TaskModel taskItem)
    {
        await context.Tasks.AddAsync(taskItem);
        await context.SaveChangesAsync();
        return Task.CompletedTask;
    }

    public async Task<Task> CompleteTaskAsync(TaskModel task, int id)
    {
        task.IsCompleted = true;
        await UpdateTaskAsync(task);
        return Task.CompletedTask;
    }

    public async Task<Task> UpdateTaskAsync(UpdateTaskDto taskItem, int id)
    {
        var existingTask = await context.Tasks.SingleOrDefaultAsync(t => t.Id == id);
        if (existingTask != null)
        {
            existingTask.Title = taskItem.Title;
            existingTask.Description = taskItem.Description;
            existingTask.IsCompleted = taskItem.IsCompleted;
            await UpdateTaskAsync(existingTask);
        }

        return Task.CompletedTask;
    }

    public async Task<Task> DeleteAsync(int id)
    {
        var task = await context.Tasks.SingleOrDefaultAsync(t => t.Id == id);
        if (task != null) context.Tasks.Remove(task);
        await context.SaveChangesAsync();
        return Task.CompletedTask;
    }

    public async Task<Task> UpdateTaskAsync(TaskModel taskItem)
    {
        context.Tasks.Update(taskItem);
        await context.SaveChangesAsync();
        return Task.CompletedTask;
    }
}