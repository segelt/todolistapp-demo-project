using TodoListApp.Application.Abstractions.Repo;
using TodoListApp.Application.Abstractions.Services;
using TodoListApp.Domain;

namespace TodoListApp.Application.Services
{
    public class TodoTaskService : ITodoTaskService
    {
        private readonly ITodoTaskRepository _TodoRepository;

        public TodoTaskService(ITodoTaskRepository todoRepository)
        {
            _TodoRepository = todoRepository;
        }

        public int? CreateTodoTask(CreateTodoTaskRequest createRequest)
        {
            TodoTask targetTask = new TodoTask
            {
                Title = createRequest.Title,
                DueDate = createRequest.DueDate
            };

            try
            {
                _TodoRepository.Add(targetTask);
                return targetTask.Id;
            }
            catch (Exception)
            {
                // TODO: Log the exception
                return null;
            }
        }

        public bool EditTask(EditTodoTaskRequest editRequest)
        {
            try
            {
                _TodoRepository.Update(editRequest.id, editRequest.Title, editRequest.DueDate, editRequest.IsCompleted);
                return true;
            }
            catch(Exception)
            {
                // TODO: Log the exception
                return false;
            }
        }

        public IEnumerable<TodoTask> GetPendingTasks()
        {
            return _TodoRepository.GetWhere(e => e.DueDate > DateTime.Now && e.Completed == false);
        }

        public IEnumerable<TodoTask> GetOverdueTasks()
        {
            return _TodoRepository.GetWhere(e => e.DueDate < DateTime.Now && e.Completed == false);
        }

    }
}
