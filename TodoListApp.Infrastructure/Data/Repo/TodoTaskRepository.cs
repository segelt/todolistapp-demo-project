using System.Linq.Expressions;
using TodoListApp.Application.Abstractions.Repo;
using TodoListApp.Domain;

namespace TodoListApp.Infrastructure.Data.Repo
{
    /// <summary>
    /// Repository layer that interacts with the database. <see cref="AppDbContext"/> is injected
    /// to allow for unit testing of the repository. During tests, the DbContext is mocked.
    /// </summary>
    public class TodoTaskRepository : ITodoTaskRepository
    {
        private readonly AppDbContext _dbContext;

        public TodoTaskRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int Add(TodoTask entity)
        {
            _dbContext.TodoTasks.Add(entity);
            _dbContext.SaveChanges();

            return entity.Id;
        }

        public IEnumerable<TodoTask> GetWhere(Expression<Func<TodoTask, bool>> predicate)
        {
            return _dbContext.TodoTasks.Where(predicate);
        }

        public void Update(int id, string? title, DateTime? dueDate, bool? isCompleted)
        {
            TodoTask? targetTask = _dbContext.TodoTasks.FirstOrDefault(e => e.Id == id);

            if (targetTask == null)
            {
                throw new ArgumentException($"Target task not found for id: {id}.");
            }

            if (title != null)
            {
                targetTask.Title = title;
            }
            if (dueDate != null)
            {
                targetTask.DueDate = dueDate;
            }

            if (isCompleted.HasValue)
            {
                targetTask.Completed = isCompleted.Value;
            }

            _dbContext.SaveChanges();
        }
    }
}
