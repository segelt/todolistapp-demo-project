using System.Linq.Expressions;
using TodoListApp.Application.Abstractions.Repo;
using TodoListApp.Domain;

namespace TodoListApp.Infrastructure.Data.Repo
{
    public class TodoTaskRepository : ITodoTaskRepository
    {
        private readonly AppDbContext _dbContext;

        public TodoTaskRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(TodoTask entity)
        {
            _dbContext.TodoTasks.Add(entity);
            _dbContext.SaveChanges();
        }

        public IEnumerable<TodoTask> Get()
        {
            return _dbContext.TodoTasks;
        }

        public TodoTask? GetById(int id)
        {
            return _dbContext.TodoTasks.FirstOrDefault(e => e.Id == id);
        }

        public IEnumerable<TodoTask> GetWhere(Expression<Func<TodoTask, bool>> predicate)
        {
            return _dbContext.TodoTasks.Where(predicate);
        }

        public void Remove(int id)
        {
            TodoTask? targetTask = _dbContext.TodoTasks.FirstOrDefault(e => e.Id == id);

            if(targetTask == null)
            {
                throw new ArgumentException($"Target task not found for id: {id}.");
            }

            _dbContext.TodoTasks.Remove(targetTask);
            _dbContext.SaveChanges();
        }

        public void Update(int id, string title, DateTime? dueDate, bool? isCompleted)
        {
            TodoTask? targetTask = _dbContext.TodoTasks.FirstOrDefault(e => e.Id == id);

            if (targetTask == null)
            {
                throw new ArgumentException($"Target task not found for id: {id}.");
            }

            targetTask.Title = title;
            if(dueDate != null)
            {
                targetTask.DueDate = dueDate;
            }

            if(isCompleted.HasValue)
            {
                targetTask.Completed = isCompleted.Value;
            }

            _dbContext.SaveChanges();
        }
    }
}
