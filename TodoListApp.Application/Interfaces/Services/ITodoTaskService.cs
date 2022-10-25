using TodoListApp.Domain;

namespace TodoListApp.Application.Interfaces.Services
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
        string Title;
        DateTime? DueDate;
    }

    public class EditTodoTaskRequest
    {
        int id;
        string Title;
        DateTime? DueDate;
        bool? IsCompleted;
    }
}
