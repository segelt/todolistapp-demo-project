using System.Linq.Expressions;
using TodoListApp.Domain;

namespace TodoListApp.Application.Abstractions.Repo
{
    public interface ITodoTaskRepository
    {
        IEnumerable<TodoTask> Get();

        TodoTask? GetById(int id);

        IEnumerable<TodoTask> GetWhere(Expression<Func<TodoTask, bool>> predicate);

        int Add(TodoTask entity);

        void Remove(int id);

        void Update(int id, string? title, DateTime? dueDate, bool? isCompleted);
    }
}
