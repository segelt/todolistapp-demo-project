using TodoListApp.Domain;

namespace TodoListApp.Application.Abstractions.Services
{
    public interface ITodoTaskService
    {
        int? CreateTodoTask(CreateTodoTaskRequest createRequest);
        bool EditTask(EditTodoTaskRequest editRequest);
        IEnumerable<TodoTask> GetPendingTasks();
        IEnumerable<TodoTask> GetOverdueTasks();
    }

    public class CreateTodoTaskRequest
    {
        public string Title { get; set; }
        public DateTime? DueDate { get; set; }
    }

    public class EditTodoTaskRequest
    {
        public int id { get; set; }
        public string Title { get; set; }
        public DateTime? DueDate { get; set; }
        public bool? IsCompleted { get; set; }
    }
}
