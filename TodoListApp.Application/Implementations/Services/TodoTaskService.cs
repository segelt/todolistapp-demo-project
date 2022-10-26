using TodoListApp.Application.Abstractions;
using TodoListApp.Application.Abstractions.Repo;
using TodoListApp.Application.Abstractions.Services;
using TodoListApp.Domain;

namespace TodoListApp.Application.Implementations.Services
{
    public class TodoTaskService : ITodoTaskService
    {
        private readonly ITodoTaskRepository _TodoRepository;
        private readonly IDateTimeProvider _DateTimeProvider;

        public TodoTaskService(ITodoTaskRepository todoRepository, IDateTimeProvider dateTimeProvider)
        {
            _TodoRepository = todoRepository;
            _DateTimeProvider = dateTimeProvider;
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
            var currentDateTime = _DateTimeProvider.Now();
            return _TodoRepository.GetWhere(e => e.DueDate > currentDateTime && e.Completed == false);
        }

        public IEnumerable<TodoTask> GetOverdueTasks()
        {
            var currentDateTime = _DateTimeProvider.Now();
            return _TodoRepository.GetWhere(e => e.DueDate < currentDateTime && e.Completed == false);
        }

    }
}
