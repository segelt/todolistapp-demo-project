using System.Linq.Expressions;
using TodoListApp.Domain;

namespace TodoListApp.Application.Interfaces.Repo
{
    public interface ITodoTaskRepository
    {
        IEnumerable<TodoTask> Get();

        IEnumerable<TodoTask> GetById(int id);

        IEnumerable<TodoTask> GetWhere(Expression<Func<TodoTask, bool>> predicate);

        bool Add(TodoTask entity);

        bool Remove(int id);

        bool Update(TodoTask model, int id);
    }
}
