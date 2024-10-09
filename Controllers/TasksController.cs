using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskTrackerAPI.DTOs;
using TaskTrackerAPI.Models;
using TaskTrackerAPI.Repositories;

namespace TaskTrackerAPI.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class TasksController(ITaskRepository taskRepository) : ControllerBase
{
    [HttpGet("taskFor/{username}")]
    public async Task<IActionResult> GetAllTasksByUsername(string username)
    {
        var tasks = await taskRepository.GetAllTasksByUsernameAsync(username);
        return Ok(tasks);
    }

    [HttpGet("taskFor/{id:int}")]
    public async Task<IActionResult> GetTaskById(int id)
    {
        var task = await taskRepository.GetByIdAsync(id);
        if (task == null) return NotFound();
        return Ok(task);
    }

    [HttpPost]
    public async Task<IActionResult> CreateTask([FromBody] CreateTaskDto createTaskDto)
    {
        var task = new TaskModel
        {
            Title = createTaskDto.Title,
            Description = createTaskDto.Description,
            StartDate = createTaskDto.StartDate,
            EndDate = createTaskDto.EndDate,
            IsCompleted = false,
            Username = createTaskDto.Username
        };

        await taskRepository.AddAsync(task);
        return CreatedAtAction(nameof(GetTaskById), new { id = task.Id }, task);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateTask([FromBody] UpdateTaskDto taskItem, int id)
    {
        var existingTask = await taskRepository.GetByIdAsync(id);
        if (existingTask == null) return NotFound();

        var task = await taskRepository.UpdateTaskAsync(taskItem, id);
        return Ok(task);
    }

    [HttpPut("complete/{id:int}")]
    public async Task<IActionResult> CompleteTask(int id)
    {
        var existingTask = await taskRepository.GetByIdAsync(id);
        if (existingTask is not { IsCompleted: false })
            return NotFound();
        var task = await taskRepository.CompleteTaskAsync(existingTask, id);
        return Ok(task);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteTask(int id)
    {
        var task = await taskRepository.DeleteAsync(id);
        var exTask = await taskRepository.GetByIdAsync(id);
        if (exTask == null) return Ok(task);
        return NotFound();
    }
}