﻿using Microsoft.EntityFrameworkCore;
using Moq;
using TodoListApp.Application.Abstractions;
using TodoListApp.Application.Implementations.Services;
using TodoListApp.Domain;
using TodoListApp.Infrastructure.Data;
using TodoListApp.Infrastructure.Data.Repo;

namespace TodoListApp.Tests
{
    [TestClass]
    public class OverdueTaskTests
    {
        private IDateTimeProvider _DateTimeProviderMock;
        private readonly DateTime _PastDate = new DateTime(2022, 10, 24, 0, 0, 0);
        private readonly DateTime _FutureDate = new DateTime(2022, 10, 26, 0, 0, 0);


        [TestInitialize]
        public void Initialize()
        {
            Mock<IDateTimeProvider> mockDateTimeProvider = new Mock<IDateTimeProvider>();
            DateTime currentDate = new DateTime(2022, 10, 25, 0, 0, 0);
            mockDateTimeProvider.Setup(p => p.Now()).Returns(currentDate);

            _DateTimeProviderMock = mockDateTimeProvider.Object;
        }

        [TestMethod]
        public void TestOverdueTasks_ByDate()
        {
            // Arrange
            var todoTasks = new List<TodoTask>()
            {
                new TodoTask
                {
                    Id = 1,
                    Title = "Test TodoTask - 1",
                    DueDate = _PastDate,
                    Completed = false
                },
                new TodoTask
                {
                    Id = 2,
                    Title = "Test TodoTask - 2",
                    DueDate = _FutureDate,
                    Completed = false
                },
                new TodoTask
                {
                    Id = 3,
                    Title = "Test TodoTask - 3",
                    DueDate = _PastDate,
                    Completed = false
                },
                new TodoTask
                {
                    Id = 4,
                    Title = "Test TodoTask - 4",
                    DueDate = _FutureDate,
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
            var _sut = new TodoTaskService(repo, _DateTimeProviderMock);

            // Act
            var blogs = _sut.GetOverdueTasks();

            // Assert
            var actualIds = new List<int>() { 1, 3 };
            Assert.AreEqual(blogs.Count(), actualIds.Count);
            Assert.IsTrue(blogs.Select(p => p.Id).All(id => actualIds.Contains(id)));
        }


        [TestMethod]
        public void TestOverdueTasks_ByCompleted()
        {
            // Arrange
            var todoTasks = new List<TodoTask>()
            {
                new TodoTask
                {
                    Id = 1,
                    Title = "Test TodoTask - 1",
                    DueDate = _PastDate,
                    Completed = true
                },
                new TodoTask
                {
                    Id = 2,
                    Title = "Test TodoTask - 2",
                    DueDate = _PastDate,
                    Completed = false
                },
                new TodoTask
                {
                    Id = 3,
                    Title = "Test TodoTask - 3",
                    DueDate = _PastDate,
                    Completed = false
                },
                new TodoTask
                {
                    Id = 4,
                    Title = "Test TodoTask - 4",
                    DueDate = _PastDate,
                    Completed = true
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
            var _sut = new TodoTaskService(repo, _DateTimeProviderMock);

            // Act
            var blogs = _sut.GetOverdueTasks();

            // Assert
            var actualIds = new List<int>() { 2, 3 };
            Assert.AreEqual(blogs.Count(), actualIds.Count);
            Assert.IsTrue(blogs.Select(p => p.Id).All(id => actualIds.Contains(id)));
        }
    }
}