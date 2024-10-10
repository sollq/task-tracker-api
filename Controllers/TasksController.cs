using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskTrackerAPI.DTOs;
using TaskTrackerAPI.Models;
using TaskTrackerAPI.Repositories;

namespace TaskTrackerAPI.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class TasksController(ITaskRepository taskRepository, ILogger<TasksController> logger) : ControllerBase
{
    private readonly ITaskRepository _taskRepository = taskRepository;
    private readonly ILogger<TasksController> _logger = logger;

    [HttpGet("taskFor/{username}")]
    public async Task<IActionResult> GetAllTasksByUsername(string username)
    {
        _logger.LogInformation("Fetching tasks for user {Username}", username);

        var tasks = await _taskRepository.GetAllTasksByUsernameAsync(username);
        return Ok(tasks);
    }

    [HttpGet("taskFor/{id:int}")]
    public async Task<IActionResult> GetTaskById(int id)
    {
        _logger.LogInformation("Fetching tasks for id {Id}", id);

        var task = await _taskRepository.GetByIdAsync(id);
        if (task == null) return NotFound();
        return Ok(task);
    }

    [HttpPost]
    public async Task<IActionResult> CreateTask([FromBody] CreateTaskDto createTaskDto)
    {
        _logger.LogInformation("Creating a new task for user {Username}", createTaskDto.Username);

        var task = new TaskModel
        {
            Title = createTaskDto.Title,
            Description = createTaskDto.Description,
            StartDate = createTaskDto.StartDate,
            EndDate = createTaskDto.EndDate,
            IsCompleted = false,
            Username = createTaskDto.Username
        };

        await _taskRepository.AddAsync(task);
        return CreatedAtAction(nameof(GetTaskById), new { id = task.Id }, task);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateTask([FromBody] UpdateTaskDto taskItem, int id)
    {
        _logger.LogInformation("Updating task {Id}", id);

        var existingTask = await _taskRepository.GetByIdAsync(id);
        if (existingTask == null) return NotFound();

        var task = await _taskRepository.UpdateTaskAsync(taskItem, id);
        return Ok(task);
    }

    [HttpPut("complete/{id:int}")]
    public async Task<IActionResult> CompleteTask(int id)
    {
        _logger.LogInformation("Changing task to comleted state {Id}", id);

        var existingTask = await _taskRepository.GetByIdAsync(id);
        if (existingTask is not { IsCompleted: false })
            return NotFound();
        var task = await _taskRepository.CompleteTaskAsync(existingTask, id);
        return Ok(task);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteTask(int id)
    {
        _logger.LogInformation("Deleting task {Id}", id);

        var task = await _taskRepository.DeleteAsync(id);
        var exTask = await _taskRepository.GetByIdAsync(id);
        if (exTask == null) return Ok(task);
        return NotFound();
    }
}