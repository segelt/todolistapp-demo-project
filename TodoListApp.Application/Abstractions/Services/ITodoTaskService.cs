using TodoListApp.Domain;

namespace TodoListApp.Application.Abstractions.Services
{
    public interface ITodoTaskService
    {
        int? CreateTodoTask(CreateTodoTaskRequest createRequest);

        /// <summary>
        /// Edits the TodoTask specified by the ID.
        /// Only the non-null edit request fields are edited.
        /// </summary>
        /// <param name="editRequest"></param>
        /// <returns></returns>
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
        public string? Title { get; set; }
        public DateTime? DueDate { get; set; }
        public bool? IsCompleted { get; set; }
    }
}
