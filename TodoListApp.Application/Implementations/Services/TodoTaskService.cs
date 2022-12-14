using Microsoft.Extensions.Logging;
using TodoListApp.Application.Abstractions;
using TodoListApp.Application.Abstractions.Repo;
using TodoListApp.Application.Abstractions.Services;
using TodoListApp.Domain;

namespace TodoListApp.Application.Implementations.Services
{
    public class TodoTaskService : ITodoTaskService
    {
        private readonly ITodoTaskRepository _todoRepository;

        // Provider that retrieves the current DateTime is injected, to allow for unit testing.
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly ILogger _logger;

        public TodoTaskService(ITodoTaskRepository todoRepository,
            IDateTimeProvider dateTimeProvider,
            ILogger<TodoTaskService> logger)
        {
            _todoRepository = todoRepository;
            _dateTimeProvider = dateTimeProvider;
            _logger = logger;
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
                _todoRepository.Add(targetTask);
                return targetTask.Id;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "TodoTaskService.CreateTodoTask");
                return null;
            }
        }

        public bool EditTask(EditTodoTaskRequest editRequest)
        {
            try
            {
                _todoRepository.Update(editRequest.id, editRequest.Title, editRequest.DueDate, editRequest.IsCompleted);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "TodoTaskService.EditTask");
                return false;
            }
        }

        public IEnumerable<TodoTask> GetPendingTasks()
        {
            DateTime currentDateTime = _dateTimeProvider.Now();
            return _todoRepository.GetWhere(e => e.DueDate == null || e.DueDate.Value > currentDateTime && e.Completed == false);
        }

        public IEnumerable<TodoTask> GetOverdueTasks()
        {
            DateTime currentDateTime = _dateTimeProvider.Now();
            return _todoRepository.GetWhere(e => e.DueDate < currentDateTime && e.Completed == false);
        }

    }
}
