using System.Linq.Expressions;
using TodoListApp.Domain;

namespace TodoListApp.Application.Abstractions.Repo
{
    /// <summary>
    /// Contract for the task repository
    /// </summary>
    public interface ITodoTaskRepository
    {
        IEnumerable<TodoTask> GetWhere(Expression<Func<TodoTask, bool>> predicate);

        int Add(TodoTask entity);

        void Update(int id, string? title, DateTime? dueDate, bool? isCompleted);
    }
}
