using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using TodoListApp.Application.Abstractions;
using TodoListApp.Application.Abstractions.Services;
using TodoListApp.Application.Implementations.Services;
using TodoListApp.Domain;
using TodoListApp.Infrastructure.Data;
using TodoListApp.Infrastructure.Data.Repo;

namespace TodoListApp.Tests
{
    /// <summary>
    /// Contains unit tests for Edit Task logic
    /// </summary>
    [TestClass]
    public class EditTaskTests
    {
        private IDateTimeProvider _dateTimeProviderMock;
        private ILogger<TodoTaskService> _loggerMock;
        private readonly DateTime _pastDate = new DateTime(2022, 10, 24, 0, 0, 0);
        private readonly DateTime _futureDate = new DateTime(2022, 10, 26, 0, 0, 0);


        [TestInitialize]
        public void Initialize()
        {
            Mock<IDateTimeProvider> mockDateTimeProvider = new Mock<IDateTimeProvider>();
            DateTime currentDate = new DateTime(2022, 10, 25, 0, 0, 0);
            mockDateTimeProvider.Setup(p => p.Now()).Returns(currentDate);

            _dateTimeProviderMock = mockDateTimeProvider.Object;

            var loggerMock = new Mock<ILogger<TodoTaskService>>();
            _loggerMock = loggerMock.Object;

        }

        /// <summary>
        /// Service edit method should return false and no tasks should be edited if no matching ID is given on
        /// edit request.
        /// </summary>
        [TestMethod]
        public void TestEditTask_NoIdMatch()
        {
            // Arrange
            var todoTasks = new List<TodoTask>()
            {
                new TodoTask
                {
                    Id = 1,
                    Title = "Test TodoTask - 1",
                    DueDate = _pastDate,
                    Completed = false
                },
                new TodoTask
                {
                    Id = 2,
                    Title = "Test TodoTask - 2",
                    DueDate = _futureDate,
                    Completed = false
                },
            }.AsQueryable();
            var mockSet = new Mock<DbSet<TodoTask>>();

            mockSet.As<IQueryable<TodoTask>>().Setup(m => m.Provider).Returns(todoTasks.Provider);
            mockSet.As<IQueryable<TodoTask>>().Setup(m => m.Expression).Returns(todoTasks.Expression);
            mockSet.As<IQueryable<TodoTask>>().Setup(m => m.ElementType).Returns(todoTasks.ElementType);
            mockSet.As<IQueryable<TodoTask>>().Setup(m => m.GetEnumerator()).Returns(() => todoTasks.GetEnumerator());

            var mockContext = new Mock<AppDbContext>();
            mockContext.Setup(m => m.TodoTasks).Returns(mockSet.Object);

            var repo = new TodoTaskRepository(mockContext.Object);
            var _sut = new TodoTaskService(repo, _dateTimeProviderMock, _loggerMock);

            EditTodoTaskRequest editRequest = new EditTodoTaskRequest
            {
                id = 5,
                Title = "New title",
                DueDate = _futureDate,
                IsCompleted = true
            };

            // Act
            var editResult = _sut.EditTask(editRequest);

            // Assert
            Assert.IsFalse(editResult);
        }

        /// <summary>
        /// Service edit method should return true and the target task should be edited if the ID of an existing task is given on
        /// edit request.
        /// </summary>
        [TestMethod]
        public void TestEditTask_IdMatch()
        {
            // Arrange
            var todoTasks = new List<TodoTask>()
            {
                new TodoTask
                {
                    Id = 1,
                    Title = "Test TodoTask - 1",
                    DueDate = _pastDate,
                    Completed = false
                },
                new TodoTask
                {
                    Id = 2,
                    Title = "Test TodoTask - 2",
                    DueDate = _futureDate,
                    Completed = false
                },
            }.AsQueryable();
            var mockSet = new Mock<DbSet<TodoTask>>();

            mockSet.As<IQueryable<TodoTask>>().Setup(m => m.Provider).Returns(todoTasks.Provider);
            mockSet.As<IQueryable<TodoTask>>().Setup(m => m.Expression).Returns(todoTasks.Expression);
            mockSet.As<IQueryable<TodoTask>>().Setup(m => m.ElementType).Returns(todoTasks.ElementType);
            mockSet.As<IQueryable<TodoTask>>().Setup(m => m.GetEnumerator()).Returns(() => todoTasks.GetEnumerator());

            var mockContext = new Mock<AppDbContext>();
            mockContext.Setup(m => m.TodoTasks).Returns(mockSet.Object);

            var repo = new TodoTaskRepository(mockContext.Object);
            var _sut = new TodoTaskService(repo, _dateTimeProviderMock, _loggerMock);

            var newTargetDate = _futureDate.AddDays(1);
            EditTodoTaskRequest editRequest = new EditTodoTaskRequest
            {
                id = 1,
                Title = "New title",
                DueDate = newTargetDate,
                IsCompleted = true
            };

            // Act
            var editResult = _sut.EditTask(editRequest);

            // Assert
            Assert.IsTrue(editResult);

            var editedTask = mockContext.Object.TodoTasks.SingleOrDefault(i => i.Id == 1);
            Assert.IsNotNull(editedTask);
            Assert.AreEqual(editedTask.Title, editRequest.Title);
            Assert.AreEqual(editedTask.DueDate, editRequest.DueDate);
            Assert.AreEqual(editedTask.Completed, editRequest.IsCompleted);

            var nonEditedTask = mockContext.Object.TodoTasks.SingleOrDefault(i => i.Id == 2);
            Assert.IsNotNull(nonEditedTask);
            Assert.AreEqual(nonEditedTask.Title, "Test TodoTask - 2");
        }
    }
}
