using Microsoft.EntityFrameworkCore;
using Moq;
using TodoListApp.Application.Services;
using TodoListApp.Domain;
using TodoListApp.Infrastructure.Data;
using TodoListApp.Infrastructure.Data.Repo;

namespace TodoListApp.Tests
{
    [TestClass]
    public class TaskServiceMocks
    {
        [TestMethod]
        public void TestPendingTasks()
        {
            var todoTasks = new List<TodoTask>()
            {
                new TodoTask
                {
                    Id = 1,
                    Title = "Test TodoTask - 1",
                    DueDate = DateTime.Now.AddDays(1),
                    Completed = false
                },
                new TodoTask
                {
                    Id = 2,
                    Title = "Test TodoTask - 2",
                    DueDate = DateTime.Now.AddDays(2),
                    Completed = false
                },
                new TodoTask
                {
                    Id = 3,
                    Title = "Test TodoTask - 3",
                    DueDate = DateTime.Now.AddDays(-1),
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
            var svc = new TodoTaskService(repo);

            var blogs = svc.GetPendingTasks();

            Assert.AreEqual(blogs.Count(), 2);
        }
    }
}