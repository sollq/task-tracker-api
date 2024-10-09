using Moq;
using NUnit.Framework;
using System.Threading.Tasks;
using TaskTrackerAPI.Controllers;
using TaskTrackerAPI.DTOs;
using TaskTrackerAPI.Models;
using TaskTrackerAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using NUnit.Framework.Legacy;

namespace TaskTrackerAPI.Tests
{
    public class TasksControllerTests
    {
        private Mock<ITaskRepository> _mockTaskRepository;
        private TasksController _controller;

        [SetUp]
        public void Setup()
        {
            _mockTaskRepository = new Mock<ITaskRepository>();
            _controller = new TasksController(_mockTaskRepository.Object);
        }

        [Test]
        public async Task GetAllTasksByUsername_ReturnsOk_WhenTasksExist()
        {
            var username = "testuser";
            _mockTaskRepository
                .Setup(repo => repo.GetAllTasksByUsernameAsync(username))
                .ReturnsAsync([ new TaskModel
                    {
                        Username = username,
                        Title = "Task1",
                        StartDate = default,
                        EndDate = default,
                        IsCompleted = false
                    }
                ]);

            var result = await _controller.GetAllTasksByUsername(username);

            ClassicAssert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            ClassicAssert.NotNull(okResult);
            ClassicAssert.IsInstanceOf<IEnumerable<TaskModel>>(okResult.Value);
        }

        [Test]
        public async Task GetTaskById_ReturnsOk_WhenTaskExists()
        {
            var taskId = 1;
            _mockTaskRepository
                .Setup(repo => repo.GetByIdAsync(taskId))
                .ReturnsAsync(new TaskModel
                {
                    Id = taskId,
                    Title = "Task1",
                    StartDate = default,
                    EndDate = default,
                    IsCompleted = false,
                    Username = null
                });

            var result = await _controller.GetTaskById(taskId);

            ClassicAssert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            ClassicAssert.NotNull(okResult);
            ClassicAssert.IsInstanceOf<TaskModel>(okResult.Value);
        }

        [Test]
        public async Task GetTaskById_ReturnsNotFound_WhenTaskDoesNotExist()
        {
            var taskId = 1;
            _mockTaskRepository
                .Setup(repo => repo.GetByIdAsync(taskId))
                .ReturnsAsync((TaskModel)null);

            var result = await _controller.GetTaskById(taskId);

            ClassicAssert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public async Task CreateTask_ReturnsCreatedAtAction_WhenTaskIsCreated()
        {
            // Arrange
            var createTaskDto = new CreateTaskDto
            {
                Title = "New Task",
                Description = "Task Description",
                StartDate = System.DateTime.Now,
                EndDate = System.DateTime.Now.AddDays(1),
                Username = "testuser"
            };
            var result = await _controller.CreateTask(createTaskDto);

            ClassicAssert.IsInstanceOf<CreatedAtActionResult>(result);
        }
    }
}
