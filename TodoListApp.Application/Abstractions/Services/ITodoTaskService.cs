using TodoListApp.Domain;

namespace TodoListApp.Application.Abstractions.Services
{
    public interface ITodoTaskService
    {
        bool CreateTodoTask(CreateTodoTaskRequest createRequest);
        bool EditTask(EditTodoTaskRequest editRequest);
        IEnumerable<TodoTask> GetPendingTasks();
        IEnumerable<TodoTask> GetOverdueTasks();
    }

    public class CreateTodoTaskRequest
    {
        public string Title;
        public DateTime? DueDate;
    }

    public class EditTodoTaskRequest
    {
        public int id;
        public string Title;
        public DateTime? DueDate;
        public bool? IsCompleted;
    }
}
